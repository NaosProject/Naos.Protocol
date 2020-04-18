// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GetKeyFromObjectOp.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Protocol.Domain
{
    using System;

    /// <summary>
    /// Abstract base of an operation.
    /// </summary>
    /// <typeparam name="TKey">Type of key being used.</typeparam>
    /// <typeparam name="TObject">Type of object.</typeparam>
    public class GetKeyFromObjectOp<TKey, TObject> : ReturningOperationBase<TKey>, IHaveKeyType
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetKeyFromObjectOp{TKey, TObject}"/> class.
        /// </summary>
        /// <param name="objectToDetermineKeyFrom">The object to determine key from.</param>
        public GetKeyFromObjectOp(
            TObject objectToDetermineKeyFrom)
        {
            this.ObjectToDetermineKeyFrom = objectToDetermineKeyFrom;
        }

        /// <summary>
        /// Gets the object to determine key from.
        /// </summary>
        /// <value>The object to determine key from.</value>
        public TObject ObjectToDetermineKeyFrom { get; private set; }

        /// <inheritdoc />
        public Type KeyType => typeof(TKey);
    }
}
