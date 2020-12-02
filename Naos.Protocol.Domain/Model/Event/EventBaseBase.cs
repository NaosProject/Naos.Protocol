// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EventBaseBase.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Protocol.Domain
{
    using System;
    using System.Collections.Generic;
    using OBeautifulCode.Type;

    /// <summary>
    /// Abstract base class for an <see cref="IEvent"/>.
    /// </summary>
    public abstract partial class EventBaseBase : IEvent, IModelViaCodeGen
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EventBaseBase"/> class.
        /// </summary>
        protected EventBaseBase()
        {
        }
    }
}
