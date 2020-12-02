// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FailedEvent{TId,TFailureContext}.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Protocol.Domain
{
    using System;
    using System.Collections.Generic;
    using OBeautifulCode.Assertion.Recipes;

    /// <summary>
    /// Event indicating that an operation failed to execute.
    /// </summary>
    /// <typeparam name="TId">The type of identifier of the event.</typeparam>
    /// <typeparam name="TFailureContext">The type of the context object for the failure.</typeparam>
    public partial class FailedEvent<TId, TFailureContext> : EventWithTagsBase<TId>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FailedEvent{TId, TFailureContext}"/> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="timestampUtc">The timestamp in UTC.</param>
        /// <param name="failureContext">The context of the failure.</param>
        /// <param name="tags">The optional tags.</param>
        public FailedEvent(
            TId id,
            DateTime timestampUtc,
            TFailureContext failureContext,
            IReadOnlyDictionary<string, string> tags = null)
        : base(id, timestampUtc, tags)
        {
            failureContext.MustForArg(nameof(failureContext)).NotBeNull();

            this.FailureContext = failureContext;
        }

        /// <summary>
        /// Gets the failure context.
        /// </summary>
        /// <value>The failure context.</value>
        public TFailureContext FailureContext { get; private set; }
    }
}
