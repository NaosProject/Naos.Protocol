// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SqlStreamDataProtocol.Put.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Protocol.SqlServer
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Xml.Linq;
    using Naos.Protocol.Domain;
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
    }
}