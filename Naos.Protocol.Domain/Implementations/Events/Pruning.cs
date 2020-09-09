// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Pruning.cs" company="Naos Project">
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
    public partial class Pruning : EventBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Pruning"/> class.
        /// </summary>
        /// <param name="pruner">The pruner.</param>
        /// <exception cref="System.ArgumentNullException">pruner.</exception>
        public Pruning(
            string pruner)
        {
            this.Pruner = pruner ?? throw new ArgumentNullException(nameof(pruner));
        }

        /// <summary>
        /// Gets the pruner.
        /// </summary>
        /// <value>The pruner.</value>
        public string Pruner { get; private set; }
    }
}
