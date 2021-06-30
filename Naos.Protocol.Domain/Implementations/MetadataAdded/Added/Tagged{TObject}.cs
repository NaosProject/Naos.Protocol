// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Tagged{TObject}.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Protocol.Domain
{
    using System;
    using System.Collections.Generic;
    using OBeautifulCode.Type;

    /// <summary>
    /// Container to hold an object and tags.
    /// </summary>
    /// <typeparam name="TObject">The type of the object.</typeparam>
    /// <seealso cref="OBeautifulCode.Type.IHaveTags" />
    public partial class Tagged<TObject> : IDeepCloneMergingInNewTags<Tagged<TObject>>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Tagged{TObject}"/> class.
        /// </summary>
        /// <param name="object">The object.</param>
        /// <param name="tags">The tags.</param>
        public Tagged(
            TObject @object,
            IReadOnlyCollection<KeyValuePair<string, string>> tags)
        {
            this.Object = @object;
            this.Tags = tags;
        }

        /// <summary>
        /// Gets the object.
        /// </summary>
        /// <value>The object.</value>
        public TObject Object { get; private set; }

        /// <inheritdoc />
        public IReadOnlyCollection<KeyValuePair<string, string>> Tags { get; private set; }

        /// <inheritdoc />
        public Tagged<TObject> DeepCloneMergingInNewTags(
            IReadOnlyCollection<KeyValuePair<string, string>> newTags,
            TagMergeStrategy tagMergeStrategy = TagMergeStrategy.ThrowOnExistingKey)
        {
            throw new NotImplementedException();
        }
    }
}
