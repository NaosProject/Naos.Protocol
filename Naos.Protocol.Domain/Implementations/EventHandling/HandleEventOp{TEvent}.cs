// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HandleEventOp{TEvent}.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Protocol.Domain
{
    using System;

    /// <summary>
    /// Handle (perform specific logic on) an event of the specified <typeparamref name="TEvent"/>.
    /// </summary>
    /// <typeparam name="TEvent">Type of the event to be handled.</typeparam>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "ExecuteScalar", Justification = "Name/Spelling is correct.")]
    public partial class HandleEventOp<TEvent> : VoidOperationBase
        where TEvent : IEvent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HandleEventOp{TEvent}"/> class.
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
