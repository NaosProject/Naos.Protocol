// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Created{TObject}.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Protocol.Domain
{
    using System;

    /// <summary>
    /// Event showing the creation of some object.
    /// </summary>
    /// <typeparam name="TObject">The type of the t object.</typeparam>
    /// <seealso cref="Naos.Protocol.Domain.EventBase" />
    public class Created<TObject> : EventBase
        where TObject : class
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Created{TObject}"/> class.
        /// </summary>
        /// <param name="createdObject">The created object.</param>
        /// <exception cref="System.ArgumentNullException">createdObject.</exception>
        public Created(
            TObject createdObject)
        {
            this.CreatedObject = createdObject ?? throw new ArgumentNullException(nameof(createdObject));
        }

        /// <summary>
        /// Gets the created object.
        /// </summary>
        /// <value>The created object.</value>
        public TObject CreatedObject { get; private set; }
    }
}
