// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IProtocolFactoryStreamObjectOperations{TId}.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Protocol.Domain
{
    using System.Collections.Generic;

    /// <summary>
    /// Interface to get the protocols for the basic stream data operations.
    /// </summary>
    /// <typeparam name="TId">Type of ID used.</typeparam>
    public interface IProtocolFactoryStreamObjectOperations<TId>
    {
        /// <summary>
        /// Builds the protocol for <see cref="GetTagsFromObjectOp{TObject}"/>.
        /// </summary>
        /// <typeparam name="TObject">The type of the t object.</typeparam>
        /// <returns>Protocol for <see cref="GetTagsFromObjectOp{TObject}"/>.</returns>
        ISyncAndAsyncReturningProtocol<GetTagsFromObjectOp<TObject>, IReadOnlyDictionary<string, string>> BuildGetTagsFromObjectProtocol<TObject>();

        /// <summary>
        /// Builds the protocol for <see cref="GetIdFromObjectOp{TId,TObject}"/>.
        /// </summary>
        /// <typeparam name="TObject">The type of the object.</typeparam>
        /// <returns>Protocol for <see cref="GetIdFromObjectOp{TId,TObject}"/>.</returns>
        ISyncAndAsyncReturningProtocol<GetIdFromObjectOp<TId, TObject>, TId> BuildGetIdFromObjectProtocol<TObject>();

        /// <summary>
        /// Gets the <see cref="PutOp{TObject}"/> protocol.
        /// </summary>
        /// <typeparam name="TObject">The type of the object.</typeparam>
        /// <returns>TProtocol.</returns>
        ISyncAndAsyncVoidProtocol<PutOp<TObject>> BuildPutProtocol<TObject>();

        /// <summary>
        /// Gets the <see cref="GetLatestByIdAndTypeOp{TId,TObject}"/> protocol.
        /// </summary>
        /// <typeparam name="TObject">The type of the object.</typeparam>
        /// <returns>TProtocol.</returns>
        ISyncAndAsyncReturningProtocol<GetLatestByIdAndTypeOp<TId, TObject>, TObject> BuildGetLatestByIdAndTypeProtocol<TObject>();
    }
}
