// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SelfTagged{TObject}.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Protocol.Domain
{
    using System;
    using System.Collections.Generic;
    using OBeautifulCode.Type;

    /// <summary>
    /// Container to auto extract tags.
    /// </summary>
    /// <typeparam name="TObject">The type of the object.</typeparam>
    /// <seealso cref="OBeautifulCode.Type.IIdentifiableBy{TId}" />
    public partial class SelfTagged<TObject> : IHaveTags
        where TObject : IHaveTags
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SelfTagged{TObject}"/> class.
        /// </summary>
        /// <param name="object">The object.</param>
        public SelfTagged(
            TObject @object)
        {
            if (@object == null)
            {
                throw new ArgumentNullException(nameof(@object));
            }

            this.Object = @object;
        }

        /// <inheritdoc />
        public IReadOnlyDictionary<string, string> Tags => this.Object.Tags;

        /// <summary>
        /// Gets the object.
        /// </summary>
        /// <value>The object.</value>
        public TObject Object { get; private set; }
    }
}
