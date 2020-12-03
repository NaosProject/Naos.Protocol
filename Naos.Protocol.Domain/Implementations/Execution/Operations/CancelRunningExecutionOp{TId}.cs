﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CancelRunningExecutionOp{TId}.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Protocol.Domain
{
    using System.Collections.Generic;
    using OBeautifulCode.Assertion.Recipes;
    using OBeautifulCode.Type;

    /// <summary>
    /// Operation to mark a running operation as canceled externally.
    /// </summary>
    /// <typeparam name="TId">The type of identifier of the event.</typeparam>
    public partial class CancelRunningExecutionOp<TId> : VoidOperationBase, IIdentifiableBy<TId>, IHaveTags, IHaveDetails
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CancelRunningExecutionOp{TId}"/> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="details">The details for produced events.</param>
        /// <param name="tags">The optional tags for produced events.</param>
        public CancelRunningExecutionOp(
            TId id,
            string details,
            IReadOnlyDictionary<string, string> tags = null)
        {
            details.MustForArg(nameof(details)).NotBeNullNorWhiteSpace();
            this.Id = id;
            this.Details = details;
            this.Tags = tags;
        }

        /// <inheritdoc />
        public TId Id { get; private set; }

        /// <inheritdoc />
        public IReadOnlyDictionary<string, string> Tags { get; private set; }

        /// <inheritdoc />
        public string Details { get; private set; }
    }
}
