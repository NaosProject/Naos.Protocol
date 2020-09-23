// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RecordedObjectEvent{TId,TObject}.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Protocol.Domain
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Event containing a specific moment in time of an object.
    /// </summary>
    /// <typeparam name="TId">The type of the identifier.</typeparam>
    /// <typeparam name="TObject">The type of the object.</typeparam>
    public partial class RecordedObjectEvent<TId, TObject> : EventBase<TId>
        where TObject : class
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RecordedObjectEvent{TId, TObject}"/> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="timestampUtc">The timestamp UTC.</param>
        /// <param name="createdObject">The created object.</param>
        /// <param name="tags">The optional tags.</param>
        public RecordedObjectEvent(
            TId id,
            DateTime timestampUtc,
            TObject createdObject,
            IReadOnlyDictionary<string, string> tags = null)
            : base(id, timestampUtc, tags)
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
