// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Handled.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Protocol.Domain
{
    using System;

    /// <summary>
    /// Event indicating that an event was handled.
    /// </summary>
    /// <typeparam name="TEvent">The type of the t operation.</typeparam>
    /// <seealso cref="Naos.Protocol.Domain.EventBase" />
    public class Handled<TEvent> : EventBase
        where TEvent : EventBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Handled{TEvent}"/> class.
        /// </summary>
        /// <param name="handledEvent">The handled event.</param>
        public Handled(
            TEvent handledEvent)
        {
            this.HandledEvent = handledEvent ?? throw new ArgumentNullException(nameof(handledEvent));
        }

        /// <summary>
        /// Gets the Handled operation.
        /// </summary>
        /// <value>The Handled operation.</value>
        public TEvent HandledEvent { get; private set; }
    }
}
