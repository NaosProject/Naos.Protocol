// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SqlStream.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Protocol.SqlServer
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Threading.Tasks;
    using Dapper;
    using Naos.Protocol.Domain;
    using Naos.Protocol.SqlServer.Internal;
    using Naos.Recipes.RunWithRetry;
    using OBeautifulCode.Assertion.Recipes;
    using OBeautifulCode.Type;

    /// <summary>
    /// SQL implementation of an <see cref="IStream{TKey}" />.
    /// </summary>
    /// <typeparam name="TKey">Type of the key.</typeparam>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1711:IdentifiersShouldNotHaveIncorrectSuffix", Justification = NaosSuppressBecause.CA1711_IdentifiersShouldNotHaveIncorrectSuffix_TypeNameAddedAsSuffixForTestsWhereTypeIsPrimaryConcern)]
    public partial class SqlStream<TKey> : IHaveKeyType, IStream<TKey>, IModelViaCodeGen
    {
        private readonly IReturningProtocol<GetStreamLocatorByTypeOp, StreamLocatorBase> getStreamLocatorByType;
        private readonly IReturningProtocol<GetStreamLocatorByKeyOp<TKey>, StreamLocatorBase> getStreamLocatorByKey;
        private readonly IReturningProtocol<GetAllStreamLocatorsOp, IReadOnlyCollection<StreamLocatorBase>> getAllStreamLocators;

        /// <summary>
        /// Initializes a new instance of the <see cref="SqlStream{TKey}"/> class.
        /// </summary>
        /// <param name="name">The name of the stream.</param>
        /// <param name="defaultTimeout">The default timeout.</param>
        /// <param name="getStreamLocatorByType">The executor of <see cref="GetStreamLocatorByTypeOp"/>.</param>
        /// <param name="getStreamLocatorByKey">The executor of <see cref="GetStreamLocatorByKeyOp{TKey}"/>.</param>
        /// <param name="getAllStreamLocators">The executor of <see cref="GetAllStreamLocatorsOp"/>.</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "AllStream", Justification = NaosSuppressBecause.CA1702_CompoundWordsShouldBeCasedCorrectly_AnalyzerIsIncorrectlyDetectingCompoundWordsInUnitTestMethodName)]
        public SqlStream(
            string name,
            TimeSpan defaultTimeout,
            IReturningProtocol<GetStreamLocatorByTypeOp, StreamLocatorBase> getStreamLocatorByType,
            IReturningProtocol<GetStreamLocatorByKeyOp<TKey>, StreamLocatorBase> getStreamLocatorByKey,
            IReturningProtocol<GetAllStreamLocatorsOp, IReadOnlyCollection<StreamLocatorBase>> getAllStreamLocators)
        {
            name.MustForArg(nameof(name)).NotBeNullNorWhiteSpace();
            getStreamLocatorByType.MustForArg(nameof(getStreamLocatorByType)).NotBeNull();
            getStreamLocatorByKey.MustForArg(nameof(getStreamLocatorByKey)).NotBeNull();

            this.Name = name;
            this.DefaultTimeout = defaultTimeout;
            this.getStreamLocatorByType = getStreamLocatorByType;
            this.getStreamLocatorByKey = getStreamLocatorByKey;
            this.getAllStreamLocators = getAllStreamLocators;
        }

        /// <inheritdoc />
        public string Name { get; private set; }

        /// <summary>
        /// Gets the default timeout.
        /// </summary>
        /// <value>The default timeout.</value>
        public TimeSpan DefaultTimeout { get; private set; }

        /// <summary>
        /// Gets the type of the key.
        /// </summary>
        /// <value>The type of the key.</value>
        public Type KeyType => typeof(TKey);

        /// <inheritdoc />
        public StreamLocatorBase Execute(
            GetStreamLocatorByKeyOp<TKey> operation)
        {
            return this.getStreamLocatorByKey.Execute(operation);
        }

        /// <inheritdoc />
        public StreamLocatorBase Execute(
            GetStreamLocatorByTypeOp operation)
        {
            return this.getStreamLocatorByType.Execute(operation);
        }

        /// <inheritdoc />
        public IReadOnlyCollection<StreamLocatorBase> Execute(
            GetAllStreamLocatorsOp operation)
        {
            return this.getAllStreamLocators.Execute(operation);
        }

        /// <inheritdoc />
        public void Execute(
            CreateStreamOp<TKey> operation)
        {
            var stream = operation.Stream;
            var allLocators = stream.Execute(new GetAllStreamLocatorsOp());
            foreach (var locator in allLocators)
            {
                if (locator is SqlStreamLocator sqlStreamLocator)
                {
                    using (var connection = sqlStreamLocator.OpenSqlConnection())
                    {
                        var creationScripts = new[]
                        {
                            StreamSchema.BuildCreationScriptForSchema(this.Name),
                            StreamSchema.BuildCreationScriptForTypeWithVersion(this.Name),
                            StreamSchema.BuildCreationScriptForTypeWithoutVersion(this.Name),
                            StreamSchema.BuildCreationScriptForSerializerDescription(this.Name),
                            StreamSchema.BuildCreationScriptForObject(this.Name),
                            StreamSchema.BuildCreationScriptForTag(this.Name),
                        };

                        /*
                        Task ExecuteFinalSqlSetupScripts(Server server)
                        {
                            foreach (var script in creationScripts)
                            {
                                // because it might contain "GO" statements most likely this needs to be executed via the SMO connection.
                                server.ConnectionContext.ExecuteNonQuery(script);
                            }

                            return Task.Run(() => { });
                        }
                        Run.TaskUntilCompletion(SqlServerDatabaseManager.RunOperationOnSmoServerAsync(ExecuteFinalSqlSetupScripts, connection));
                        */

                        foreach (var script in creationScripts)
                        {
                            // because it might contain "GO" statements most likely this needs to be executed via the SMO connection.
                            connection.Execute(script);
                        }
                    }
                }
                else
                {
                    throw new NotSupportedException(FormattableString.Invariant($"Only {nameof(SqlStreamLocator)}'s are supported; provided was: {locator.GetType()}"));
                }
            }
        }
    }
}
