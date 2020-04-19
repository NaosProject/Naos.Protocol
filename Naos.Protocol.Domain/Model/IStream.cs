// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IStream.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Protocol.Domain
{
    using System.Collections.Generic;

    using Naos.Protocol.Domain.Internal;

    /// <summary>
    /// Stream interface, a stream is a list of objects ordered by timestamp.
    /// </summary>
    /// <typeparam name="TKey">The type of key of the stream.</typeparam>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1711:IdentifiersShouldNotHaveIncorrectSuffix", Justification = NaosSuppressBecause.CA1711_IdentifiersShouldNotHaveIncorrectSuffix_TypeNameAddedAsSuffixForTestsWhereTypeIsPrimaryConcern)]
    public interface IStream<TKey>
        : IReturningProtocol<GetStreamLocatorByKeyOp<TKey>, StreamLocatorBase>,
          IReturningProtocol<GetStreamLocatorByTypeOp, StreamLocatorBase>,
          IReturningProtocol<GetAllStreamLocatorsOp, IReadOnlyCollection<StreamLocatorBase>>,
          IVoidProtocol<CreateStreamOp<TKey>>,
          IDataOperationsProtocolFactory<TKey>
    {
        /// <summary>
        /// Gets the name of the stream.
        /// </summary>
        /// <value>The name of the stream.</value>
        string Name { get; }
    }
}
