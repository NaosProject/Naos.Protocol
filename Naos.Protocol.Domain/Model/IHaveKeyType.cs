// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IHaveKeyType.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Protocol.Domain
{
    using System;
    using Naos.Protocol.Domain.Internal;

    /// <summary>
    /// Interface that exposes the <see cref="Type"/> of the key.
    /// </summary>
    public interface IHaveKeyType
    {
        /// <summary>
        /// Gets the type of the key.
        /// </summary>
        /// <value>The type of the key.</value>
        Type KeyType { get; }
    }
}
