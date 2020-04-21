// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IHaveId{TId}.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Protocol.Domain
{
    /// <summary>
    /// Model interface for objects that have a ID.
    /// </summary>
    /// <typeparam name="TId">The type of ID of the object.</typeparam>
    public interface IHaveId<TId>
    {
        /// <summary>
        /// Gets the ID.
        /// </summary>
        /// <value>The ID.</value>
        TId Id { get; }
    }
}
