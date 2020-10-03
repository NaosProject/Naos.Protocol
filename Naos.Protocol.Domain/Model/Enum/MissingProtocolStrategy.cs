// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MissingProtocolStrategy.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Protocol.Domain
{
    /// <summary>
    /// Event container.
    /// </summary>
    public enum MissingProtocolStrategy
    {
        /// <summary>
        /// Throw on missing protocol.
        /// </summary>
        Throw,

        /// <summary>
        /// Return null on missing protocol.
        /// </summary>
        Null,
    }
}
