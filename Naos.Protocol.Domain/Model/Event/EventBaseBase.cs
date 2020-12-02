// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EventBaseBase.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Protocol.Domain
{
    using System;
    using OBeautifulCode.Assertion.Recipes;
    using OBeautifulCode.Type;
    using static System.FormattableString;

    /// <summary>
    /// Abstract base class for an <see cref="IEvent"/>.
    /// </summary>
    public abstract partial class EventBaseBase : IEvent, IModelViaCodeGen
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EventBaseBase"/> class.
        /// </summary>
        /// <param name="timestampUtc">The time of the event in UTC.</param>
        protected EventBaseBase(
            DateTime timestampUtc)
        {
            timestampUtc.Kind.MustForArg(Invariant($"{nameof(timestampUtc)}.{nameof(timestampUtc.Kind)}")).BeEqualTo(DateTimeKind.Utc);

            this.TimestampUtc = timestampUtc;
        }

        /// <inheritdoc />
        public DateTime TimestampUtc { get; private set; }
    }
}
