// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SelfIdentifiedTimestampedTagged{TId,TObject}.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Protocol.Domain
{
    using System;
    using System.Collections.Generic;
    using Naos.CodeAnalysis.Recipes;
    using OBeautifulCode.Type;

    /// <summary>
    /// Container to extract the identifier.
    /// </summary>
    /// <typeparam name="TId">The type of the identifier.</typeparam>
    /// <typeparam name="TObject">The type of the object.</typeparam>
    /// <seealso cref="IIdentifiableBy{TId}" />
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Timestamped", Justification = NaosSuppressBecause.CA1704_IdentifiersShouldBeSpelledCorrectly_SpellingIsCorrectInContextOfTheDomain)]
    public partial class SelfIdentifiedTimestampedTagged<TId, TObject> : IIdentifiableBy<TId>, IHaveTimestampUtc, IHaveTags
        where TObject : IIdentifiableBy<TId>, IHaveTimestampUtc, IHaveTags
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SelfIdentifiedTimestampedTagged{TId, TObject}"/> class.
        /// </summary>
        /// <param name="object">The object.</param>
        public SelfIdentifiedTimestampedTagged(
            TObject @object)
        {
            if (@object == null)
            {
                throw new ArgumentNullException(nameof(@object));
            }

            this.Object = @object;
        }

        /// <inheritdoc />
        public TId Id => this.Object.Id;

        /// <inheritdoc />
        public DateTime TimestampUtc => this.Object.TimestampUtc;

        /// <inheritdoc />
        public IReadOnlyCollection<KeyValuePair<string, string>> Tags => this.Object.Tags;

        /// <summary>
        /// Gets the object.
        /// </summary>
        /// <value>The object.</value>
        public TObject Object { get; private set; }
    }
}
