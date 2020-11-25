// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FailedToHandleEventEvent{TId,TEvent}.cs" company="Naos Project">
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
    public partial class FailedToHandleEventEvent<TId, TEvent> : EventBase<TId>, IHaveTags
        where TEvent : class, IEvent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FailedToHandleEventEvent{TId, TEvent}"/> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="exceptionToString">The <see cref="Exception.ToString()"/> of the unhandled failure while handling an event.</param>
        /// <param name="timestampUtc">The date and time of the event in UTC.</param>
        /// <param name="tags">The optional tags.</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1720:IdentifiersShouldNotContainTypeNames", MessageId = "string", Justification = NaosSuppressBecause.CA1720_IdentifiersShouldNotContainTypeNames_TypeNameAddsClarityToIdentifierAndAlternativesDegradeClarity)]
        public FailedToHandleEventEvent(
            TId id,
            string exceptionToString,
            DateTime timestampUtc,
            IReadOnlyDictionary<string, string> tags = null)
        : base(id, timestampUtc)
        {
            exceptionToString.MustForArg(nameof(exceptionToString)).NotBeNullNorWhiteSpace();
            this.ExceptionToString = exceptionToString;
            this.Tags = tags;
        }

        /// <summary>
        /// Gets the exception to string.
        /// </summary>
        /// <value>The exception to string.</value>
        public string ExceptionToString { get; private set; }

        /// <inheritdoc />
        public IReadOnlyDictionary<string, string> Tags { get; private set; }
    }
}
