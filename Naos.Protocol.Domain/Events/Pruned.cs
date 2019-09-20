// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Pruned.cs" company="Naos Project">
//     Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Protocol.Domain
{
    using System;

    /// <summary>
    /// Event container.
    /// Implements the <see cref="Naos.Protocol.Domain.EventBase" />.
    /// </summary>
    /// <seealso cref="Naos.Protocol.Domain.EventBase" />
    public class Pruned : EventBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Pruned"/> class.
        /// </summary>
        /// <param name="details">The details.</param>
        /// <exception cref="System.ArgumentNullException">details.</exception>
        public Pruned(
            string details)
        {
            this.Details = details ?? throw new ArgumentNullException(nameof(details));
        }

        /// <summary>
        /// Gets the details.
        /// </summary>
        /// <value>The details.</value>
        public string Details { get; private set; }
    }
}