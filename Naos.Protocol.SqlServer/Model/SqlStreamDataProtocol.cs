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
    using OBeautifulCode.Serialization;
    using OBeautifulCode.Type.Recipes;
    using SerializationFormat = System.Data.SerializationFormat;

    /// <summary>
    /// Class SqlStreamDataProtocol.
    /// </summary>
    /// <typeparam name="TKey">The type of the key.</typeparam>
    /// <typeparam name="TObject">The type of the object.</typeparam>
    public class SqlStreamDataProtocol<TKey, TObject> : IVoidProtocol<PutOp<TObject>>,
                                                        IReturningProtocol<GetKeyFromObjectOp<TKey, TObject>, TKey>,
                                                        IReturningProtocol<GetTagsFromObjectOp<TObject>, IReadOnlyDictionary<string, string>>
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
                    tagsXml.Append(FormattableString.Invariant($"Name=\"{tag.Key}\""));
                    tagsXml.Append(FormattableString.Invariant($"Value=\"{tag.Value}\""));
                    tagsXml.Append("/>");
                }

                tagsXml.Append("</Tags>");

                var serializedPayload = describedSerializer.Serializer.SerializeToString(operation.Payload);
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
    }
}