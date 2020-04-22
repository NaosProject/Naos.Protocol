﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GetLatestByIdAndTypeOp{TId,TObject}.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Protocol.Domain
{
    /// <summary>
    /// Operation to get the most recent object of a certain ID and given type.
    /// </summary>
    /// <typeparam name="TId">The type of the ID of the object.</typeparam>
    /// <typeparam name="TObject">The type of the object.</typeparam>
    /// <seealso cref="Naos.Protocol.Domain.ReturningOperationBase{TObject}" />
    public class GetLatestByIdAndTypeOp<TId, TObject> : ReturningOperationBase<TObject>, IHaveId<TId>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetLatestByIdAndTypeOp{TId,TObject}"/> class.
        /// </summary>
        /// <param name="id">The ID.</param>
        /// <param name="typeVersionMatchStrategy">The type version match strategy.</param>
        public GetLatestByIdAndTypeOp(
            TId id,
            TypeVersionMatchStrategy typeVersionMatchStrategy = TypeVersionMatchStrategy.Any)
        {
            this.Id = id;
            this.TypeVersionMatchStrategy = typeVersionMatchStrategy;
        }

        /// <inheritdoc />
        public TId Id { get; private set; }

        /// <summary>
        /// Gets the type version match strategy.
        /// </summary>
        /// <value>The type version match strategy.</value>
        public TypeVersionMatchStrategy TypeVersionMatchStrategy { get; private set; }
    }
}
