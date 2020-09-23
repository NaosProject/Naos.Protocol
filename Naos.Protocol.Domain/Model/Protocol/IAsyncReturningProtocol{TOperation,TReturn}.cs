// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IAsyncReturningProtocol{TOperation,TReturn}.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Protocol.Domain
{
    using System.Threading.Tasks;

    /// <summary>
    /// Executes a <see cref="IReturningOperation{TReturn}"/> asynchronously.
    /// </summary>
    /// <typeparam name="TOperation">The type of the operation.</typeparam>
    /// <typeparam name="TReturn">The type that the operation returns.</typeparam>
    public interface IAsyncReturningProtocol<TOperation, TReturn> : IProtocol<TOperation>
        where TOperation : IReturningOperation<TReturn>
    {
        /// <summary>
        /// Executes the operation.
        /// </summary>
        /// <param name="operation">The operation.</param>
        /// <returns>The result of the operation.</returns>
        Task<TReturn> ExecuteAsync(TOperation operation);
    }
}
