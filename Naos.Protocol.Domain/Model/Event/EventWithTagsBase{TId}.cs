// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EventWithTagsBase{TId}.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Protocol.Domain
{
    using System;
    using System.Collections.Generic;
    using OBeautifulCode.Type;

    /// <summary>
    /// Abstract base class for an event that is both identifiable and UTC timestamped.
    /// </summary>
    /// <typeparam name="TId">The type of the identifier.</typeparam>
    public abstract partial class EventWithTagsBase<TId> : EventBaseBase, IHaveTags, IEvent<TId>, IHaveTimestampUtc, IModelViaCodeGen
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EventWithTagsBase{TId}"/> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="timestampUtc">The timestamp in UTC.</param>
        /// <param name="tags">The tags.</param>
        protected EventWithTagsBase(
            TId id,
            DateTime timestampUtc,
            IReadOnlyDictionary<string, string> tags = null)
            : base(timestampUtc)
        {
            this.Id = id;
            this.Tags = tags;
        }

        /// <inheritdoc />
        public TId Id { get; private set; }

        /// <inheritdoc />
        public IReadOnlyDictionary<string, string> Tags { get; private set; }
    }
}
