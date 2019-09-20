// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GetByKeyOp.cs" company="Naos Project">
//     Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Protocol.Domain
{
    /// <summary>
    /// Implements the <see cref="Naos.Protocol.Domain.ReturningOperationBase{TObject}" /> to get an object by it's key.
    /// </summary>
    /// <typeparam name="TKey">The type of the t key.</typeparam>
    /// <typeparam name="TObject">The type of the t object.</typeparam>
    public partial class GetByKeyOp<TKey, TObject> : ReturningOperationBase<TObject>
        where TObject : class
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetByKeyOp{TKey, TObject}"/> class.
        /// </summary>
        /// <param name="key">The key.</param>
        public GetByKeyOp(TKey key)
        {
            this.Key = key;
        }

        /// <summary>
        /// Gets the key.
        /// </summary>
        /// <value>The key.</value>
        public TKey Key { get; }
    }
}