// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PruneRequested.cs" company="Naos Project">
//     Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Protocol.Domain
{
    using System;

    /// <summary>
    /// Event indicating a prune should be done on the stream (standard reads will not go prior to the requested checkpoint).
    /// </summary>
    /// <seealso cref="Naos.Protocol.Domain.EventBase" />
    public partial class PruneRequested : EventBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PruneRequested"/> class.
        /// </summary>
        /// <param name="details">The details.</param>
        /// <exception cref="System.ArgumentNullException">details.</exception>
        public PruneRequested(
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
