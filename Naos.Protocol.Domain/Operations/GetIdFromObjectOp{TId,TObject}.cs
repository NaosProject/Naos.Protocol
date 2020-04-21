// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GetIdFromObjectOp{TId,TObject}.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Protocol.Domain
{
    /// <summary>
    /// Get the ID for a stream from the provided object.
    /// </summary>
    /// <typeparam name="TId">Type of ID being used.</typeparam>
    /// <typeparam name="TObject">Type of object.</typeparam>
    public class GetIdFromObjectOp<TId, TObject> : ReturningOperationBase<TId>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetIdFromObjectOp{TId,TObject}"/> class.
        /// </summary>
        /// <param name="objectToDetermineIdFrom">The object to determine ID from.</param>
        public GetIdFromObjectOp(
            TObject objectToDetermineIdFrom)
        {
            this.ObjectToDetermineIdFrom = objectToDetermineIdFrom;
        }

        /// <summary>
        /// Gets the object to determine ID from.
        /// </summary>
        /// <value>The object to determine ID from.</value>
        public TObject ObjectToDetermineIdFrom { get; private set; }
    }
}
