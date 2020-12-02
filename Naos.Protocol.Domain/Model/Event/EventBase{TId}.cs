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
    /// Abstract base class for an event that is both identifiable and UTC timestamped.
    /// </summary>
    /// <typeparam name="TId">The type of the identifier.</typeparam>
    public abstract partial class EventBase<TId> : EventBaseBase, IEvent<TId>, IHaveTimestampUtc, IModelViaCodeGen
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EventBase{TId}"/> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="timestampUtc">The timestamp in UTC.</param>
        protected EventBase(
            TId id,
            DateTime timestampUtc)
        {
            this.Id = id;
            this.TimestampUtc = timestampUtc;
        }

        /// <inheritdoc />
        public TId Id { get; private set; }

        /// <inheritdoc />
        public DateTime TimestampUtc { get; private set; }
    }
}
