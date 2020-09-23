// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SyncSpecificReturningProtocolBase{TOperation,TReturn}.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Protocol.Domain
{
    using System.Threading.Tasks;

    /// <summary>
    /// Protocol that gives pass-through implementation for the asynchronous execution for <see cref="ISyncAndAsyncReturningProtocol{TOperation,TReturn}"/>.
    /// </summary>
    /// <typeparam name="TOperation">Type of operation.</typeparam>
    /// <typeparam name="TReturn">Type of return.</typeparam>
    public abstract class SyncSpecificReturningProtocolBase<TOperation, TReturn> : ISyncAndAsyncReturningProtocol<TOperation, TReturn>
        where TOperation : IReturningOperation<TReturn>
    {
        /// <inheritdoc />
        public abstract TReturn Execute(
            TOperation operation);

        /// <inheritdoc />
        public async Task<TReturn> ExecuteAsync(
            TOperation operation)
        {
            var syncResult = this.Execute(operation);
            var result = await Task.FromResult(syncResult);
            return result;
        }
    }
}
