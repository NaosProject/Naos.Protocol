// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EventCrudOperationHandlers.CreateStream.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Protocol.SqlServer
{
    using Naos.Protocol.Domain;
    using OBeautifulCode.Assertion.Recipes;

#pragma warning disable CS1710 // XML comment has a duplicate typeparam tag
    /// <summary>
    /// TODO: Starting point for new project.
    /// </summary>
    /// <typeparam name="TKey">Type of key.</typeparam>
    /// <typeparam name="TObject">Type of payload.</typeparam>
    public partial class CrudOperationHandlers<TKey, TObject> : IVoidProtocol<CreateOp<SqlStream<TKey>>>
    {
        /// <inheritdoc />
        public void Execute(
            CreateOp<SqlStream<TKey>> operation)
        {
            operation.MustForArg(nameof(operation)).NotBeNull();

            // main table
            //   guid id
            //   foreign key to Kind tinyint serializationKind
            //   foreign key to Type int configType
            //   foreign key to Type int payloadType
            //   varchar(max) serializedPayload
            //   datetime createdDateTimeUtc
            // type table
            //   int id
            //   varchar(
            // tag table
        }
    }
}
