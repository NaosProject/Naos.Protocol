// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HandledEventEvent{TId,TEvent}.cs" company="Naos Project">
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
    using OBeautifulCode.Assertion.Recipes;
    using OBeautifulCode.Type;

    /// <summary>
    /// Event indicating that an event was handled.
    /// </summary>
    /// <typeparam name="TId">The type of the identifier.</typeparam>
    /// <typeparam name="TEvent">The type of the event.</typeparam>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1005:AvoidExcessiveParametersOnGenericTypes", Justification = NaosSuppressBecause.CA1005_AvoidExcessiveParametersOnGenericTypes_SpecifiedParametersRequiredForNeededFunctionality)]
    public partial class HandledEventEvent<TId, TEvent> : EventBase<TId>, IHaveTags
        where TEvent : class, IEvent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HandledEventEvent{TId, TEvent}"/> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="timestampUtc">The timestamp UTC.</param>
        /// <param name="details">The details.</param>
        /// <param name="tags">The optional tags.</param>
        public HandledEventEvent(
            TId id,
            DateTime timestampUtc,
            string details,
            IReadOnlyDictionary<string, string> tags = null)
        : base(id, timestampUtc)
        {
            details.MustForArg(nameof(details)).NotBeNullNorWhiteSpace();
            this.Details = details;
            this.Tags = tags;
        }

        /// <summary>
        /// Gets the details.
        /// </summary>
        /// <value>The details.</value>
        public string Details { get; private set; }

        /// <inheritdoc />
        public IReadOnlyDictionary<string, string> Tags { get; private set; }
    }
}
