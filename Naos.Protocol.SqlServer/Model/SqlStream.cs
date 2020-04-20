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
    /// <typeparam name="TId">Type of the key.</typeparam>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1711:IdentifiersShouldNotHaveIncorrectSuffix", Justification = NaosSuppressBecause.CA1711_IdentifiersShouldNotHaveIncorrectSuffix_TypeNameAddedAsSuffixForTestsWhereTypeIsPrimaryConcern)]
    public partial class SqlStream<TId> : IHaveKeyType, IStream<TId>, IReturningProtocol<GetIdAddIfNecessarySerializerDescriptionOp, int>
    {
        private readonly IDictionary<SerializationDescription, DescribedSerializer> serializerDescriptionToDescribedSerializerMap = new Dictionary<SerializationDescription, DescribedSerializer>();

        private readonly IProtocolStreamLocator<TId> streamLocatorProtocols;
        private readonly IReadOnlyDictionary<Type, IProtocol> typeToGetIdProtocolMap;
        private readonly IReadOnlyDictionary<Type, IProtocol> typeToGetTagsProtocolMap;

        /// <summary>
        /// Initializes a new instance of the <see cref="SqlStream{TKey}"/> class.
        /// </summary>
        /// <param name="name">The name of the stream.</param>
        /// <param name="defaultConnectionTimeout">The default connection timeout.</param>
        /// <param name="defaultCommandTimeout">The default command timeout.</param>
        /// <param name="defaultSerializerDescription">Default serializer description to use.</param>
        /// <param name="serializerFactory">The factory to get a serializer to use for objects.</param>
        /// <param name="compressorFactory">The factory to get a compressor to use for objects.</param>
        /// <param name="streamLocatorProtocols">The executor of <see cref="GetStreamLocatorByIdOp{TKey}"/>.</param>
        /// <param name="typeToGetIdProtocolMap">Id extractor protocols by type.</param>
        /// <param name="typeToGetTagsProtocolMap">Tag extractor protocols by type.</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "AllStream", Justification = NaosSuppressBecause.CA1702_CompoundWordsShouldBeCasedCorrectly_AnalyzerIsIncorrectlyDetectingCompoundWordsInUnitTestMethodName)]
        public SqlStream(
            string name,
            TimeSpan defaultConnectionTimeout,
            TimeSpan defaultCommandTimeout,
            SerializationDescription defaultSerializerDescription,
            ISerializerFactory serializerFactory,
            ICompressorFactory compressorFactory,
            IProtocolStreamLocator<TId> streamLocatorProtocols,
            IReadOnlyDictionary<Type, IProtocol> typeToGetIdProtocolMap,
            IReadOnlyDictionary<Type, IProtocol> typeToGetTagsProtocolMap)
        {
            name.MustForArg(nameof(name)).NotBeNullNorWhiteSpace();
            defaultSerializerDescription.MustForArg(nameof(defaultSerializerDescription)).NotBeNull();
            serializerFactory.MustForArg(nameof(serializerFactory)).NotBeNull();
            compressorFactory.MustForArg(nameof(compressorFactory)).NotBeNull();
            streamLocatorProtocols.MustForArg(nameof(streamLocatorProtocols)).NotBeNull();

            var localTypeToGetIdProtocolMap = typeToGetIdProtocolMap ?? new Dictionary<Type, IProtocol>();
            foreach (var localTypeToGetIdProtocol in localTypeToGetIdProtocolMap)
            {
                var type = localTypeToGetIdProtocol.Key;
                var protocol = localTypeToGetIdProtocol.Value;
                var expectedProtocolType = typeof(IReturningProtocol<,>).MakeGenericType(
                    typeof(GetIdFromObjectOp<,>).MakeGenericType(type),
                    typeof(TId),
                    type);
                if (!protocol.GetType().IsAssignableTo(expectedProtocolType))
                {
                    throw new ArgumentException(FormattableString.Invariant($"The type {type.ToStringReadable()} must match the TObject in IReturningProtocol<GetIdFromObjectOp<TObject>, TKey>)"));
                }
            }

            var localTypeToGetTagsProtocolMap = typeToGetTagsProtocolMap ?? new Dictionary<Type, IProtocol>();
            foreach (var localTypeToGetTagsProtocol in localTypeToGetTagsProtocolMap)
            {
                var type = localTypeToGetTagsProtocol.Key;
                var protocol = localTypeToGetTagsProtocol.Value;
                var expectedProtocolType = typeof(IReturningProtocol<,>).MakeGenericType(
                    typeof(GetTagsFromObjectOp<>).MakeGenericType(type),
                    typeof(IReadOnlyDictionary<string, string>));
                if (!protocol.GetType().IsAssignableTo(expectedProtocolType))
                {
                    throw new ArgumentException(FormattableString.Invariant($"The type {type.ToStringReadable()} must match the TObject in IReturningProtocol<GetTagsFromObjectOp<TObject>, IReadOnlyDictionary<string, string>>)"));
                }
            }

            this.Name = name;
            this.DefaultConnectionTimeout = defaultConnectionTimeout;
            this.DefaultCommandTimeout = defaultCommandTimeout;
            this.DefaultSerializerDescription = defaultSerializerDescription;
            this.SerializerFactory = serializerFactory;
            this.CompressorFactory = compressorFactory;
            this.streamLocatorProtocols = streamLocatorProtocols;
            this.typeToGetIdProtocolMap = localTypeToGetIdProtocolMap;
            this.typeToGetTagsProtocolMap = localTypeToGetTagsProtocolMap;
        }

        /// <inheritdoc />
        public string Name { get; private set; }

        /// <summary>
        /// Gets the default serializer description.
        /// </summary>
        /// <value>The default serializer description.</value>
        public SerializationDescription DefaultSerializerDescription { get; private set; }

        /// <summary>
        /// Gets the default connection timeout.
        /// </summary>
        /// <value>The default connection timeout.</value>
        public TimeSpan DefaultConnectionTimeout { get; private set; }

        /// <summary>
        /// Gets the default command timeout.
        /// </summary>
        /// <value>The default command timeout.</value>
        public TimeSpan DefaultCommandTimeout { get; private set; }

        /// <summary>
        /// Gets the type of the key.
        /// </summary>
        /// <value>The type of the key.</value>
        public Type IdType => typeof(TId);

        /// <summary>
        /// Gets the serializer factory.
        /// </summary>
        /// <value>The serializer factory.</value>
        public ISerializerFactory SerializerFactory { get; private set; }

        /// <summary>
        /// Gets the compressor factory.
        /// </summary>
        /// <value>The compressor factory.</value>
        public ICompressorFactory CompressorFactory { get; private set; }

        /// <inheritdoc />
        public StreamLocatorBase Execute(
            GetStreamLocatorByIdOp<TId> operation)
        {
            return this.streamLocatorProtocols.Execute(operation);
        }

        /// <inheritdoc />
        public IReadOnlyCollection<StreamLocatorBase> Execute(
            GetAllStreamLocatorsOp operation)
        {
            return this.streamLocatorProtocols.Execute(operation);
        }

        /// <inheritdoc />
        public void Execute(
            CreateStreamOp<TId> operation)
        {
            var stream = operation.Stream;
            var allLocators = stream.Execute(new GetAllStreamLocatorsOp());
            foreach (var locator in allLocators)
            {
                if (locator is SqlStreamLocator sqlStreamLocator)
                {
                    using (var connection = sqlStreamLocator.OpenSqlConnection())
                    {
                        // should use a transation here!!
                        var streamAlreadyExists = connection.ExecuteScalar<bool>(
                            FormattableString.Invariant($"IF (EXISTS(select * from sys.schemas where name = '{this.Name}'))BEGIN SELECT 'true' END ELSE BEGIN SELECT 'false' END"));
                        if (!streamAlreadyExists)
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
                                                      StreamSchema.BuildCreationScriptForGetLatestByKeySproc(this.Name),
                                                  };

                            foreach (var script in creationScripts)
                            {
                                connection.Execute(script);
                            }
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
            return new SqlStreamDataProtocol<TId, TObject>(this);
        }

        /// <inheritdoc />
        public IReturningProtocol<GetLatestByIdOp<TId, TObject>, TObject> BuildGetLatestByKeyProtocol<TObject>()
        {
            return new SqlStreamDataProtocol<TId, TObject>(this);
        }

        /// <inheritdoc />
        public IReturningProtocol<GetIdFromObjectOp<TId, TObject>, TId> BuildGetIdFromObjectProtocol<TObject>()
        {
            var containsKey = this.typeToGetIdProtocolMap.TryGetValue(typeof(TObject), out IProtocol protocol);
            if (containsKey)
            {
                return (IReturningProtocol<GetIdFromObjectOp<TId, TObject>, TId>)protocol;
            }
            else
            {
                return new GetIdFromObjectProtocol<TId, TObject>();
            }
        }

        /// <inheritdoc />
        public IReturningProtocol<GetTagsFromObjectOp<TObject>, IReadOnlyDictionary<string, string>> BuildGetTagsFromObjectProtocol<TObject>()
        {
            var containsKey = this.typeToGetTagsProtocolMap.TryGetValue(typeof(TObject), out IProtocol protocol);
            if (containsKey)
            {
                return (IReturningProtocol<GetTagsFromObjectOp<TObject>, IReadOnlyDictionary<string, string>>)protocol;
            }
            else
            {
                return new GetTagsFromObjectProtocol<TObject>();
            }
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

            var serializer = this.SerializerFactory.BuildSerializer(
                this.DefaultSerializerDescription,
                unregisteredTypeEncounteredStrategy: UnregisteredTypeEncounteredStrategy.Attempt);

            var serializerDescriptionId = this.Execute(new GetIdAddIfNecessarySerializerDescriptionOp(streamLocator, this.DefaultSerializerDescription));
            var result = new DescribedSerializer(this.DefaultSerializerDescription, serializer, serializerDescriptionId);
            return result;
        }

        /// <inheritdoc />
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope", Justification = "Should dispose correctly.")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2100:Review SQL queries for security vulnerabilities", Justification = "Built internally and should be safe from injection.")]
        public int Execute(
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
                    var resultParameter = new SqlParameter("Result", SqlDbType.Int)
                                          {
                                              Direction = ParameterDirection.Output,
                                          };
                    command.Parameters.Add(resultParameter);

                    command.ExecuteNonQuery();

                    var result = resultParameter.Value;
                    if (result is int intResult)
                    {
                        return intResult;
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
