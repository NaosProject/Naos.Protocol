// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SqlStreamDataProtocol.cs" company="Naos Project">
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
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using Naos.Protocol.Domain;
    using Naos.Protocol.SqlServer.Internal;
    using OBeautifulCode.Assertion.Recipes;
    using OBeautifulCode.Compression;
    using OBeautifulCode.Representation.System;
    using OBeautifulCode.Serialization;
    using OBeautifulCode.Type.Recipes;
    using SerializationFormat = OBeautifulCode.Serialization.SerializationFormat;

    /// <summary>
    /// Class SqlStreamDataProtocol.
    /// </summary>
    /// <typeparam name="TKey">The type of the key.</typeparam>
    /// <typeparam name="TObject">The type of the object.</typeparam>
    public class SqlStreamDataProtocol<TKey, TObject> : IVoidProtocol<PutOp<TObject>>,
                                                        IReturningProtocol<GetKeyFromObjectOp<TKey, TObject>, TKey>,
                                                        IReturningProtocol<GetTagsFromObjectOp<TObject>, IReadOnlyDictionary<string, string>>,
                                                        IReturningProtocol<GetLatestByKeyOp<TKey, TObject>, TObject>
    {
        private readonly SqlStream<TKey> stream;

        /// <summary>
        /// Initializes a new instance of the <see cref="SqlStreamDataProtocol{TKey, TObject}"/> class.
        /// </summary>
        /// <param name="stream">The stream to operation against.</param>
        public SqlStreamDataProtocol(
            SqlStream<TKey> stream)
        {
            stream.MustForArg(nameof(stream)).NotBeNull();

            this.stream = stream;
        }

        /// <inheritdoc />
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope", Justification = "Should dispose correctly.")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2100:Review SQL queries for security vulnerabilities", Justification = "Name is built internally.")]
        public void Execute(PutOp<TObject> operation)
        {
            var key = this.Execute(new GetKeyFromObjectOp<TKey, TObject>(operation.Payload));
            var locator = this.stream.Execute(new GetStreamLocatorByKeyOp<TKey>(key));
            if (locator is SqlStreamLocator sqlStreamLocator)
            {
                var objectType = operation.Payload?.GetType() ?? typeof(TObject);
                var objectTypeWithoutVersion = objectType.AssemblyQualifiedName;
                var objectTypeWithVersion = objectType.AssemblyQualifiedName;
                var describedSerializer = this.stream.GetDescribedSerializer(sqlStreamLocator);
                var tags = this.Execute(new GetTagsFromObjectOp<TObject>(operation.Payload));
                var tagsXml = new StringBuilder();
                tagsXml.Append("<Tags>");
                foreach (var tag in tags ?? new Dictionary<string, string>())
                {
                    tagsXml.Append("<Tag ");
                    tagsXml.Append(FormattableString.Invariant($"Name=\"{tag.Key}\" Value=\"{tag.Value}\""));
                    tagsXml.Append("/>");
                }

                tagsXml.Append("</Tags>");

                var serializedPayload = describedSerializer.Serializer.SerializeToString(operation.Payload);
                var serializedKey = (key is string stringKey) ? stringKey : describedSerializer.Serializer.SerializeToString(key);
                var storedProcedureName = StreamSchema.BuildPutSprocName(this.stream.Name);
                using (var connection = sqlStreamLocator.OpenSqlConnection())
                {
                    using (var command = new SqlCommand(storedProcedureName, connection)
                                         {
                                             CommandType = CommandType.StoredProcedure,
                                         })
                    {
                        command.Parameters.Add(new SqlParameter("AssemblyQualitifiedNameWithoutVersion", objectTypeWithoutVersion));
                        command.Parameters.Add(new SqlParameter("AssemblyQualitifiedNameWithVersion", objectTypeWithVersion));
                        command.Parameters.Add(
                            new SqlParameter(
                                nameof(describedSerializer.SerializerDescriptionId),
                                describedSerializer.SerializerDescriptionId));
                        command.Parameters.Add(new SqlParameter("SerializedKey", serializedKey));
                        command.Parameters.Add(new SqlParameter("SerializedPayload", serializedPayload));
                        command.Parameters.Add(new SqlParameter("Tags", tagsXml.ToString()));

                        command.ExecuteNonQuery();
                    }
                }
            }
            else
            {
                throw SqlStreamLocator.BuildInvalidStreamLocatorException(locator.GetType());
            }
        }

        /// <inheritdoc />
        public TKey Execute(
            GetKeyFromObjectOp<TKey, TObject> operation)
        {
            if (operation.ObjectToDetermineKeyFrom is IHaveKey<TKey> hasKey)
            {
                return hasKey.Key;
            }
            else if (operation.ObjectToDetermineKeyFrom.GetType().GetProperties(BindingFlags.DeclaredOnly).Any(_ => _.Name == "Key"))
            {
                var property = operation.ObjectToDetermineKeyFrom.GetType().GetProperties(BindingFlags.DeclaredOnly).Single(_ => _.Name == "Key");
                if (property.PropertyType == typeof(TKey))
                {
                    return (TKey)property.GetValue(operation.ObjectToDetermineKeyFrom);
                }
                else
                {
                    var message = FormattableString.Invariant(
                        $"Type of key {typeof(TKey).ToStringReadable()} does not match 'Key' property type {property.PropertyType.ToStringReadable()}.");
                    throw new ArgumentException(message);
                }
            }
            else
            {
                throw new NotImplementedException(FormattableString.Invariant($"Currently do not have an implementation for getting a Key without using the {nameof(IHaveKey<TKey>)} interface."));
            }
        }

        /// <inheritdoc />
        public IReadOnlyDictionary<string, string> Execute(
            GetTagsFromObjectOp<TObject> operation)
        {
            var protocol = this.stream.BuildGetTagsFromObjectProtocol<TObject>();
            var result = protocol.Execute(operation);
            return result;
        }

        /// <inheritdoc />
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2100:Review SQL queries for security vulnerabilities", Justification = "Internally generated and should be safe.")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope", Justification = "Should be disposing correctly.")]
        public TObject Execute(
            GetLatestByKeyOp<TKey, TObject> operation)
        {
            var locator = this.stream.Execute(new GetStreamLocatorByKeyOp<TKey>(operation.Key));
            if (locator is SqlStreamLocator sqlStreamLocator)
            {
                var serializedKey = (operation.Key is string stringKey)
                    ? stringKey
                    : this.stream.GetDescribedSerializer(sqlStreamLocator).Serializer.SerializeToString(operation.Key);

                var storedProcedureName = StreamSchema.BuildGetLatestByKeySprocName(this.stream.Name);

                SerializationKind serializationKind;
                SerializationFormat serializationFormat;
                string serializationConfigAssemblyQualifiedNameWithoutVersion;
                CompressionKind compressionKind;
                string serializedPayload;

                using (var connection = sqlStreamLocator.OpenSqlConnection())
                {
                    using (var command = new SqlCommand(storedProcedureName, connection)
                    {
                        CommandType = CommandType.StoredProcedure,
                    })
                    {
                        command.Parameters.Add(new SqlParameter("SerializedKey", serializedKey));
                        var serializationConfigAssemblyQualifiedNameWithoutVersionParam = new SqlParameter("SerializationConfigAssemblyQualifiedNameWithoutVersion", SqlDbType.NVarChar, 2000)
                                                                                          {
                                                                                              Direction = ParameterDirection.Output,
                                                                                          };
                        var serializationKindParam = new SqlParameter(nameof(SerializationKind), SqlDbType.VarChar, 50)
                                                     {
                                                         Direction = ParameterDirection.Output,
                                                     };
                        var serializationFormatParam = new SqlParameter(nameof(SerializationFormat), SqlDbType.VarChar, 50)
                                                     {
                                                         Direction = ParameterDirection.Output,
                                                     };
                        var compressionKindParam = new SqlParameter(nameof(CompressionKind), SqlDbType.VarChar, 50)
                                                   {
                                                       Direction = ParameterDirection.Output,
                                                   };
                        var serializedPayloadParam = new SqlParameter("SerializedPayload", SqlDbType.NVarChar, -1)
                                                     {
                                                         Direction = ParameterDirection.Output,
                                                     };
                        command.Parameters.Add(serializationConfigAssemblyQualifiedNameWithoutVersionParam);
                        command.Parameters.Add(serializationKindParam);
                        command.Parameters.Add(serializationFormatParam);
                        command.Parameters.Add(compressionKindParam);
                        command.Parameters.Add(serializedPayloadParam);

                        command.ExecuteNonQuery();
                        serializationKind = (SerializationKind)Enum.Parse(typeof(SerializationKind), serializationKindParam?.Value?.ToString() ?? throw new InvalidDataException(FormattableString.Invariant($"{nameof(SerializationKind)} from {storedProcedureName} should not be null output for key {operation.Key}.")));
                        serializationFormat = (SerializationFormat)Enum.Parse(typeof(SerializationFormat), serializationFormatParam?.Value?.ToString() ?? throw new InvalidDataException(FormattableString.Invariant($"{nameof(SerializationFormat)} from {storedProcedureName} should not be null output for key {operation.Key}.")));
                        serializationConfigAssemblyQualifiedNameWithoutVersion = serializationConfigAssemblyQualifiedNameWithoutVersionParam.Value?.ToString() ?? throw new InvalidDataException(FormattableString.Invariant($"{serializationConfigAssemblyQualifiedNameWithoutVersionParam.ParameterName} from {storedProcedureName} should not be null output for key {operation.Key}."));
                        compressionKind = (CompressionKind)Enum.Parse(typeof(CompressionKind), compressionKindParam.Value?.ToString() ?? throw new InvalidDataException(FormattableString.Invariant($"{nameof(CompressionKind)} from {storedProcedureName} should not be null output for key {operation.Key}.")));
                        serializedPayload = serializedPayloadParam.Value?.ToString();
                    }
                }

                var describedSerialization = new DescribedSerialization(
                    typeof(TObject).ToRepresentation(),
                    serializedPayload,
                    new SerializationDescription(
                        serializationKind,
                        serializationFormat,
                        Type.GetType(serializationConfigAssemblyQualifiedNameWithoutVersion).ToRepresentation(),
                        compressionKind));
                var result = describedSerialization.DeserializePayloadUsingSpecificFactory<TObject>(
                    this.stream.SerializerFactory,
                    this.stream.CompressorFactory,
                    unregisteredTypeEncounteredStrategy: UnregisteredTypeEncounteredStrategy.Attempt);
                return result;
            }
            else
            {
                throw SqlStreamLocator.BuildInvalidStreamLocatorException(locator.GetType());
            }
        }
    }
}