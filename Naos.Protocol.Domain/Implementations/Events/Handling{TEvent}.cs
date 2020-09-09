// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Handling{TEvent}.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Protocol.Domain
{
    using System;

    /// <summary>
    /// Event indicating that an event is being handled.
    /// </summary>
    /// <typeparam name="TEvent">The type of the event.</typeparam>
    /// <seealso cref="Naos.Protocol.Domain.EventBase" />
    public class Handling<TEvent> : EventBase
        where TEvent : EventBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Handling{TEvent}"/> class.
        /// </summary>
        /// <param name="handledEvent">The handled event.</param>
        public Handling(
            TEvent handledEvent)
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
