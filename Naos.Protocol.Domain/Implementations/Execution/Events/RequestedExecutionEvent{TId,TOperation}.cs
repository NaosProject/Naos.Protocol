// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RequestedExecutionEvent{TId,TOperation}.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Protocol.Domain
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Event indicating that an operation needs to be executed.
    /// </summary>
    /// <typeparam name="TId">The type of identifier of the event.</typeparam>
    /// <typeparam name="TOperation">The type of the operation.</typeparam>
    public partial class RequestedExecutionEvent<TId, TOperation> : EventWithTagsBase<TId>
        where TOperation : class, IOperation
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RequestedExecutionEvent{TId,TOperation}"/> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="timestampUtc">The timestamp in UTC.</param>
        /// <param name="operationToExecute">The operation to execute.</param>
        /// <param name="tags">The optional tags.</param>
        public RequestedExecutionEvent(
            TId id,
            DateTime timestampUtc,
            TOperation operationToExecute,
            IReadOnlyDictionary<string, string> tags = null)
        : base(id, timestampUtc, tags)
        {
            this.OperationToExecute = operationToExecute ?? throw new ArgumentNullException(nameof(operationToExecute));
        }

        /// <summary>
        /// Gets the executed operation.
        /// </summary>
        /// <value>The executed operation.</value>
        public TOperation OperationToExecute { get; private set; }
    }
}
