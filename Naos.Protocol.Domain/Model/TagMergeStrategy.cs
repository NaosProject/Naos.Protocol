// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TagMergeStrategy.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Protocol.Domain
{
    /// <summary>
    /// Strategy for merging tags.
    /// </summary>
    public enum TagMergeStrategy
    {
        /// <summary>
        /// Throw an exception if the key already exists during merging.
        /// </summary>
        ThrowOnExistingKey,

        /// <summary>
        /// Skip the new tag if the key already exists during merging.
        /// </summary>
        SkipOnExistingKey,

        /// <summary>
        /// Overwrite the existing tag if the key already exists during merging.
        /// </summary>
        OverwriteExistingKey,
    }
}