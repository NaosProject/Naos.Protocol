// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IProtocolResourceLocator{TId}.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Protocol.Domain
{
    using System.Collections.Generic;

    /// <summary>
    /// Set of common protocols around <see cref="IResourceLocator" /> for use with protocols accessing communication or storage.
    /// </summary>
    /// <typeparam name="TId">The type of the ID of the stream.</typeparam>
    public interface IProtocolResourceLocator<TId>
        : ISyncAndAsyncReturningProtocol<GetResourceLocatorByIdOp<TId>, IResourceLocator>,
          ISyncAndAsyncReturningProtocol<GetAllResourceLocatorsOp, IReadOnlyCollection<IResourceLocator>>
    {
    }
}
