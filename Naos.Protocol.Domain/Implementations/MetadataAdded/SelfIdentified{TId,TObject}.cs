// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SelfIdentified{TId,TObject}.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Protocol.Domain
{
    using System;
    using System.Collections.Generic;
    using OBeautifulCode.Type;

    /// <summary>
    /// Container to extract the identifier.
    /// </summary>
    /// <typeparam name="TId">The type of the identifier.</typeparam>
    /// <typeparam name="TObject">The type of the object.</typeparam>
    /// <seealso cref="OBeautifulCode.Type.IIdentifiableBy{TId}" />
    public partial class SelfIdentified<TId, TObject> : IIdentifiableBy<TId>
        where TObject : IIdentifiableBy<TId>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SelfIdentified{TId, TObject}"/> class.
        /// </summary>
        /// <param name="object">The object.</param>
        public SelfIdentified(
            TObject @object)
        {
            if (@object == null)
            {
                throw new ArgumentNullException(nameof(@object));
            }

            this.Object = @object;
        }

        /// <inheritdoc />
        public TId Id => this.Object.Id;

        /// <summary>
        /// Gets the object.
        /// </summary>
        /// <value>The object.</value>
        public TObject Object { get; private set; }
    }
}
