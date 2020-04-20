// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IDataOperationsProtocolFactory.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Protocol.Domain
{
    using System.Collections.Generic;

    using Naos.Protocol.Domain.Internal;

    /// <summary>
    /// Interface to get the protocols for the basic stream data operations.
    /// </summary>
    /// <typeparam name="TKey">Type of key used.</typeparam>
    public interface IDataOperationsProtocolFactory<TKey>
    {
        /// <summary>
        /// Builds the protocol for <see cref="GetTagsFromObjectOp{TObject}"/>.
        /// </summary>
        /// <typeparam name="TObject">The type of the t object.</typeparam>
        /// <returns>Protocol for <see cref="GetTagsFromObjectOp{TObject}"/>.</returns>
        IReturningProtocol<GetTagsFromObjectOp<TObject>, IReadOnlyDictionary<string, string>> BuildGetTagsFromObjectProtocol<TObject>();

        /// <summary>
        /// Builds the protocol for <see cref="GetIdFromObjectOp{TKey,TObject}"/>.
        /// </summary>
        /// <typeparam name="TObject">The type of the object.</typeparam>
        /// <returns>Protocol for <see cref="GetIdFromObjectOp{TKey,TObject}"/>.</returns>
        IReturningProtocol<GetIdFromObjectOp<TKey, TObject>, TKey> BuildGetIdFromObjectProtocol<TObject>();

        /// <summary>
        /// Gets the <see cref="PutOp{TObject}"/> protocol.
        /// </summary>
        /// <typeparam name="TObject">The type of the object.</typeparam>
        /// <returns>TProtocol.</returns>
        IVoidProtocol<PutOp<TObject>> BuildPutProtocol<TObject>();

        /// <summary>
        /// Gets the <see cref="GetLatestByIdOp{TKey,TObject}"/> protocol.
        /// </summary>
        /// <typeparam name="TObject">The type of the object.</typeparam>
        /// <returns>TProtocol.</returns>
        IReturningProtocol<GetLatestByIdOp<TKey, TObject>, TObject> BuildGetLatestByKeyProtocol<TObject>();
    }
}
