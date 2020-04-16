// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Executed.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Protocol.Domain
{
    using System;

    /// <summary>
    /// Event indicating that an operation was executed.
    /// </summary>
    /// <typeparam name="TOperation">The type of the operation.</typeparam>
    /// <seealso cref="Naos.Protocol.Domain.EventBase" />
    public class Executed<TOperation> : EventBase
        where TOperation : class, IOperation
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Executed{TOperation}"/> class.
        /// </summary>
        /// <param name="executedOperation">The executed operation.</param>
        /// <exception cref="System.ArgumentNullException">executedOperation.</exception>
        public Executed(
            TOperation executedOperation)
        {
            this.ExecutedOperation = executedOperation ?? throw new ArgumentNullException(nameof(executedOperation));
        }

        /// <summary>
        /// Gets the executed operation.
        /// </summary>
        /// <value>The executed operation.</value>
        public TOperation ExecutedOperation { get; private set; }
    }
}
