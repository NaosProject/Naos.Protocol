// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GetTagsFromObjectOp.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Protocol.Domain
{
    using System.Collections.Generic;

    /// <summary>
    /// Gets the tags from an object.
    /// </summary>
    /// <typeparam name="TObject">Type of object.</typeparam>
    public class GetTagsFromObjectOp<TObject> : ReturningOperationBase<IReadOnlyDictionary<string, string>>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetTagsFromObjectOp{TObject}"/> class.
        /// </summary>
        /// <param name="objectToDetermineKeyFrom">The object to determine tags from.</param>
        public GetTagsFromObjectOp(
            TObject objectToDetermineKeyFrom)
        {
            this.ObjectToDetermineKeyFrom = objectToDetermineKeyFrom;
        }

        /// <summary>
        /// Gets the object to determine key from.
        /// </summary>
        /// <value>The object to determine key from.</value>
        public TObject ObjectToDetermineKeyFrom { get; private set; }
    }
}
