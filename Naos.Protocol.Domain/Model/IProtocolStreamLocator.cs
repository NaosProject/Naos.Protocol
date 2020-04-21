// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IProtocolStreamLocator.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Protocol.Domain
{
    using System.Collections.Generic;

    /// <summary>
    /// Set of common protocols around <see cref="StreamLocatorBase" /> for use with <see cref="IStream{TId}" />.
    /// </summary>
    /// <typeparam name="TId">The type of the ID of the stream.</typeparam>
    public interface IProtocolStreamLocator<TId>
        : ISyncAndAsyncReturningProtocol<GetStreamLocatorByIdOp<TId>, StreamLocatorBase>,
          ISyncAndAsyncReturningProtocol<GetAllStreamLocatorsOp, IReadOnlyCollection<StreamLocatorBase>>
    {
    }
}
