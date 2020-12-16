// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ExecuteOpRequestedEvent{TId,TOperation}.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Protocol.Domain
{
    using System;
    using OBeautifulCode.Assertion.Recipes;

    /// <summary>
    /// Event with a <typeparamref name="TOperation"/> to execute.
    /// </summary>
    /// <typeparam name="TId">Type of the event identifier.</typeparam>
    /// <typeparam name="TOperation"><see cref="IOperation"/> to execute.</typeparam>
    public partial class ExecuteOpRequestedEvent<TId, TOperation> : EventBase<TId>, IHaveDetails
        where TOperation : IOperation
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ExecuteOpRequestedEvent{TId,TOperation}"/> class.
        /// </summary>
        /// <param name="id">The identifier of the event.</param>
        /// <param name="operation">Operation to execute.</param>
        /// <param name="timestampUtc">The timestamp of the event in UTC.</param>
        /// <param name="details">The optional details about the completion.</param>
        public ExecuteOpRequestedEvent(
            TId id,
            TOperation operation,
            DateTime timestampUtc,
            string details = null)
            : base(id, timestampUtc)
        {
            operation.MustForArg(nameof(operation)).NotBeNull();

            this.Operation = operation;
            this.Details = details;
        }

        /// <summary>
        /// Gets the operation.
        /// </summary>
        /// <value>The operation.</value>
        public TOperation Operation { get; private set; }

        /// <inheritdoc />
        public string Details { get; private set; }
    }
}
