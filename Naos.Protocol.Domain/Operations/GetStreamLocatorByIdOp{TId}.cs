// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GetStreamLocatorByIdOp{TId}.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Protocol.Domain
{
    /// <summary>
    /// Abstract base of an operation.
    /// </summary>
    /// <typeparam name="TId">Type of ID being used.</typeparam>
    public class GetStreamLocatorByIdOp<TId> : ReturningOperationBase<StreamLocatorBase>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetStreamLocatorByIdOp{TKey}"/> class.
        /// </summary>
        /// <param name="id">The key.</param>
        public GetStreamLocatorByIdOp(
            TId id)
        {
            this.Id = id;
        }

        /// <summary>
        /// Gets the key.
        /// </summary>
        /// <value>The key.</value>
        public TId Id { get; private set; }
    }
}
