// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SqlStreamDataProtocol.GetLatestById.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Protocol.SqlServer
{
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.IO;
    using System.Threading.Tasks;
    using Naos.Protocol.Domain;
    using OBeautifulCode.Compression;
    using OBeautifulCode.Representation.System;
    using OBeautifulCode.Serialization;
    using SerializationFormat = OBeautifulCode.Serialization.SerializationFormat;

#pragma warning disable CS1710 // XML comment has a duplicate typeparam tag
#pragma warning disable CS1710 // XML comment has a duplicate typeparam tag
    /// <summary>
    /// SQL Server implementation of <see cref="IProtocolFactoryStreamObjectOperations{TId}" />.
    /// </summary>
    /// <typeparam name="TId">The type of the key.</typeparam>
    /// <typeparam name="TObject">The type of the object.</typeparam>
    public partial class SqlStreamDataProtocol<TId, TObject>
#pragma warning restore CS1710 // XML comment has a duplicate typeparam tag
#pragma warning restore CS1710 // XML comment has a duplicate typeparam tag
    {
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