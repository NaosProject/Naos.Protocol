// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ISyncAndAsyncReturningProtocol{TOperation,TReturn}.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Protocol.Domain
{
    /// <summary>
    /// Executes a <see cref="ReturningOperationBase{TReturn}"/> both synchronously and asynchronously.
    /// </summary>
    /// <typeparam name="TOperation">The type of the operation.</typeparam>
    /// <typeparam name="TReturn">The type that the operation returns.</typeparam>
    public interface ISyncAndAsyncReturningProtocol<TOperation, TReturn> : IReturningProtocol<TOperation, TReturn>, IAsyncReturningProtocol<TOperation, TReturn>
        where TOperation : ReturningOperationBase<TReturn>
    {
    }
}