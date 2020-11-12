// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EventBase{TId}.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Protocol.Domain
{
    using System;
    using OBeautifulCode.Type;

    /// <summary>
    /// Abstract base class for an event that is both identifiable and UTC timestamped.
    /// </summary>
    /// <typeparam name="TId">The type of the identifier.</typeparam>
    public abstract partial class EventBase<TId> : IEvent, IIdentifiableBy<TId>, IHaveTimestampUtc, IModelViaCodeGen
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EventBase{TId}"/> class.
        /// </summary>
        /// <param name="id">Optional, identifier; default will be the 'default' for <typeparamref name="TId"/>.</param>
        /// <param name="timestampUtc">Optional UTC timestamp of the occurence; default will be <see cref="DateTime"/>.'UtcNow' at time of construction.</param>
        protected EventBase(
            TId id = default, // this is totally made up, it works but if we don't require it then why are we pretending it's party of the contract?
            DateTime? timestampUtc = null) // doesn't work in codegen or serialization b/c the types don't match; default does NOT allow you to specify DateTime.Min
        {
            this.Id = id;
            this.TimestampUtc = timestampUtc ?? DateTime.UtcNow;
        }

        /// <inheritdoc />
        public TId Id { get; private set; }

        /// <inheritdoc />
        public DateTime TimestampUtc { get; private set; }
    }
}
