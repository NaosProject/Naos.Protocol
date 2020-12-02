// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Timestamped{TObject}.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Protocol.Domain
{
    using System;
    using Naos.CodeAnalysis.Recipes;
    using OBeautifulCode.Assertion.Recipes;
    using OBeautifulCode.Type;
    using static System.FormattableString;

    /// <summary>
    /// Container to hold an object and tags.
    /// </summary>
    /// <typeparam name="TObject">The type of the object.</typeparam>
    /// <seealso cref="IIdentifiableBy{TId}" />
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Timestamped", Justification = NaosSuppressBecause.CA1704_IdentifiersShouldBeSpelledCorrectly_SpellingIsCorrectInContextOfTheDomain)]
    public partial class Timestamped<TObject> : IHaveTimestampUtc
        where TObject : IDeepCloneable<TObject>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Timestamped{TObject}"/> class.
        /// </summary>
        /// <param name="timestampUtc">The timestamp.</param>
        /// <param name="object">The object.</param>
        public Timestamped(
            DateTime timestampUtc,
            TObject @object)
        {
            if (@object == null)
            {
                throw new ArgumentNullException(nameof(@object));
            }

            timestampUtc.Kind.MustForArg(Invariant($"{nameof(timestampUtc)}.{nameof(timestampUtc.Kind)}")).BeEqualTo(DateTimeKind.Utc);

            this.TimestampUtc = timestampUtc;
            this.Object = @object;
        }

        /// <inheritdoc />
        public DateTime TimestampUtc { get; }

        /// <summary>
        /// Gets the object.
        /// </summary>
        /// <value>The object.</value>
        public TObject Object { get; private set; }
    }
}
