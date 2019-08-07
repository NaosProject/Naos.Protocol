// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Created.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Protocol.Domain
{
    using System;

    /// <summary>
    /// Event container.
    /// </summary>
    public class Created<TObject> : EventBase
        where TObject : class
    {
        public Created(
            TObject createdObject)
        {
            this.CreatedObject = createdObject ?? throw new ArgumentNullException(nameof(createdObject));
        }

        public TObject CreatedObject { get; private set; }
    }
}