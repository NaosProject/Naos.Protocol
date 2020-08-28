// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IAsyncVoidProtocol{TOperation}.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Protocol.Domain
{
    using System.Threading.Tasks;

    /// <summary>
    /// Executes a <see cref="VoidOperationBase"/> asynchronously.
    /// </summary>
    /// <typeparam name="TOperation">The type of the operation.</typeparam>
    public interface IAsyncVoidProtocol<TOperation> : IProtocol<TOperation>
        where TOperation : VoidOperationBase
    {
        /// <summary>
        /// Executes the operation.
        /// </summary>
        /// <param name="operation">The operation.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task ExecuteAsync(TOperation operation);
    }
}