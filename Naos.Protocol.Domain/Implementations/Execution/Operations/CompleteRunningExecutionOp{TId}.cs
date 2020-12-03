// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CompleteRunningExecutionOp{TId}.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Protocol.Domain
{
    using System.Collections.Generic;
    using OBeautifulCode.Assertion.Recipes;
    using OBeautifulCode.Type;

    /// <summary>
    /// Operation to mark a running operation as completed.
    /// </summary>
    /// <typeparam name="TId">The type of identifier of the event.</typeparam>
    public partial class CompleteRunningExecutionOp<TId> : VoidOperationBase, IIdentifiableBy<TId>, IHaveTags
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CompleteRunningExecutionOp{TId}"/> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="tags">The optional tags for produced events.</param>
        public CompleteRunningExecutionOp(
            TId id,
            IReadOnlyDictionary<string, string> tags = null)
        {
            this.Id = id;
            this.Tags = tags;
        }

        /// <inheritdoc />
        public TId Id { get; private set; }

        /// <inheritdoc />
        public IReadOnlyDictionary<string, string> Tags { get; private set; }
    }
}
