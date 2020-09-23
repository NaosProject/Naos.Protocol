// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HandleEventOp{TEvent,TEventId}.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Protocol.Domain
{
    using System;

    /// <summary>
    /// Abstract base of an operation.
    /// </summary>
    /// <typeparam name="TEvent">Type of the event to be handled.</typeparam>
    /// <typeparam name="TEventId">Type of the identifier of the event.</typeparam>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "ExecuteScalar", Justification = "Name/Spelling is correct.")]
    public partial class HandleEventOp<TEvent, TEventId> : VoidOperationBase
        where TEvent : IEvent<TEventId>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HandleEventOp{TEvent, TEventId}"/> class.
        /// </summary>
        /// <param name="eventToHandle">The event to handle.</param>
        public HandleEventOp(
            TEvent eventToHandle)
        {
            if (eventToHandle == null)
            {
                throw new ArgumentNullException(nameof(eventToHandle));
            }

            this.EventToHandle = eventToHandle;
        }

        /// <summary>
        /// Gets the eventToHandle.
        /// </summary>
        /// <value>The eventToHandle.</value>
        public TEvent EventToHandle { get; private set; }
    }
}
