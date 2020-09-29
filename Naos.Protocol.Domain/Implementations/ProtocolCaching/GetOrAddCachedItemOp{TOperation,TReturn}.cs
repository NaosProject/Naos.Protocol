// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GetOrAddCachedItemOp{TOperation,TReturn}.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Protocol.Domain
{
    using System;
    using static System.FormattableString;

    /// <summary>
    /// Gets the item using the provided <see cref="IOperation"/> as the key in the cache dictionary.
    /// </summary>
    /// <typeparam name="TOperation">Type of the operation.</typeparam>
    /// <typeparam name="TReturn">Type of return type of the operation being cached.</typeparam>
    public partial class GetOrAddCachedItemOp<TOperation, TReturn> : ReturningOperationBase<CacheResult<TOperation, TReturn>>
    where TOperation : IReturningOperation<TReturn>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetOrAddCachedItemOp{TOperation, TReturn}"/> class.
        /// </summary>
        /// <param name="operation">The operation.</param>
        /// <param name="backingProtocol">The backing protocol if there is no item in the cache.</param>
        public GetOrAddCachedItemOp(
            TOperation operation,
            ISyncAndAsyncReturningProtocol<TOperation, TReturn> backingProtocol)
        {
            if (operation == null)
            {
                throw new ArgumentNullException(nameof(operation));
            }

            if (backingProtocol == null)
            {
                throw new ArgumentNullException(nameof(backingProtocol));
            }

            this.Operation = operation;
            this.BackingProtocol = backingProtocol;
        }

        /// <summary>
        /// Gets the operation.
        /// </summary>
        /// <value>The operation.</value>
        public TOperation Operation { get; private set; }

        /// <summary>
        /// Gets the backing protocol.
        /// </summary>
        /// <value>The backing protocol.</value>
        public ISyncAndAsyncReturningProtocol<TOperation, TReturn> BackingProtocol { get; private set; }
    }
}
