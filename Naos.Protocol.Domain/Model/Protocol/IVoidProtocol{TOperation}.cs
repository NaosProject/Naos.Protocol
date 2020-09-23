// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IVoidProtocol{TOperation}.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Protocol.Domain
{
    /// <summary>
    /// Executes a <see cref="IVoidOperation"/> synchronously.
    /// </summary>
    /// <typeparam name="TOperation">The type of the operation.</typeparam>
    public interface IVoidProtocol<TOperation> : IProtocol<TOperation>
        where TOperation : IVoidOperation
    {
        /// <summary>
        /// Executes the operation.
        /// </summary>
        /// <param name="operation">The operation.</param>
        void Execute(TOperation operation);
    }
}
