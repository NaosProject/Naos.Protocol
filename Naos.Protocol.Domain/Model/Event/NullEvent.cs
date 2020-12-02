// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NullEvent.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Protocol.Domain
{
    using System;

    /// <summary>
    /// Null object pattern implementation for <see cref="EventBase{TId}"/>.
    /// </summary>
    public partial class NullEvent : EventBaseBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NullEvent"/> class.
        /// </summary>
        public NullEvent()
        {
        }
    }
}
