// --------------------------------------------------------------------------------------------------------------------
// <copyright file="OperationSequence.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Protocol.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using OBeautifulCode.Validation.Recipes;

    /// <summary>
    /// Abstract base of an operation.
    /// </summary>
    public abstract class OperationSequence : OperationBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OperationSequence"/> class.
        /// </summary>
        /// <param name="operationPrototypes">Operation builders for the sequence.</param>
        protected OperationSequence(IReadOnlyCollection<OperationPrototype> operationPrototypes)
        {
            this.OperationPrototypes = operationPrototypes ?? throw new ArgumentNullException(nameof(operationPrototypes));

            if (!this.OperationPrototypes.Any())
            {
                throw new ArgumentException("Cannot be an empty collection", nameof(operationPrototypes));
            }

            if (this.OperationPrototypes.Any(_ => _ == null))
            {
                throw new ArgumentNullException(nameof(operationPrototypes), "Cannot contain nulls.");
            }
        }

        /// <summary>
        /// Gets the operation builders for the sequence.
        /// </summary>
        public IReadOnlyCollection<OperationPrototype> OperationPrototypes { get; private set; }
    }

    /// <summary>
    /// Abstract base of an operation.
    /// </summary>
    public class DispatchedOperationSequence : OperationSequence
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DispatchedOperationSequence"/> class.
        /// </summary>
        /// <param name="operationPrototypes">Operations builders for the sequence.</param>
        /// <param name="channel">Channel to dispatch to.</param>
        public DispatchedOperationSequence(
            IReadOnlyCollection<OperationPrototype> operationPrototypes,
            Channel channel)
            : base(operationPrototypes)
        {
            this.Channel = channel ?? throw new ArgumentNullException(nameof(channel));
        }

        /// <summary>
        /// Gets the channel to dispatch to.
        /// </summary>
        public Channel Channel { get; private set; }
    }
}
