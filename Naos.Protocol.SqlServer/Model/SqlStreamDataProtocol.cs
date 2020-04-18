// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SqlStreamDataProtocol.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Protocol.SqlServer
{
    using System;
    using System.Configuration;
    using System.Data;
    using System.Data.SqlClient;
    using Dapper;
    using Naos.Protocol.Domain;
    using OBeautifulCode.Assertion.Recipes;
    using OBeautifulCode.Serialization;

    /// <summary>
    /// Class SqlStreamDataProtocol.
    /// </summary>
    /// <typeparam name="TKey">The type of the key.</typeparam>
    /// <typeparam name="TObject">The type of the object.</typeparam>
    public class SqlStreamDataProtocol<TKey, TObject> : IVoidProtocol<PutOp<TObject>>,
                                                        IReturningProtocol<GetKeyFromObjectOp<TKey, TObject>, TKey>
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
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2100:Review SQL queries for security vulnerabilities", Justification = "Name is built internally.")]
        public void Execute(PutOp<TObject> operation)
        {
            var serializer = this.stream.GetStringSerializer();
            var key = this.Execute(new GetKeyFromObjectOp<TKey, TObject>(operation.Payload));
            var locator = this.stream.Execute(new GetStreamLocatorByKeyOp<TKey>(key));
            if (locator is SqlStreamLocator sqlStreamLocator)
            {
                var serializedPayload = serializer.SerializeToString(operation.Payload);
                using (var connection = sqlStreamLocator.OpenSqlConnection())
                {
                    var putSprocName = StreamSchema.BuildPutSprocName(this.stream.Name);
                    using (var command = new SqlCommand(putSprocName))
                    {
                        command.Parameters.Add(new SqlParameter("serializedPayload", serializedPayload));
                        command.Connection = connection;
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
            throw new NotImplementedException();
        }
    }
}