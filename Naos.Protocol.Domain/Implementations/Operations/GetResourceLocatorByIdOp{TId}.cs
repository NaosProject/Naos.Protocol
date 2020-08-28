﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GetResourceLocatorByIdOp{TId}.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Protocol.Domain
{
    /// <summary>
    /// Get the <see cref="ResourceLocatorBase"/> by the ID, this support ID based sharding.
    /// </summary>
    /// <typeparam name="TId">Type of ID being used.</typeparam>
    public class GetResourceLocatorByIdOp<TId> : ReturningOperationBase<ResourceLocatorBase>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetResourceLocatorByIdOp{TId}"/> class.
        /// </summary>
        /// <param name="id">The ID.</param>
        public GetResourceLocatorByIdOp(
            TId id)
        {
            this.Id = id;
        }

        /// <summary>
        /// Gets the ID.
        /// </summary>
        /// <value>The ID.</value>
        public TId Id { get; private set; }
    }
}
