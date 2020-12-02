// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CancelExecutionRequestOp{TId,TCancelContext}.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Protocol.Domain
{
    using System;
    using System.Collections.Generic;
    using OBeautifulCode.Assertion.Recipes;
    using OBeautifulCode.Type;

    /// <summary>
    /// Event indicating that an operation needs to be executed.
    /// </summary>
    /// <typeparam name="TId">The type of identifier of the event.</typeparam>
    /// <typeparam name="TCancelContext">The type of the context object for the cancel.</typeparam>
    public partial class CancelExecutionRequestOp<TId, TCancelContext> : VoidOperationBase, IIdentifiableBy<TId>, IHaveTags
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CancelExecutionRequestOp{TId,TOperation}"/> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="cancelContext">The context of the cancellation.</param>
        /// <param name="tags">The optional tags.</param>
        public CancelExecutionRequestOp(
            TId id,
            TCancelContext cancelContext,
            IReadOnlyDictionary<string, string> tags = null)
        {
            cancelContext.MustForArg(nameof(cancelContext)).NotBeNull();

            this.Id = id;
            this.Tags = tags;
            this.CancelContext = cancelContext;
        }

        /// <inheritdoc />
        public TId Id { get; private set; }

        /// <inheritdoc />
        public IReadOnlyDictionary<string, string> Tags { get; private set; }

        /// <summary>
        /// Gets the cancel context.
        /// </summary>
        /// <value>The cancel context.</value>
        public TCancelContext CancelContext { get; private set; }
    }
}
