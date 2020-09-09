// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DiscoverOp.cs" company="Naos Project">
//     Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Protocol.Domain
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    using static System.FormattableString;

    /// <summary>
    /// Implements the <see cref="Naos.Protocol.Domain.ReturningOperationBase{TReturn}" /> to discover something using some input.
    /// </summary>
    /// <typeparam name="TObject">Type of data being written.</typeparam>
    /// <typeparam name="TReturn">The type of the return.</typeparam>
    /// <seealso cref="Naos.Protocol.Domain.ReturningOperationBase{TReturn}" />
    [SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Discover", Justification = "Name/Spelling is correct.")]
    public partial class DiscoverOp<TObject, TReturn> : ReturningOperationBase<TReturn>
        where TObject : class
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DiscoverOp{TObject, TReturn}"/> class.
        /// </summary>
        /// <param name="operationId">The operation ID.</param>
        public DiscoverOp(
            TObject operationId)
        {
            this.OperationId = operationId ?? throw new ArgumentNullException(nameof(operationId));
        }

        /// <summary>
        /// Gets or sets the operation ID.
        /// </summary>
        /// <value>The operation ID.</value>
        public TObject OperationId { get; set; }
    }
}
