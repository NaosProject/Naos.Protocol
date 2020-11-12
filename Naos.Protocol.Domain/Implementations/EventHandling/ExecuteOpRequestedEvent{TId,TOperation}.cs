// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ExecuteOpRequestedEvent{TId,TOperation}.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Protocol.Domain
{
    using System;
    using System.Collections.Generic;
    using OBeautifulCode.Type;

    /// <summary>
    /// Event indicating that an operation needs to be executed.
    /// </summary>
    /// <typeparam name="TId">The type of identifier of the event.</typeparam>
    /// <typeparam name="TOperation">The type of the operation.</typeparam>
    public partial class ExecuteOpRequestedEvent<TId, TOperation> : EventBase<TId>, IHaveTags
        where TOperation : class, IOperation
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ExecuteOpRequestedEvent{TId,TOperation}"/> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="timestampUtc">The timestamp in UTC.</param>
        /// <param name="operationToExecute">The operation to execute.</param>
        /// <param name="tags">The optional tags.</param>
        public ExecuteOpRequestedEvent(
            TId id,
            DateTime timestampUtc,
            TOperation operationToExecute,
            IReadOnlyDictionary<string, string> tags = null)
        : base(id, timestampUtc)
        {
            this.OperationToExecute = operationToExecute ?? throw new ArgumentNullException(nameof(operationToExecute));
            this.Tags = tags;
        }

        /// <summary>
        /// Gets the executed operation.
        /// </summary>
        /// <value>The executed operation.</value>
        public TOperation OperationToExecute { get; private set; }

        /// <inheritdoc />
        public IReadOnlyDictionary<string, string> Tags { get; private set; }
    }
}
