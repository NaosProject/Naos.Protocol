// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IHaveKey.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Protocol.Domain
{
    /// <summary>
    /// Model interface for objects that have a key.
    /// </summary>
    /// <typeparam name="TKey">The type of key of the object.</typeparam>
    public interface IHaveKey<TKey>
    {
        /// <summary>
        /// Gets the key.
        /// </summary>
        /// <value>The key.</value>
        TKey Key { get; }
    }
}
