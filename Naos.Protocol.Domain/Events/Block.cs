// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Block.cs" company="Naos Project">
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
    public class Block : EventBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Block"/> class.
        /// </summary>
        /// <param name="details">The details.</param>
        public Block(
            string details)
        {
            this.Details = details ?? throw new ArgumentNullException(nameof(details));
        }

        /// <summary>
        /// Gets the details about why it's blocked.
        /// </summary>
        /// <value>The details about why it's blocked.</value>
        public string Details { get; private set; }
    }
}