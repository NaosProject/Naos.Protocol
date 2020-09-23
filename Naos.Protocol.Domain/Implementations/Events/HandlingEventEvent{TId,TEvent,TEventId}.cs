// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HandlingEventEvent{TId,TEvent,TEventId}.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Protocol.Domain
{
    using System;
    using System.Collections.Generic;
    using Naos.Protocol.Domain.Internal;

    /// <summary>
    /// Event indicating that an event is being handled.
    /// </summary>
    /// <typeparam name="TId">The type of the identifier.</typeparam>
    /// <typeparam name="TEvent">The type of the event.</typeparam>
    /// <typeparam name="TEventId">The type of the identifier of the provided event.</typeparam>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1005:AvoidExcessiveParametersOnGenericTypes", Justification = NaosSuppressBecause.CA1005_AvoidExcessiveParametersOnGenericTypes_SpecifiedParametersRequiredForNeededFunctionality)]
    public partial class Handling<TId, TEvent, TEventId> : EventBase<TId>
        where TEvent : class, IEvent<TEventId>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Handling{TId, TEvent, TEventId}"/> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="timestampUtc">The timestamp in UTC.</param>
        /// <param name="handledEvent">The handled event.</param>
        /// <param name="tags">The optional tags.</param>
        public Handling(
            TId id,
            DateTime timestampUtc,
            TEvent handledEvent,
            IReadOnlyDictionary<string, string> tags = null)
        : base(id, timestampUtc, tags)
        {
            this.HandledEvent = handledEvent ?? throw new ArgumentNullException(nameof(handledEvent));
        }

        /// <summary>
        /// Gets the executed operation.
        /// </summary>
        /// <value>The executed operation.</value>
        public TEvent HandledEvent { get; private set; }
    }
}
