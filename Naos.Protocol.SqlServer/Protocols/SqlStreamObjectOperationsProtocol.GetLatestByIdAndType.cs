﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SqlStreamObjectOperationsProtocol.GetLatestByIdAndType.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Protocol.SqlServer
{
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.IO;
    using System.Runtime.InteropServices;
    using System.Threading.Tasks;
    using Naos.Protocol.Domain;
    using OBeautifulCode.Assertion.Recipes;
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
    public partial class SqlStreamObjectOperationsProtocol<TId, TObject>
#pragma warning restore CS1710 // XML comment has a duplicate typeparam tag
#pragma warning restore CS1710 // XML comment has a duplicate typeparam tag
    {
        /// <inheritdoc />
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling", Justification = "Expected here.")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2100:Review SQL queries for security vulnerabilities", Justification = "Internally generated and should be safe.")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope", Justification = "Should be disposing correctly.")]
        public TObject Execute(
            GetLatestByIdAndTypeOp<TId, TObject> operation)
        {
            var locator = this.stream.StreamLocatorProtocol.Execute(new GetStreamLocatorByIdOp<TId>(operation.Id));
            if (locator is SqlStreamLocator sqlStreamLocator)
            {
                var serializedObjectId = (operation.Id is string stringId)
                    ? stringId
                    : this.stream.GetDescribedSerializer(sqlStreamLocator).Serializer.SerializeToString(operation.Id);

                var assemblyQualifiedNameWithoutVersion = typeof(TObject).AssemblyQualifiedName;
                var assemblyQualifiedNameWithVersion = typeof(TObject).AssemblyQualifiedName;

                var storedProcOp = StreamSchema.Sprocs.GetLatestByIdAndType.BuildExecuteStoredProcedureOp(
                    this.stream.Name,
                    serializedObjectId,
                    assemblyQualifiedNameWithoutVersion,
                    assemblyQualifiedNameWithVersion,
                    TypeVersionMatchStrategy.Any);

                var sqlProtocol = this.stream.BuildSqlOperationsProtocol(sqlStreamLocator);
                var sprocResult = sqlProtocol.Execute(storedProcOp);

                SerializationKind serializationKind = sprocResult.OutputParameters[nameof(StreamSchema.Sprocs.GetLatestByIdAndType.OutputParamNames.SerializationKind)].GetValue<SerializationKind>();
                SerializationFormat serializationFormat = sprocResult.OutputParameters[nameof(StreamSchema.Sprocs.GetLatestByIdAndType.OutputParamNames.SerializationFormat)].GetValue<SerializationFormat>();
                string serializationConfigAssemblyQualifiedNameWithoutVersion = sprocResult.OutputParameters[nameof(StreamSchema.Sprocs.GetLatestByIdAndType.OutputParamNames.SerializationConfigAssemblyQualifiedNameWithoutVersion)].GetValue<string>();
                CompressionKind compressionKind = sprocResult.OutputParameters[nameof(StreamSchema.Sprocs.GetLatestByIdAndType.OutputParamNames.CompressionKind)].GetValue<CompressionKind>();
                string serializedObjectString = sprocResult.OutputParameters[nameof(StreamSchema.Sprocs.GetLatestByIdAndType.OutputParamNames.SerializedObjectString)].GetValue<string>();
                byte[] serializedObjectBytes = sprocResult.OutputParameters[nameof(StreamSchema.Sprocs.GetLatestByIdAndType.OutputParamNames.SerializedObjectBinary)].GetValue<byte[]>();

                var serializerDescription = new SerializerRepresentation(
                    serializationKind,
                    Type.GetType(serializationConfigAssemblyQualifiedNameWithoutVersion).ToRepresentation(),
                    compressionKind);

                var serializer = this.stream.SerializerFactory.BuildSerializer(
                    serializerDescription);

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
            GetLatestByIdAndTypeOp<TId, TObject> operation)
        {
            throw new NotImplementedException();
        }
    }
}