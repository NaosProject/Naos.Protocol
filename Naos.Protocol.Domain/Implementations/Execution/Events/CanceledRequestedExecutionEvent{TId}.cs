// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CanceledRequestedExecutionEvent{TId}.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Protocol.Domain
{
    using System;
    using System.Collections.Generic;
    using OBeautifulCode.Assertion.Recipes;

    /// <summary>
    /// Event indicating that an operation CancelExecution to execute.
    /// </summary>
    /// <typeparam name="TId">The type of identifier of the event.</typeparam>
    public partial class CanceledRequestedExecutionEvent<TId> : EventBase<TId>, IHaveDetails
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CanceledRequestedExecutionEvent{TId}"/> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="details">The details about the cancellation.</param>
        /// <param name="timestampUtc">The timestamp in UTC.</param>
        public CanceledRequestedExecutionEvent(
            TId id,
            string details,
            DateTime timestampUtc)
        : base(id, timestampUtc)
        {
            details.MustForArg(nameof(details)).NotBeNullNorWhiteSpace();

            this.Details = details;
        }

        /// <inheritdoc />
        public string Details { get; private set; }
    }
}
