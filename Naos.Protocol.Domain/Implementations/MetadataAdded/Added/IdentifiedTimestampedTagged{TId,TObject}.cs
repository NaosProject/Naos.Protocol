// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IdentifiedTimestampedTagged{TId,TObject}.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Protocol.Domain
{
    using System;
    using System.Collections.Generic;
    using Naos.CodeAnalysis.Recipes;
    using OBeautifulCode.Assertion.Recipes;
    using OBeautifulCode.Type;
    using static System.FormattableString;

    /// <summary>
    /// Container to hold an object and tags.
    /// </summary>
    /// <typeparam name="TId">The type of the identifier.</typeparam>
    /// <typeparam name="TObject">The type of the object.</typeparam>
    /// <seealso cref="IIdentifiableBy{TId}" />
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Timestamped", Justification = NaosSuppressBecause.CA1704_IdentifiersShouldBeSpelledCorrectly_SpellingIsCorrectInContextOfTheDomain)]
    public partial class IdentifiedTimestampedTagged<TId, TObject> : IIdentifiableBy<TId>, IHaveTimestampUtc, IDeepCloneMergingInNewTags<IdentifiedTimestampedTagged<TId, TObject>>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IdentifiedTimestampedTagged{TId, TObject}"/> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="timestampUtc">The timestamp.</param>
        /// <param name="tags">The tags.</param>
        /// <param name="object">The object.</param>
        public IdentifiedTimestampedTagged(
            TId id,
            DateTime timestampUtc,
            IReadOnlyCollection<KeyValuePair<string, string>> tags,
            TObject @object)
        {
            timestampUtc.Kind.MustForArg(Invariant($"{nameof(timestampUtc)}.{nameof(timestampUtc.Kind)}")).BeEqualTo(DateTimeKind.Utc);

            this.Id = id;
            this.TimestampUtc = timestampUtc;
            this.Tags = tags;
            this.Object = @object;
        }

        /// <inheritdoc />
        public TId Id { get; private set; }

        /// <summary>
        /// Gets the object.
        /// </summary>
        /// <value>The object.</value>
        public TObject Object { get; private set; }

        /// <inheritdoc />
        public DateTime TimestampUtc { get; }

        /// <inheritdoc />
        public IReadOnlyCollection<KeyValuePair<string, string>> Tags { get; private set; }

        /// <inheritdoc />
        public IdentifiedTimestampedTagged<TId, TObject> DeepCloneMergingInNewTags(
            IReadOnlyCollection<KeyValuePair<string, string>> newTags,
            TagMergeStrategy tagMergeStrategy = TagMergeStrategy.ThrowOnExistingKey)
        {
            throw new NotImplementedException();
        }
    }
}
