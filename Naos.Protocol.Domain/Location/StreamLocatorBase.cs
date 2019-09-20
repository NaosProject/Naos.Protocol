// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StreamLocatorBase.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Protocol.Domain
{
    /// <summary>
    /// Locator to get the necessary connection information for the vault.
    /// </summary>
    public abstract class StreamLocatorBase : LocatorBase
    {
        // connection to get to a stream (all shard targeting info needs to be in here)
    }
}