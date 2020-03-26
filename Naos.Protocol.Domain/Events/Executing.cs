// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Executing.cs" company="Naos Project">
//     Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Protocol.Domain
{
    using System;

    /// <summary>
    /// Event indicating that an operation is being executed.
    /// </summary>
    /// <typeparam name="TOperation">The type of the t operation.</typeparam>
    /// <seealso cref="Naos.Protocol.Domain.EventBase" />
    public class Executing<TOperation> : EventBase
        where TOperation : class
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Executing{TOperation}"/> class.
        /// </summary>
        /// <param name="executedOperation">The executed operation.</param>
        /// <exception cref="System.ArgumentNullException">executedOperation.</exception>
        public Executing(
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
