// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ExecuteOpRequested{TOperation}.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Reporting.Domain
{
    using System;
    using Naos.Protocol.Domain;
    using OBeautifulCode.Assertion.Recipes;

    /// <summary>
    /// Event with a <typeparamref name="TOperation"/> to execute.
    /// </summary>
    /// <typeparam name="TOperation"><see cref="IOperation"/> to execute.</typeparam>
    public partial class ExecuteOpRequestedEvent<TOperation> : EventBaseBase, IHaveDetails
        where TOperation : IOperation
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ExecuteOpRequestedEvent{TOperation}"/> class.
        /// </summary>
        /// <param name="operation">Operation to execute.</param>
        /// <param name="timestampUtc">The timestamp of the event in UTC.</param>
        /// <param name="details">The optional details about the completion.</param>
        public ExecuteOpRequestedEvent(
            TOperation operation,
            DateTime timestampUtc,
            string details = null)
            : base(timestampUtc)
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
