// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GetTagsFromObjectOp{TObject}.cs" company="Naos Project">
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
    public partial class GetTagsFromObjectOp<TObject> : ReturningOperationBase<IReadOnlyCollection<KeyValuePair<string, string>>>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetTagsFromObjectOp{TObject}"/> class.
        /// </summary>
        /// <param name="objectToDetermineTagsFrom">The object to determine tags from.</param>
        public GetTagsFromObjectOp(
            TObject objectToDetermineTagsFrom)
        {
            this.ObjectToDetermineTagsFrom = objectToDetermineTagsFrom;
        }

        /// <summary>
        /// Gets the object to determine tags from.
        /// </summary>
        /// <value>The object to determine tags from.</value>
        public TObject ObjectToDetermineTagsFrom { get; private set; }
    }
}
