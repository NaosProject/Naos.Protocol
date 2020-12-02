// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NullIdentifiedEvent{TId}.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Protocol.Domain
{
    using System;

    /// <summary>
    /// Null object pattern implementation for <see cref="EventBase{TId}"/>.
    /// </summary>
    /// <typeparam name="TId">The type of the identifier.</typeparam>
    public partial class NullIdentifiedEvent<TId> : EventBase<TId>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NullIdentifiedEvent{TId}"/> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="timestampUtc">The optional timestamp; DEFAULT is default <see cref="DateTime"/>.</param>
        public NullIdentifiedEvent(TId id = default, DateTime timestampUtc = default)
        : base(id, timestampUtc)
        {
        }
    }
}
