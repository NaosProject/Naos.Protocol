// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CompletedExecutionEvent{TId}.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Protocol.Domain
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Event indicating that an operation was executed without exception.
    /// </summary>
    /// <typeparam name="TId">The type of identifier of the event.</typeparam>
    public partial class CompletedExecutionEvent<TId> : EventBase<TId>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CompletedExecutionEvent{TId}"/> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="timestampUtc">The timestamp in UTC.</param>
        public CompletedExecutionEvent(
            TId id,
            DateTime timestampUtc)
        : base(id, timestampUtc)
        {
        }
    }
}
