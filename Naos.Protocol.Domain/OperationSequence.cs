// --------------------------------------------------------------------------------------------------------------------
// <copyright file="OperationSequence.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Protocol.Domain
{
    using System;
    using System.Collections.Generic;
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
        /// <param name="operationBuilders">Operation builders for the sequence.</param>
        protected OperationSequence(IReadOnlyCollection<Expression<Func<ILocker, OperationBase>>> operationBuilders)
        {
            new { operationBuilders }.Must().NotBeNullNorEmptyEnumerableNorContainAnyNulls();

            this.OperationBuilders = operationBuilders;
        }

        /// <summary>
        /// Gets the operation builders for the sequence.
        /// </summary>
        public IReadOnlyCollection<Expression<Func<ILocker, OperationBase>>> OperationBuilders { get; private set; }
    }

    /// <summary>
    /// Abstract base of an operation.
    /// </summary>
    public class DispatchedOperationSequence : OperationSequence
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DispatchedOperationSequence"/> class.
        /// </summary>
        /// <param name="operationBuilders">Operations builders for the sequence.</param>
        /// <param name="channel">Channel to dispatch to.</param>
        public DispatchedOperationSequence(
            IReadOnlyCollection<Expression<Func<ILocker, OperationBase>>> operationBuilders,
            Channel channel)
            : base(operationBuilders)
        {
            new { channel }.Must().NotBeNull();
            this.Channel = channel;
        }

        /// <summary>
        /// Gets the channel to dispatch to.
        /// </summary>
        public Channel Channel { get; private set; }
    }
}
