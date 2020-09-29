// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MemoryCachingReturningProtocol{TOperation,TReturn}.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Protocol.Domain
{
    using System;
    using System.Threading.Tasks;

    /// <summary>
    /// Protocol that will wrap and cache the provided protocol using <see cref="OperationResultCacheProtocolBase{TOperation, TReturn}"/>.
    /// </summary>
    /// <typeparam name="TOperation">Type of the operation.</typeparam>
    /// <typeparam name="TReturn">Type of return type of the operation being cached.</typeparam>
    public class MemoryCachingReturningProtocol<TOperation, TReturn>
        : OperationResultCacheProtocolBase<TOperation, TReturn>,
          ISyncAndAsyncReturningProtocol<TOperation, TReturn>
        where TOperation : IReturningOperation<TReturn>
    {
        private readonly ISyncAndAsyncReturningProtocol<TOperation, TReturn> protocol;

        /// <summary>
        /// Initializes a new instance of the <see cref="MemoryCachingReturningProtocol{TOperation,TReturn}"/> class.
        /// </summary>
        /// <param name="protocol">The protocol to cache.</param>
        public MemoryCachingReturningProtocol(
            ISyncAndAsyncReturningProtocol<TOperation, TReturn> protocol)
        {
            this.protocol = protocol ?? throw new ArgumentNullException(nameof(protocol));
        }

        /// <inheritdoc />
        public TReturn Execute(TOperation operation)
        {
            var getOrAddOp = new GetOrAddCachedItemOp<TOperation, TReturn>(operation, this.protocol);
            var cacheResult = this.Execute(getOrAddOp);
            return cacheResult.CachedObject;
        }

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
