// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IGetProtocol.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Protocol.Domain
{
    /// <summary>
    /// Gets a protocol.
    /// </summary>
    /// <typeparam name="TOperation">The type of the operation.</typeparam>
    public interface IGetProtocol<in TOperation>
        where TOperation : IOperation
    {
        /// <summary>
        /// Gets the protocol.
        /// </summary>
        /// <returns>
        /// The protocol.
        /// </returns>
        IProtocol<TOperation> Get();
    }

    /// <summary>
    /// Gets a protocol that executes a <see cref="VoidOperationBase"/>.
    /// </summary>
    /// <typeparam name="TOperation">The type of the operation.</typeparam>
    public interface IGetVoidProtocol<in TOperation>
        where TOperation : VoidOperationBase
    {
        /// <summary>
        /// Gets the protocol.
        /// </summary>
        /// <returns>
        /// The protocol.
        /// </returns>
        IVoidProtocol<TOperation> Get();
    }

    /// <summary>
    /// Gets a protocol that executes a <see cref="ReturningOperationBase{TReturn}"/>.
    /// </summary>
    /// <typeparam name="TOperation">The type of the operation.</typeparam>
    /// <typeparam name="TReturn">The type that the operation return.</typeparam>
    public interface IGetReturningProtocol<in TOperation, out TReturn>
        where TOperation : ReturningOperationBase<TReturn>
    {
        /// <summary>
        /// Gets the protocol.
        /// </summary>
        /// <returns>
        /// The protocol.
        /// </returns>
        IReturningProtocol<TOperation, TReturn> Get();
    }
}