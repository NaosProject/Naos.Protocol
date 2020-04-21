// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GetLatestByIdOp{TId,TObject}.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Protocol.Domain
{
    /// <summary>
    /// Operation to get the most recent object of a certain ID.
    /// </summary>
    /// <typeparam name="TId">The type of the ID of the object.</typeparam>
    /// <typeparam name="TObject">The type of the object.</typeparam>
    /// <seealso cref="Naos.Protocol.Domain.ReturningOperationBase{TObject}" />
    public class GetLatestByIdOp<TId, TObject> : ReturningOperationBase<TObject>, IHaveId<TId>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetLatestByIdOp{TKey,TObject}"/> class.
        /// </summary>
        /// <param name="id">The ID.</param>
        public GetLatestByIdOp(
            TId id)
        {
            this.Id = id;
        }

        /// <inheritdoc />
        public TId Id { get; private set; }
    }
}
