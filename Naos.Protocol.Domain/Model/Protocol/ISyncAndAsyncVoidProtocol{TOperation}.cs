// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ISyncAndAsyncVoidProtocol{TOperation}.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Protocol.Domain
{
    /// <summary>
    /// Executes a <see cref="VoidOperationBase"/> both synchronously and asynchronously.
    /// </summary>
    /// <typeparam name="TOperation">The type of the operation.</typeparam>
    public interface ISyncAndAsyncVoidProtocol<TOperation> : IVoidProtocol<TOperation>, IAsyncVoidProtocol<TOperation>
        where TOperation : VoidOperationBase
    {
    }
}