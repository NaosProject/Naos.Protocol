// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IProtocolStreamObjectOperations{TId,TObject}.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Protocol.Domain
{
    /// <summary>
    /// Interface to protocol the basic stream data operations.
    /// </summary>
    /// <typeparam name="TId">Type of ID used.</typeparam>
    /// <typeparam name="TObject">Type of object used.</typeparam>
    public interface IProtocolStreamObjectOperations<TId, TObject> :
        ISyncAndAsyncVoidProtocol<PutOp<TObject>>,
        ISyncAndAsyncReturningProtocol<GetLatestByIdAndTypeOp<TId, TObject>, TObject>
    {
    }
}
