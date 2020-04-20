// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GetIdFromObjectOp{TId,TObject}.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Protocol.Domain
{
    using System;

    /// <summary>
    /// Abstract base of an operation.
    /// </summary>
    /// <typeparam name="TId">Type of ID being used.</typeparam>
    /// <typeparam name="TObject">Type of object.</typeparam>
    public class GetIdFromObjectOp<TId, TObject> : ReturningOperationBase<TId>, IHaveKeyType
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetIdFromObjectOp{TKey,TObject}"/> class.
        /// </summary>
        /// <param name="objectToDetermineIdFrom">The object to determine key from.</param>
        public GetIdFromObjectOp(
            TObject objectToDetermineIdFrom)
        {
            this.ObjectToDetermineIdFrom = objectToDetermineIdFrom;
        }

        /// <summary>
        /// Gets the object to determine key from.
        /// </summary>
        /// <value>The object to determine key from.</value>
        public TObject ObjectToDetermineIdFrom { get; private set; }

        /// <inheritdoc />
        public Type IdType => typeof(TId);
    }
}
