// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AsyncSpecificVoidProtocolBase{TOperation}.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Protocol.Domain
{
    using System.Threading.Tasks;
    using Naos.Recipes.RunWithRetry;

    /// <summary>
    /// Protocol that gives pass-through implementation for the synchronous execution for <see cref="ISyncAndAsyncVoidProtocol{TOperation}"/>.
    /// </summary>
    /// <typeparam name="TOperation">Type of operation.</typeparam>
    public abstract class AsyncSpecificVoidProtocolBase<TOperation> : ISyncAndAsyncVoidProtocol<TOperation>
        where TOperation : IVoidOperation
    {
        /// <inheritdoc />
        public void Execute(
            TOperation operation)
        {
            var task = this.ExecuteAsync(operation);
            Run.TaskUntilCompletion(task);
        }

        /// <inheritdoc />
        public abstract Task ExecuteAsync(
            TOperation operation);
    }
}
