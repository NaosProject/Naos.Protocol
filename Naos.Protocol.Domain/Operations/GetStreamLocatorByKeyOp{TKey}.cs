// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GetStreamLocatorByKeyOp{TKey}.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Protocol.Domain
{
    /// <summary>
    /// Abstract base of an operation.
    /// </summary>
    /// <typeparam name="TKey">Type of key being used.</typeparam>
    public class GetStreamLocatorByKeyOp<TKey> : ReturningOperationBase<StreamLocatorBase>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetStreamLocatorByKeyOp{TKey}"/> class.
        /// </summary>
        /// <param name="key">The key.</param>
        public GetStreamLocatorByKeyOp(
            TKey key)
        {
            this.Key = key;
        }

        /// <summary>
        /// Gets the key.
        /// </summary>
        /// <value>The key.</value>
        public TKey Key { get; private set; }
    }
}
