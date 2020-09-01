// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SyncSpecificVoidProtocolBase{TOperation}.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Protocol.Domain
{
    using System.Threading.Tasks;

    /// <summary>
    /// Protocol that gives pass-through implementation for the asynchronous execution for <see cref="ISyncAndAsyncVoidProtocol{TOperation}"/>.
    /// </summary>
    /// <typeparam name="TOperation">Type of operation.</typeparam>
    public abstract class SyncSpecificVoidProtocolBase<TOperation> : ISyncAndAsyncVoidProtocol<TOperation>
        where TOperation : VoidOperationBase
    {
        /// <inheritdoc />
        public abstract void Execute(
            TOperation operation);

        /// <inheritdoc />
        public async Task ExecuteAsync(
            TOperation operation)
        {
            await Task.Run(
                () =>
                {
                    /* no-op for await */
                });

            this.Execute(operation);
        }
    }
}
