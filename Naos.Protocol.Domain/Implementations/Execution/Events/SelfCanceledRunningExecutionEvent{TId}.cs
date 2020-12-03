// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SelfCanceledRunningExecutionEvent{TId}.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Protocol.Domain
{
    using System;
    using System.Collections.Generic;
    using OBeautifulCode.Assertion.Recipes;

    /// <summary>
    /// Event indicating that a protocol canceled it's own execution of the operation.
    /// </summary>
    /// <typeparam name="TId">The type of identifier of the event.</typeparam>
    public partial class SelfCanceledRunningExecutionEvent<TId> : EventBase<TId>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SelfCanceledRunningExecutionEvent{TId}"/> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="details">The details about the cancellation.</param>
        /// <param name="timestampUtc">The timestamp in UTC.</param>
        public SelfCanceledRunningExecutionEvent(
            TId id,
            string details,
            DateTime timestampUtc)
        : base(id, timestampUtc)
        {
            details.MustForArg(nameof(details)).NotBeNullNorWhiteSpace();

            this.Details = details;
        }

        /// <summary>
        /// Gets the details of the cancellation.
        /// </summary>
        public string Details { get; private set; }
    }
}
