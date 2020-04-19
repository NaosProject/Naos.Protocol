// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GetLatestByKeyOp{TKey,TObject}.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Protocol.Domain
{
    using System;

    /// <summary>
    /// Operation to get the most recent object of a certain key.
    /// </summary>
    /// <typeparam name="TKey">The type of the key of the object.</typeparam>
    /// <typeparam name="TObject">The type of the object.</typeparam>
    /// <seealso cref="Naos.Protocol.Domain.ReturningOperationBase{TObject}" />
    public class GetLatestByKeyOp<TKey, TObject> : ReturningOperationBase<TObject>, IHaveKey<TKey>, IHaveKeyType
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetLatestByKeyOp{TKey, TObject}"/> class.
        /// </summary>
        /// <param name="key">The key.</param>
        public GetLatestByKeyOp(
            TKey key)
        {
            this.Key = key;
        }

        /// <inheritdoc />
        public TKey Key { get; private set; }

        /// <inheritdoc />
        public Type KeyType => typeof(TKey);
    }
}
