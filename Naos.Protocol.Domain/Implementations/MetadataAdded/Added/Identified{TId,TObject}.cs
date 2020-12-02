// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Identified{TId,TObject}.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Protocol.Domain
{
    using System;
    using System.Collections.Generic;
    using OBeautifulCode.Type;

    /// <summary>
    /// Container to hold an object and an identifier.
    /// </summary>
    /// <typeparam name="TId">The type of the identifier.</typeparam>
    /// <typeparam name="TObject">The type of the object.</typeparam>
    /// <seealso cref="OBeautifulCode.Type.IIdentifiableBy{TId}" />
    public partial class Identified<TId, TObject> : IIdentifiableBy<TId>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Identified{TId, TObject}"/> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="object">The object.</param>
        public Identified(
            TId id,
            TObject @object)
        {
            this.Id = id;
            this.Object = @object;
        }

        /// <inheritdoc />
        public TId Id { get; private set; }

        /// <summary>
        /// Gets the object.
        /// </summary>
        /// <value>The object.</value>
        public TObject Object { get; private set; }
    }
}
