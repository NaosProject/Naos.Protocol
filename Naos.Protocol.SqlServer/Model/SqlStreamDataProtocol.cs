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
    using System.Threading.Tasks;
    using System.Xml.Linq;
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
    /// <typeparam name="TId">The type of the key.</typeparam>
    /// <typeparam name="TObject">The type of the object.</typeparam>
    public class SqlStreamDataProtocol<TId, TObject> : IProtocolStreamObjectOperations<TId, TObject>
    {
        private readonly SqlStream<TId> stream;
        private readonly IReturningProtocol<GetIdFromObjectOp<TId, TObject>, TId> getIdFromObjectProtocol;
        private readonly IReturningProtocol<GetTagsFromObjectOp<TObject>, IReadOnlyDictionary<string, string>> getTagsFromObjectProtocol;

        /// <summary>
        /// Initializes a new instance of the <see cref="SqlStreamDataProtocol{TKey, TObject}"/> class.
        /// </summary>
        /// <param name="stream">The stream to operation against.</param>
        public SqlStreamDataProtocol(
            SqlStream<TId> stream)
        {
            stream.MustForArg(nameof(stream)).NotBeNull();

            this.stream = stream;
            this.getIdFromObjectProtocol = this.stream.BuildGetIdFromObjectProtocol<TObject>();
            this.getTagsFromObjectProtocol = this.stream.BuildGetTagsFromObjectProtocol<TObject>();
        }

        /// <inheritdoc />
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope", Justification = "Should dispose correctly.")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2100:Review SQL queries for security vulnerabilities", Justification = "Name is built internally.")]
        public void Execute(PutOp<TObject> operation)
        {
            var id = this.getIdFromObjectProtocol.Execute(new GetIdFromObjectOp<TId, TObject>(operation.ObjectToPut));
            var locator = this.stream.StreamLocatorProtocol.Execute(new GetStreamLocatorByIdOp<TId>(id));
            if (locator is SqlStreamLocator sqlStreamLocator)
            {
                var objectType = operation.ObjectToPut?.GetType() ?? typeof(TObject);
                var objectTypeWithoutVersion = objectType.AssemblyQualifiedName;
                var objectTypeWithVersion = objectType.AssemblyQualifiedName;
                var describedSerializer = this.stream.GetDescribedSerializer(sqlStreamLocator);
                var tagsXml = this.GetTagsXmlString(operation);

                var serializedObjectString = describedSerializer.SerializerDescription.SerializationFormat != SerializationFormat.String
                    ? null
                    : describedSerializer.Serializer.SerializeToString(operation.ObjectToPut);
                var serializedObjectBinary = describedSerializer.SerializerDescription.SerializationFormat != SerializationFormat.Binary
                    ? null
                    : describedSerializer.Serializer.SerializeToBytes(operation.ObjectToPut);

                var serializedObjectId = (id is string stringKey) ? stringKey : describedSerializer.Serializer.SerializeToString(id);
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
                        command.Parameters.Add(new SqlParameter("SerializedObjectId", serializedObjectId));
                        command.Parameters.Add(
                            new SqlParameter("SerializedObjectString", SqlDbType.NVarChar, -1)
                            {
                                IsNullable = true,
                                Value = serializedObjectString ?? string.Empty, // the parameter won't accept null here for some reason...
                            });
                        command.Parameters.Add(
                            new SqlParameter("SerializedObjectBinary", SqlDbType.VarBinary, -1)
                            {
                                IsNullable = true,
                                Value = serializedObjectBinary ?? new byte[0], // the parameter won't accept null here for some reason...
                            });
                        command.Parameters.Add(new SqlParameter("Tags", tagsXml ?? string.Empty));

                        command.ExecuteNonQuery();
                    }
                }
            }
            else
            {
                throw SqlStreamLocator.BuildInvalidStreamLocatorException(locator.GetType());
            }
        }

        private string GetTagsXmlString(
            PutOp<TObject> operation)
        {
            var tags = this.getTagsFromObjectProtocol.Execute(new GetTagsFromObjectOp<TObject>(operation.ObjectToPut));
            if (!tags.Any())
            {
                return null;
            }

            var tagsXmlBuilder = new StringBuilder();
            tagsXmlBuilder.Append("<Tags>");
            foreach (var tag in tags ?? new Dictionary<string, string>())
            {
                var escapedKey = new XElement("ForEscapingOnly", tag.Key).LastNode.ToString();
                var escapedValue = tag.Value == null ? null : new XElement("ForEscapingOnly", tag.Value).LastNode.ToString();
                tagsXmlBuilder.Append("<Tag ");
                if (escapedValue == null)
                {
                    tagsXmlBuilder.Append(FormattableString.Invariant($"Key=\"{escapedKey}\" Value=null"));
                }
                else
                {
                    tagsXmlBuilder.Append(FormattableString.Invariant($"Key=\"{escapedKey}\" Value=\"{escapedValue}\""));
                }

                tagsXmlBuilder.Append("/>");
            }

            tagsXmlBuilder.Append("</Tags>");
            var result = tagsXmlBuilder.ToString();
            return result;
        }

        /// <inheritdoc />
        public Task ExecuteAsync(
            PutOp<TObject> operation)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling", Justification = "Expected here.")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2100:Review SQL queries for security vulnerabilities", Justification = "Internally generated and should be safe.")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope", Justification = "Should be disposing correctly.")]
        public TObject Execute(
            GetLatestByIdOp<TId, TObject> operation)
        {
            var locator = this.stream.StreamLocatorProtocol.Execute(new GetStreamLocatorByIdOp<TId>(operation.Id));
            if (locator is SqlStreamLocator sqlStreamLocator)
            {
                var serializedObjectId = (operation.Id is string stringKey)
                    ? stringKey
                    : this.stream.GetDescribedSerializer(sqlStreamLocator).Serializer.SerializeToString(operation.Id);

                var storedProcedureName = StreamSchema.BuildGetLatestByKeySprocName(this.stream.Name);

                SerializationKind serializationKind;
                SerializationFormat serializationFormat;
                string serializationConfigAssemblyQualifiedNameWithoutVersion;
                CompressionKind compressionKind;
                string serializedObjectString;
                byte[] serializedObjectBytes;

                using (var connection = sqlStreamLocator.OpenSqlConnection())
                {
                    using (var command = new SqlCommand(storedProcedureName, connection)
                    {
                        CommandType = CommandType.StoredProcedure,
                    })
                    {
                        command.Parameters.Add(new SqlParameter("SerializedObjectId", serializedObjectId));
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
                        var serializedObjectStringParam = new SqlParameter("SerializedObjectString", SqlDbType.NVarChar, -1)
                                                     {
                                                         Direction = ParameterDirection.Output,
                                                     };
                        var serializedObjectBytesParam = new SqlParameter("SerializedObjectBinary", SqlDbType.VarBinary, -1)
                                                     {
                                                         Direction = ParameterDirection.Output,
                                                     };
                        command.Parameters.Add(serializationConfigAssemblyQualifiedNameWithoutVersionParam);
                        command.Parameters.Add(serializationKindParam);
                        command.Parameters.Add(serializationFormatParam);
                        command.Parameters.Add(compressionKindParam);
                        command.Parameters.Add(serializedObjectStringParam);
                        command.Parameters.Add(serializedObjectBytesParam);

                        command.ExecuteNonQuery();
                        serializationKind = (SerializationKind)Enum.Parse(typeof(SerializationKind), serializationKindParam?.Value?.ToString() ?? throw new InvalidDataException(FormattableString.Invariant($"{nameof(SerializationKind)} from {storedProcedureName} should not be null output for key {operation.Id}.")));
                        serializationFormat = (SerializationFormat)Enum.Parse(typeof(SerializationFormat), serializationFormatParam?.Value?.ToString() ?? throw new InvalidDataException(FormattableString.Invariant($"{nameof(SerializationFormat)} from {storedProcedureName} should not be null output for key {operation.Id}.")));
                        serializationConfigAssemblyQualifiedNameWithoutVersion = serializationConfigAssemblyQualifiedNameWithoutVersionParam.Value?.ToString() ?? throw new InvalidDataException(FormattableString.Invariant($"{serializationConfigAssemblyQualifiedNameWithoutVersionParam.ParameterName} from {storedProcedureName} should not be null output for key {operation.Id}."));
                        compressionKind = (CompressionKind)Enum.Parse(typeof(CompressionKind), compressionKindParam.Value?.ToString() ?? throw new InvalidDataException(FormattableString.Invariant($"{nameof(CompressionKind)} from {storedProcedureName} should not be null output for key {operation.Id}.")));
                        serializedObjectString = serializedObjectStringParam.Value?.ToString();
                        serializedObjectBytes = (byte[])serializedObjectBytesParam.Value;
                    }
                }

                var serializerDescription = new SerializationDescription(
                    serializationKind,
                    serializationFormat,
                    Type.GetType(serializationConfigAssemblyQualifiedNameWithoutVersion).ToRepresentation(),
                    compressionKind);

                var serializer = this.stream.SerializerFactory.BuildSerializer(
                    serializerDescription,
                    unregisteredTypeEncounteredStrategy: UnregisteredTypeEncounteredStrategy.Attempt);

                TObject result = default(TObject);
                if (serializationFormat == SerializationFormat.String)
                {
                    result = serializer.Deserialize<TObject>(serializedObjectString);
                }
                else if (serializationFormat == SerializationFormat.Binary)
                {
                    result = serializer.Deserialize<TObject>(serializedObjectBytes);
                }

                return result;
            }
            else
            {
                throw SqlStreamLocator.BuildInvalidStreamLocatorException(locator.GetType());
            }
        }

        /// <inheritdoc />
        public Task<TObject> ExecuteAsync(
            GetLatestByIdOp<TId, TObject> operation)
        {
            throw new NotImplementedException();
        }
    }
}