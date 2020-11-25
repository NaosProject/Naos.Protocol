// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SyncAndAsyncCompositeVoidProtocol{TVoidOperation}.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Protocol.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using OBeautifulCode.Assertion.Recipes;
    using OBeautifulCode.Representation.System;
    using static System.FormattableString;

    /// <summary>
    /// A protocol for a composite object pattern where a list of protocols will be executed in order any executed operation.
    /// </summary>
    /// <typeparam name="TVoidOperation">Type of operation.</typeparam>
    public partial class SyncAndAsyncCompositeVoidProtocol<TVoidOperation> : ISyncAndAsyncVoidProtocol<TVoidOperation>
    where TVoidOperation : IVoidOperation
    {
        private readonly IReadOnlyList<ISyncAndAsyncVoidProtocol<TVoidOperation>> protocols;

        /// <summary>
        /// Initializes a new instance of the <see cref="SyncAndAsyncCompositeVoidProtocol{TVoidOperation}"/> class.
        /// </summary>
        /// <param name="protocols">The protocols.</param>
        public SyncAndAsyncCompositeVoidProtocol(
            IReadOnlyList<ISyncAndAsyncVoidProtocol<TVoidOperation>> protocols)
        {
            protocols.MustForArg(nameof(protocols)).NotBeNullNorEmptyEnumerableNorContainAnyNulls();
            this.protocols = protocols;
        }

        /// <inheritdoc />
        public void Execute(
            TVoidOperation operation)
        {
            foreach (var protocol in this.protocols)
            {
                protocol.Execute(operation);
            }
        }

        /// <inheritdoc />
        public async Task ExecuteAsync(
            TVoidOperation operation)
        {
            foreach (var protocol in this.protocols)
            {
                await protocol.ExecuteAsync(operation);
            }
        }
    }
}
