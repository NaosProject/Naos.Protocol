// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HandleEventOp.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Protocol.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using OBeautifulCode.Representation;
    using OBeautifulCode.Type;
    using static System.FormattableString;

    /// <summary>
    /// Abstract base of an operation.
    /// </summary>
    /// <typeparam name="TEvent">Type of data being written.</typeparam>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "ExecuteScalar", Justification = "Name/Spelling is correct.")]
    public class HandleEventOp<TEvent> : VoidOperationBase
        where TEvent : EventBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HandleEventOp{TEvent}"/> class.
        /// </summary>
        /// <param name="eventToHandle">The eventToHandle to operate on.</param>
        public HandleEventOp(TEvent eventToHandle)
        {
            this.EventToHandle = eventToHandle ?? throw new ArgumentNullException(nameof(eventToHandle));
        }

        /// <summary>
        /// Gets the eventToHandle.
        /// </summary>
        /// <value>The eventToHandle.</value>
        public TEvent EventToHandle { get; private set; }
    }
}