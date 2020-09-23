// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AsyncSpecificReturningProtocolBase{TOperation,TReturn}.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Protocol.Domain
{
    using System.Threading.Tasks;
    using Naos.Recipes.RunWithRetry;

    /// <summary>
    /// Protocol that gives pass-through implementation for the synchronous execution for <see cref="ISyncAndAsyncReturningProtocol{TOperation,TReturn}"/>.
    /// </summary>
    /// <typeparam name="TOperation">Type of operation.</typeparam>
    /// <typeparam name="TReturn">Type of return.</typeparam>
    public abstract class AsyncSpecificReturningProtocolBase<TOperation, TReturn> : ISyncAndAsyncReturningProtocol<TOperation, TReturn>
        where TOperation : IReturningOperation<TReturn>
    {
        /// <inheritdoc />
        public TReturn Execute(
            TOperation operation)
        {
            var task = this.ExecuteAsync(operation);
            var result = Run.TaskUntilCompletion(task);
            return result;
        }

        /// <inheritdoc />
        public abstract Task<TReturn> ExecuteAsync(
            TOperation operation);
    }
}
