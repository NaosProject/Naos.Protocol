// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SucceededEvent{TId,TSuccessContext}.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Protocol.Domain
{
    using System;
    using System.Collections.Generic;
    using OBeautifulCode.Assertion.Recipes;

    /// <summary>
    /// Event indicating that an operation was executed successfully.
    /// </summary>
    /// <typeparam name="TId">The type of identifier of the event.</typeparam>
    /// <typeparam name="TSuccessContext">The type of the context object for success.</typeparam>
    public partial class SucceededEvent<TId, TSuccessContext> : EventWithTagsBase<TId>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SucceededEvent{TId, TSuccessContext}"/> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="timestampUtc">The timestamp in UTC.</param>
        /// <param name="successContext">The context object of the success.</param>
        /// <param name="tags">The optional tags.</param>
        public SucceededEvent(
            TId id,
            DateTime timestampUtc,
            TSuccessContext successContext,
            IReadOnlyDictionary<string, string> tags = null)
        : base(id, timestampUtc, tags)
        {
            successContext.MustForArg(nameof(successContext)).NotBeNull();

            this.SuccessContext = successContext;
        }

        /// <summary>
        /// Gets the success context.
        /// </summary>
        /// <value>The success context.</value>
        public TSuccessContext SuccessContext { get; private set; }
    }
}
