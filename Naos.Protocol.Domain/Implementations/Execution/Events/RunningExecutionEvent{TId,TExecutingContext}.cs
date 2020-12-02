// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RunningExecutionEvent{TId,TExecutingContext}.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Protocol.Domain
{
    using System;
    using System.Collections.Generic;
    using OBeautifulCode.Assertion.Recipes;

    /// <summary>
    /// Event indicating that an operation is executing.
    /// </summary>
    /// <typeparam name="TId">The type of identifier of the event.</typeparam>
    /// <typeparam name="TExecutingContext">The type of the operation.</typeparam>
    public partial class RunningExecutionEvent<TId, TExecutingContext> : EventWithTagsBase<TId>
        where TExecutingContext : class, IOperation
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RunningExecutionEvent{TId,TExecutingContext}"/> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="timestampUtc">The timestamp in UTC.</param>
        /// <param name="executingContext">The context of the executing of the operation.</param>
        /// <param name="tags">The optional tags.</param>
        public RunningExecutionEvent(
            TId id,
            DateTime timestampUtc,
            TExecutingContext executingContext,
            IReadOnlyDictionary<string, string> tags = null)
        : base(id, timestampUtc, tags)
        {
            executingContext.MustForArg(nameof(executingContext)).NotBeNull();

            this.ExecutingContext = executingContext;
        }

        /// <summary>
        /// Gets the executing context.
        /// </summary>
        /// <value>The executing context.</value>
        public TExecutingContext ExecutingContext { get; private set; }
    }
}
