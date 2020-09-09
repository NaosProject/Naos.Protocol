// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CancelBlock.cs" company="Naos Project">
//     Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Protocol.Domain
{
    using System;

    /// <summary>
    /// Event to indicate a <see cref="Block" /> was cancelled (i.e. ignore a previous <see cref="Block" />).
    /// </summary>
    /// <seealso cref="Naos.Protocol.Domain.EventBase" />
    public partial class CancelBlock : EventBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CancelBlock"/> class.
        /// </summary>
        /// <param name="details">The details.</param>
        /// <exception cref="System.ArgumentNullException">details.</exception>
        public CancelBlock(
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
