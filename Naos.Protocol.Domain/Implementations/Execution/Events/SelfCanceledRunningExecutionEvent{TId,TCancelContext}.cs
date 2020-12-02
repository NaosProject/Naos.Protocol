// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SelfCanceledRunningExecutionEvent{TId,TCancelContext}.cs" company="Naos Project">
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
    /// <typeparam name="TCancelContext">The type of the context object for the cancel.</typeparam>
    public partial class SelfCanceledRunningExecutionEvent<TId, TCancelContext> : EventWithTagsBase<TId>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SelfCanceledRunningExecutionEvent{TId,TCancelContext}"/> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="timestampUtc">The timestamp in UTC.</param>
        /// <param name="cancelContext">The context of the cancellation.</param>
        /// <param name="tags">The optional tags.</param>
        public SelfCanceledRunningExecutionEvent(
            TId id,
            DateTime timestampUtc,
            TCancelContext cancelContext,
            IReadOnlyDictionary<string, string> tags = null)
        : base(id, timestampUtc, tags)
        {
            cancelContext.MustForArg(nameof(cancelContext)).NotBeNull();

            this.CancelContext = cancelContext;
        }

        /// <summary>
        /// Gets the cancel context.
        /// </summary>
        /// <value>The cancel context.</value>
        public TCancelContext CancelContext { get; private set; }
    }
}
