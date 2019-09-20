// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EventCrudOperationHandlers.Put.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Protocol.SqlServer
{
    using System.Threading.Tasks;
    using Naos.Protocol.Domain;

#pragma warning disable CS1710 // XML comment has a duplicate typeparam tag
    /// <summary>
    /// TODO: Starting point for new project.
    /// </summary>
    /// <typeparam name="TObject">Type of payload.</typeparam>
    public partial class CrudOperationHandlers<TObject> : IProtocol<PutOp<TObject>>
        where TObject : class
    {
    }
}
