// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IDeepCloneMergingInNewTags{TReturn}.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Protocol.Domain
{
    using System.Collections.Generic;
    using OBeautifulCode.Type;

    /// <summary>
    /// Supports getting a deep clone of the object with additional tags merged in; useful in events
    /// and operations that might be routed and need to collect session markers or other metadata along the way.
    /// </summary>
    /// <typeparam name="TReturn">The type of the object returned after deep cloning.</typeparam>
    public interface IDeepCloneMergingInNewTags<TReturn> : IHaveTags
    {
        /// <summary>
        /// Creates a deep clone of the object with the provided tags merged in using the provided strategy.
        /// </summary>
        /// <param name="newTags">The new tags to be merged in.</param>
        /// <param name="tagMergeStrategy">The tag merging strategy.</param>
        /// <returns>Deep cloned object with new tags merged in.</returns>
        TReturn DeepCloneMergingInNewTags(
            IReadOnlyCollection<KeyValuePair<string, string>> newTags,
            TagMergeStrategy tagMergeStrategy = TagMergeStrategy.ThrowOnExistingKey);
    }
}