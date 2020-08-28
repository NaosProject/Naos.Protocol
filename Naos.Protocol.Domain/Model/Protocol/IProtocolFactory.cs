// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IProtocolFactory.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Protocol.Domain
{
    /// <summary>
    /// Set of common protocols around a general purpose protocol factory concept.
    /// </summary>
    public interface IProtocolFactory
        : ISyncAndAsyncReturningProtocol<GetProtocolByTypeOp, IProtocol>
    {
    }
}
