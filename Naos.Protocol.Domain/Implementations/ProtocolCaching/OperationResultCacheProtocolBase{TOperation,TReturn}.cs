// --------------------------------------------------------------------------------------------------------------------
// <copyright file="OperationResultCacheProtocolBase{TOperation,TReturn}.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Protocol.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using OBeautifulCode.Type;

    /// <summary>
    /// Base class that implements cache management which can be extended to include specific other protocols.
    /// </summary>
    /// <typeparam name="TOperation">Type of the operation.</typeparam>
    /// <typeparam name="TReturn">Type of return type of the operation being cached.</typeparam>
    public abstract class OperationResultCacheProtocolBase<TOperation, TReturn> :
        ISyncAndAsyncVoidProtocol<ClearCacheOp>,
        ISyncAndAsyncReturningProtocol<GetOrAddCachedItemOp<TOperation, TReturn>, CacheResult<TOperation, TReturn>>,
        ISyncAndAsyncReturningProtocol<GetCacheStatusOp, CacheStatusResult>
    where TOperation : IReturningOperation<TReturn>
    {
        private readonly object syncCache = new object();
        private readonly IDictionary<TOperation, CacheResult<TOperation, TReturn>> operationToCacheResultMap = new Dictionary<TOperation, CacheResult<TOperation, TReturn>>();

        /// <inheritdoc />
        public void Execute(
            ClearCacheOp operation)
        {
            lock (this.syncCache)
            {
                this.operationToCacheResultMap.Clear();
            }
        }

        /// <inheritdoc />
        public async Task ExecuteAsync(
            ClearCacheOp operation)
        {
            await Task.Run(
                () =>
                {
                    /* no-op for await */
                });

            this.Execute(operation);
        }

        /// <inheritdoc />
        public CacheResult<TOperation, TReturn> Execute(
            GetOrAddCachedItemOp<TOperation, TReturn> operation)
        {
            lock (this.syncCache)
            {
                var found = this.operationToCacheResultMap.TryGetValue(operation.Operation, out var result);
                if (found)
                {
                    return result;
                }
                else
                {
                    var newResult = operation.BackingProtocol.Execute(operation.Operation);
                    var newCacheResult = new CacheResult<TOperation, TReturn>(operation.Operation, newResult, DateTime.UtcNow);
                    this.operationToCacheResultMap.Add(operation.Operation, newCacheResult);
                    return newCacheResult;
                }
            }
        }

        /// <inheritdoc />
        public async Task<CacheResult<TOperation, TReturn>> ExecuteAsync(
            GetOrAddCachedItemOp<TOperation, TReturn> operation)
        {
            var syncResult = this.Execute(operation);
            var result = await Task.FromResult(syncResult);
            return result;
        }

        /// <inheritdoc />
        public CacheStatusResult Execute(
            GetCacheStatusOp operation)
        {
            lock (this.syncCache)
            {
                UtcDateTimeRangeInclusive dateRangeOfCachedObjectsUtc = null;
                if (this.operationToCacheResultMap.Any())
                {
                    var minFreshness = this.operationToCacheResultMap.Values.Min(_ => _.FreshnessInUtc);
                    var maxFreshness = this.operationToCacheResultMap.Values.Max(_ => _.FreshnessInUtc);
                    dateRangeOfCachedObjectsUtc = new UtcDateTimeRangeInclusive(minFreshness, maxFreshness);
                }

                var result = new CacheStatusResult(this.operationToCacheResultMap.Count, dateRangeOfCachedObjectsUtc);
                return result;
            }
        }

        /// <inheritdoc />
        public async Task<CacheStatusResult> ExecuteAsync(
            GetCacheStatusOp operation)
        {
            var syncResult = this.Execute(operation);
            var result = await Task.FromResult(syncResult);
            return result;
        }
    }
}
