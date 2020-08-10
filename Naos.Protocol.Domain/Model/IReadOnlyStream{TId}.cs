// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IReadOnlyStream{TId}.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Protocol.Domain
{
    using Naos.Protocol.Domain.Internal;

    /// <summary>
    /// Stream interface, a stream is a list of objects ordered by timestamp, only read operations are supported.
    /// </summary>
    /// <typeparam name="TId">The type of ID of the stream.</typeparam>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1711:IdentifiersShouldNotHaveIncorrectSuffix", Justification = NaosSuppressBecause.CA1711_IdentifiersShouldNotHaveIncorrectSuffix_TypeNameAddedAsSuffixForTestsWhereTypeIsPrimaryConcern)]
    public interface IReadOnlyStream<TId>
        : IProtocolFactoryStreamObjectReadOperations<TId>
    {
        /// <summary>
        /// Gets the name of the stream.
        /// </summary>
        /// <value>The name of the stream.</value>
        string Name { get; }

        /// <summary>
        /// Gets the stream locator protocol.
        /// </summary>
        /// <value>The stream locator protocol.</value>
        IProtocolStreamLocator<TId> StreamLocatorProtocol { get; }
    }
}
