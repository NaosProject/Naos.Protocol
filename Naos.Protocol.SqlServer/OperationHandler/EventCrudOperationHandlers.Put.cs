// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CrudOperationHandlers.Put.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Protocol.SqlServer
{
    using System.Threading.Tasks;
    using Naos.Protocol.Domain;

    /// <summary>
    /// TODO: Starting point for new project.
    /// </summary>
    /// <typeparam name="TObject">Type of payload.</typeparam>
    public partial class CrudOperationHandlers<TObject> : IProtocol<PutOp<TObject>>
        where TObject : class
    {
    }
}
