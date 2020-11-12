// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HandlingEventEvent{TId,TEvent}.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Protocol.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using Naos.CodeAnalysis.Recipes;
    using OBeautifulCode.Type;

    /// <summary>
    /// Event indicating that an event was handling.
    /// </summary>
    /// <typeparam name="TId">The type of the identifier.</typeparam>
    /// <typeparam name="TEvent">The type of the event.</typeparam>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1005:AvoidExcessiveParametersOnGenericTypes", Justification = NaosSuppressBecause.CA1005_AvoidExcessiveParametersOnGenericTypes_SpecifiedParametersRequiredForNeededFunctionality)]
    public partial class HandlingEventEvent<TId, TEvent> : EventBase<TId>, IHaveTags
        where TEvent : class, IEvent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HandlingEventEvent{TId, TEvent}"/> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="timestampUtc">The timestamp UTC.</param>
        /// <param name="handlingEvent">The handling event.</param>
        /// <param name="tags">The optional tags.</param>
        public HandlingEventEvent(
            TId id,
            DateTime timestampUtc,
            TEvent handlingEvent,
            IReadOnlyDictionary<string, string> tags = null)
        : base(id, timestampUtc)
        {
            this.HandlingEvent = handlingEvent ?? throw new ArgumentNullException(nameof(handlingEvent));
            this.Tags = tags;
        }

        /// <summary>
        /// Gets the Handling operation.
        /// </summary>
        /// <value>The Handling operation.</value>
        public TEvent HandlingEvent { get; private set; }

        /// <inheritdoc />
        public IReadOnlyDictionary<string, string> Tags { get; private set; }
    }
}
