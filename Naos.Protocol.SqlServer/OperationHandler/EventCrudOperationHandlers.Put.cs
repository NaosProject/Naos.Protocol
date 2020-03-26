// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EventCrudOperationHandlers.Put.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Protocol.SqlServer
{
    using Naos.Protocol.Domain;

#pragma warning disable CS1710 // XML comment has a duplicate typeparam tag
    /// <summary>
    /// TODO: Starting point for new project.
    /// </summary>
    /// <typeparam name="TKey">Type of key.</typeparam>
    /// <typeparam name="TObject">Type of payload.</typeparam>
    public partial class CrudOperationHandlers<TKey, TObject> : IProtocol<PutOp<TObject>>
        where TObject : class
    {
    }
}
