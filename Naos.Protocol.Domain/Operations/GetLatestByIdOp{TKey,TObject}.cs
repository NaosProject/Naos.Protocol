// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GetLatestByIdOp{TKey,TObject}.cs" company="Naos Project">
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
    public class GetLatestByIdOp<TKey, TObject> : ReturningOperationBase<TObject>, IHaveKey<TKey>, IHaveKeyType
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetLatestByIdOp{TKey,TObject}"/> class.
        /// </summary>
        /// <param name="key">The key.</param>
        public GetLatestByIdOp(
            TKey key)
        {
            this.Id = key;
        }

        /// <inheritdoc />
        public TKey Id { get; private set; }

        /// <inheritdoc />
        public Type IdType => typeof(TKey);
    }
}
