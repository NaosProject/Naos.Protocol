// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DiscoverOp.cs" company="Naos Project">
//     Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Protocol.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using OBeautifulCode.Representation;
    using OBeautifulCode.Type;
    using static System.FormattableString;

    /// <summary>
    /// Implements the <see cref="Naos.Protocol.Domain.ReturningOperationBase{TReturn}" /> to discover something using some input.
    /// </summary>
    /// <typeparam name="TObject">Type of data being written.</typeparam>
    /// <typeparam name="TReturn">The type of the t return.</typeparam>
    /// <seealso cref="Naos.Protocol.Domain.ReturningOperationBase{TReturn}" />
    [SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Discover", Justification = "Name/Spelling is correct.")]
    public class DiscoverOp<TObject, TReturn> : ReturningOperationBase<TReturn>
        where TObject : class
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DiscoverOp{TObject, TReturn}"/> class.
        /// </summary>
        /// <param name="operationKey">The operation key.</param>
        public DiscoverOp(
            TObject operationKey)
        {
            this.OperationKey = operationKey ?? throw new ArgumentNullException(nameof(operationKey));
        }

        /// <summary>
        /// Gets or sets the operation key.
        /// </summary>
        /// <value>The operation key.</value>
        public TObject OperationKey { get; set; }
    }
}