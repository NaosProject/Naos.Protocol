// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EventBase{TId}.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Protocol.Domain
{
    using System;
    using System.Collections.Generic;
    using OBeautifulCode.Type;

    /// <summary>
    /// Generic implementation for <see cref="IEvent{TId}"/> which makes tags optional.
    /// Implements the <see cref="Naos.Protocol.Domain.IEvent{TId}" />.
    /// </summary>
    /// <typeparam name="TId">The type of the t identifier.</typeparam>
    /// <seealso cref="Naos.Protocol.Domain.IEvent{TId}" />
    /// <seealso cref="OBeautifulCode.Type.IModelViaCodeGen" />
    public abstract partial class EventBase<TId> : IEvent<TId>, IModelViaCodeGen
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EventBase{TId}"/> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="timestampUtc">The timestamp UTC.</param>
        /// <param name="tags">The tags (optional).</param>
        protected EventBase(
            TId id,
            DateTime timestampUtc,
            IReadOnlyDictionary<string, string> tags = null)
        {
            this.Id = id;
            this.TimestampUtc = timestampUtc;
            this.Tags = tags ?? new Dictionary<string, string>();
        }

        /// <inheritdoc />
        public TId Id { get; private set; }

        /// <inheritdoc />
        public IReadOnlyDictionary<string, string> Tags { get; private set; }

        /// <inheritdoc />
        public DateTime TimestampUtc { get; private set; }
    }
}
