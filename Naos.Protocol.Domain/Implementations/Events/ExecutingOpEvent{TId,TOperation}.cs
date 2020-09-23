// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ExecutingOpEvent{TId,TOperation}.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Protocol.Domain
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Event indicating that an operation is being executed.
    /// </summary>
    /// <typeparam name="TId">The type of the identifier.</typeparam>
    /// <typeparam name="TOperation">The type of the operation.</typeparam>
    public partial class ExecutingOpEvent<TId, TOperation> : EventBase<TId>
        where TOperation : class, IOperation
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ExecutingOpEvent{TId, TOperation}"/> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="timestampUtc">The timestamp in UTC.</param>
        /// <param name="executedOperation">The executed operation.</param>
        /// <param name="tags">The optional tags.</param>
        public ExecutingOpEvent(
            TId id,
            DateTime timestampUtc,
            TOperation executedOperation,
            IReadOnlyDictionary<string, string> tags = null)
        : base(id, timestampUtc, tags)
        {
            this.ExecutedOperation = executedOperation ?? throw new ArgumentNullException(nameof(executedOperation));
        }

        /// <summary>
        /// Gets the executed operation.
        /// </summary>
        /// <value>The executed operation.</value>
        public TOperation ExecutedOperation { get; private set; }
    }
}
