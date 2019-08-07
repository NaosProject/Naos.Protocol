// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IProtocol.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Protocol.Domain
{
    /// <summary>
    /// Executes an operation.
    /// </summary>
    /// <typeparam name="TOperation">The type of the operation.</typeparam>
    public interface IProtocol<in TOperation>
        where TOperation : IOperation
    {
    }

    /// <summary>
    /// Executes a <see cref="VoidOperationBase"/>.
    /// </summary>
    /// <typeparam name="TOperation">The type of the operation.</typeparam>
    public interface IVoidProtocol<in TOperation> : IProtocol<TOperation>
        where TOperation : VoidOperationBase
    {
        /// <summary>
        /// Executes the operation.
        /// </summary>
        /// <param name="operation">The operation.</param>
        void Execute(TOperation operation);
    }

    /// <summary>
    /// Executes a <see cref="ReturningOperationBase{TReturn}"/>.
    /// </summary>
    /// <typeparam name="TOperation">The type of the operation.</typeparam>
    /// <typeparam name="TReturn">The type that the operation return.</typeparam>
    public interface IReturningProtocol<in TOperation, out TReturn> : IProtocol<TOperation>
        where TOperation : ReturningOperationBase<TReturn>
    {
        /// <summary>
        /// Executes the operation.
        /// </summary>
        /// <param name="operation">The operation.</param>
        /// <returns>The result of the operation.</returns>
        TReturn Execute(TOperation operation);
    }
}