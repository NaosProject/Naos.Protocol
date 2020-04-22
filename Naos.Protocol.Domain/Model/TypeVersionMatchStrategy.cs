// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TypeVersionMatchStrategy.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Protocol.Domain
{
    /// <summary>
    /// Strategy to use when determining version or version-less matching.
    /// </summary>
    public enum TypeVersionMatchStrategy
    {
        /// <summary>
        /// Skip the creation.
        /// </summary>
        Any,

        /// <summary>
        /// Match specific version.
        /// </summary>
        Specific,
    }
}
