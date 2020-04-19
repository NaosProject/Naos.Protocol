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
    using System.IO;
    using System.Threading.Tasks;
    using Dapper;
    using Naos.Protocol.Domain;
    using Naos.Protocol.SqlServer.Internal;
    using Naos.Recipes.RunWithRetry;
    using OBeautifulCode.Assertion.Recipes;
    using OBeautifulCode.Compression;
    using OBeautifulCode.Serialization;
    using OBeautifulCode.Serialization.Bson;
    using OBeautifulCode.Type;
    using OBeautifulCode.Type.Recipes;
    using SerializationFormat = OBeautifulCode.Serialization.SerializationFormat;

    /// <summary>
    /// SQL implementation of an <see cref="IStream{TKey}" />.
    /// </summary>
    /// <typeparam name="TKey">Type of the key.</typeparam>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1711:IdentifiersShouldNotHaveIncorrectSuffix", Justification = NaosSuppressBecause.CA1711_IdentifiersShouldNotHaveIncorrectSuffix_TypeNameAddedAsSuffixForTestsWhereTypeIsPrimaryConcern)]
    public partial class SqlStream<TKey> : IHaveKeyType, IStream<TKey>, IModelViaCodeGen, IReturningProtocol<GetIdAddIfNecessarySerializerDescriptionOp, Guid>
    {
        private readonly IDictionary<SerializationDescription, DescribedSerializer> serializerDescriptionToDescribedSerializerMap = new Dictionary<SerializationDescription, DescribedSerializer>();

        private readonly ISerializerFactory serializerFactory;
        private readonly IReturningProtocol<GetStreamLocatorByTypeOp, StreamLocatorBase> getStreamLocatorByType;
        private readonly IReturningProtocol<GetStreamLocatorByKeyOp<TKey>, StreamLocatorBase> getStreamLocatorByKey;
        private readonly IReturningProtocol<GetAllStreamLocatorsOp, IReadOnlyCollection<StreamLocatorBase>> getAllStreamLocators;
        private readonly IReadOnlyDictionary<Type, IProtocol> tagExtractors;

        /// <summary>
        /// Initializes a new instance of the <see cref="SqlStream{TKey}"/> class.
        /// </summary>
        /// <param name="name">The name of the stream.</param>
        /// <param name="defaultTimeout">The default timeout.</param>
        /// <param name="defaultSerializerDescription">Default serializer description to use.</param>
        /// <param name="serializerFactory">The factory to get a serializer to use for objects.</param>
        /// <param name="getStreamLocatorByType">The executor of <see cref="GetStreamLocatorByTypeOp"/>.</param>
        /// <param name="getStreamLocatorByKey">The executor of <see cref="GetStreamLocatorByKeyOp{TKey}"/>.</param>
        /// <param name="getAllStreamLocators">The executor of <see cref="GetAllStreamLocatorsOp"/>.</param>
        /// <param name="tagExtractors">Tag extractor protocols.</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "AllStream", Justification = NaosSuppressBecause.CA1702_CompoundWordsShouldBeCasedCorrectly_AnalyzerIsIncorrectlyDetectingCompoundWordsInUnitTestMethodName)]
        public SqlStream(
            string name,
            TimeSpan defaultTimeout,
            SerializationDescription defaultSerializerDescription,
            ISerializerFactory serializerFactory,
            IReturningProtocol<GetStreamLocatorByTypeOp, StreamLocatorBase> getStreamLocatorByType,
            IReturningProtocol<GetStreamLocatorByKeyOp<TKey>, StreamLocatorBase> getStreamLocatorByKey,
            IReturningProtocol<GetAllStreamLocatorsOp, IReadOnlyCollection<StreamLocatorBase>> getAllStreamLocators,
            IReadOnlyDictionary<Type, IProtocol> tagExtractors)
        {
            name.MustForArg(nameof(name)).NotBeNullNorWhiteSpace();
            defaultSerializerDescription.MustForArg(nameof(defaultSerializerDescription)).NotBeNull();
            serializerFactory.MustForArg(nameof(serializerFactory)).NotBeNull();
            getStreamLocatorByType.MustForArg(nameof(getStreamLocatorByType)).NotBeNull();
            getStreamLocatorByKey.MustForArg(nameof(getStreamLocatorByKey)).NotBeNull();

            var localTagExtractors = tagExtractors ?? new Dictionary<Type, IProtocol>();
            foreach (var localTagExtractor in localTagExtractors)
            {
                var type = localTagExtractor.Key;
                var protocol = localTagExtractor.Value;
                var expectedProtocolType = typeof(IReturningProtocol<,>).MakeGenericType(
                    typeof(GetTagsFromObjectOp<>).MakeGenericType(type),
                    typeof(IReadOnlyDictionary<string, string>));
                if (!protocol.GetType().IsAssignableTo(expectedProtocolType))
                {
                    throw new ArgumentException(FormattableString.Invariant($"The type {type.ToStringReadable()} must match the TObject in IReturningProtocol<GetTagsFromObjectOp<TObject>, IReadOnlyDictionary<string, string>>)"));
                }
            }

            this.Name = name;
            this.DefaultTimeout = defaultTimeout;
            this.DefaultSerializerDescription = defaultSerializerDescription;
            this.serializerFactory = serializerFactory;
            this.getStreamLocatorByType = getStreamLocatorByType;
            this.getStreamLocatorByKey = getStreamLocatorByKey;
            this.getAllStreamLocators = getAllStreamLocators;
            this.tagExtractors = localTagExtractors;
        }

        /// <inheritdoc />
        public string Name { get; private set; }

        /// <summary>
        /// Gets the default serializer description.
        /// </summary>
        /// <value>The default serializer description.</value>
        public SerializationDescription DefaultSerializerDescription { get; private set; }

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
                            StreamSchema.BuildCreationScriptForTypeWithoutVersionSproc(this.Name),
                            StreamSchema.BuildCreationScriptForTypeWithVersionSproc(this.Name),
                            StreamSchema.BuildCreationScriptForSerializerDescriptionSproc(this.Name),
                            StreamSchema.BuildCreationScriptForPutSproc(this.Name),
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
                    throw SqlStreamLocator.BuildInvalidStreamLocatorException(locator.GetType());
                }
            }
        }

        /// <inheritdoc />
        public IVoidProtocol<PutOp<TObject>> BuildPutProtocol<TObject>()
        {
            return new SqlStreamDataProtocol<TKey, TObject>(this);
        }

        /// <inheritdoc />
        public IReturningProtocol<GetTagsFromObjectOp<TObject>, IReadOnlyDictionary<string, string>> BuildGetTagsFromObjectProtocol<TObject>()
        {
            var containsKey = this.tagExtractors.TryGetValue(typeof(TObject), out IProtocol protocol);
            if (!containsKey)
            {
                return new LambdaGetTagsFromObjectProtocol<TObject>(_ => new Dictionary<string, string>());
            }

            return (IReturningProtocol<GetTagsFromObjectOp<TObject>, IReadOnlyDictionary<string, string>>)protocol;
        }

        /// <inheritdoc />
        public IReturningProtocol<GetKeyFromObjectOp<TKey, TObject>, TKey> BuildGetKeyFromObjectProtocol<TObject>()
        {
            return new SqlStreamDataProtocol<TKey, TObject>(this);
        }

        /// <summary>
        /// Gets the described serializer.
        /// </summary>
        /// <param name="streamLocator">The stream locator in case it needs to look up.</param>
        /// <returns>DescribedSerializer.</returns>
        public DescribedSerializer GetDescribedSerializer(
            SqlStreamLocator streamLocator)
        {
            if (this.serializerDescriptionToDescribedSerializerMap.ContainsKey(this.DefaultSerializerDescription))
            {
                return this.serializerDescriptionToDescribedSerializerMap[this.DefaultSerializerDescription];
            }

            var serializer = this.serializerFactory.BuildSerializer(
                this.DefaultSerializerDescription,
                unregisteredTypeEncounteredStrategy: UnregisteredTypeEncounteredStrategy.Attempt);

            var serializerDescriptionId = this.Execute(new GetIdAddIfNecessarySerializerDescriptionOp(streamLocator, this.DefaultSerializerDescription));
            var result = new DescribedSerializer(this.DefaultSerializerDescription, serializer, serializerDescriptionId);
            return result;
        }

        /// <inheritdoc />
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope", Justification = "Should dispose correctly.")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2100:Review SQL queries for security vulnerabilities", Justification = "Built internally and should be safe from injection.")]
        public Guid Execute(
            GetIdAddIfNecessarySerializerDescriptionOp operation)
        {
            var serializationConfigurationTypeWithoutVersion = operation.SerializationDescription.ConfigurationTypeRepresentation.AssemblyQualifiedName;
            var serializationConfigurationTypeWithVersion = operation.SerializationDescription.ConfigurationTypeRepresentation.AssemblyQualifiedName;

            var storedProcedureName = StreamSchema.BuildSerializerDescriptionSprocName(this.Name);
            using (var connection = operation.StreamLocator.OpenSqlConnection())
            {
                using (var command = new SqlCommand(storedProcedureName, connection)
                                     {
                                         CommandType = CommandType.StoredProcedure,
                                     })
                {
                    command.Parameters.Add(new SqlParameter("AssemblyQualitifiedNameWithoutVersion", serializationConfigurationTypeWithoutVersion));
                    command.Parameters.Add(new SqlParameter("AssemblyQualitifiedNameWithVersion", serializationConfigurationTypeWithVersion));
                    command.Parameters.Add(new SqlParameter(nameof(SerializationKind), operation.SerializationDescription.SerializationKind));
                    command.Parameters.Add(new SqlParameter(nameof(SerializationFormat), operation.SerializationDescription.SerializationFormat));
                    command.Parameters.Add(new SqlParameter(nameof(CompressionKind), operation.SerializationDescription.CompressionKind));
                    command.Parameters.Add(
                        new SqlParameter(nameof(UnregisteredTypeEncounteredStrategy), UnregisteredTypeEncounteredStrategy.Attempt));
                    var resultParameter = new SqlParameter("Result", SqlDbType.UniqueIdentifier)
                                          {
                                              Direction = ParameterDirection.Output,
                                          };
                    command.Parameters.Add(resultParameter);

                    command.ExecuteNonQuery();

                    var result = resultParameter.Value;
                    if (result is Guid guidResult)
                    {
                        return guidResult;
                    }
                    else
                    {
                        throw new InvalidDataException(
                            FormattableString.Invariant(
                                $"Result from [{this.Name}].[{storedProcedureName}] was expected to be of type int but was {result?.GetType().ToStringReadable()} ({result})."));
                    }
                }
            }
        }
    }
}
