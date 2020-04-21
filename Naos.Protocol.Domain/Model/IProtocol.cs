// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IProtocol.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Protocol.Domain
{
    using System.Threading.Tasks;

    /// <summary>
    /// Fully open interface.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1040:AvoidEmptyInterfaces", Justification = "Need a top level interface for collections.")]
    public interface IProtocol
    {
    }

    /// <summary>
    /// Executes an operation.
    /// </summary>
    /// <typeparam name="TOperation">The type of the operation.</typeparam>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1040:AvoidEmptyInterfaces", Justification = "Prefer interface over attribute here.")]
    public interface IProtocol<TOperation> : IProtocol
        where TOperation : IOperation
    {
    }

    /// <summary>
    /// Executes a <see cref="VoidOperationBase"/> synchronously.
    /// </summary>
    /// <typeparam name="TOperation">The type of the operation.</typeparam>
    public interface IVoidProtocol<TOperation> : IProtocol<TOperation>
        where TOperation : VoidOperationBase
    {
        /// <summary>
        /// Executes the operation.
        /// </summary>
        /// <param name="operation">The operation.</param>
        void Execute(TOperation operation);
    }

    /// <summary>
    /// Executes a <see cref="VoidOperationBase"/> asynchronously.
    /// </summary>
    /// <typeparam name="TOperation">The type of the operation.</typeparam>
    public interface IAsyncVoidProtocol<TOperation> : IProtocol<TOperation>
        where TOperation : VoidOperationBase
    {
        /// <summary>
        /// Executes the operation.
        /// </summary>
        /// <param name="operation">The operation.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task ExecuteAsync(TOperation operation);
    }

    /// <summary>
    /// Executes a <see cref="VoidOperationBase"/> both synchronously and asynchronously.
    /// </summary>
    /// <typeparam name="TOperation">The type of the operation.</typeparam>
    public interface ISyncAndAsyncVoidProtocol<TOperation> : IVoidProtocol<TOperation>, IAsyncVoidProtocol<TOperation>
        where TOperation : VoidOperationBase
    {
    }

    /// <summary>
    /// Executes a <see cref="ReturningOperationBase{TReturn}"/> synchronously.
    /// </summary>
    /// <typeparam name="TOperation">The type of the operation.</typeparam>
    /// <typeparam name="TReturn">The type that the operation returns.</typeparam>
    public interface IReturningProtocol<TOperation, TReturn> : IProtocol<TOperation>
        where TOperation : ReturningOperationBase<TReturn>
    {
        /// <summary>
        /// Executes the operation.
        /// </summary>
        /// <param name="operation">The operation.</param>
        /// <returns>The result of the operation.</returns>
        TReturn Execute(TOperation operation);
    }

    /// <summary>
    /// Executes a <see cref="ReturningOperationBase{TReturn}"/> asynchronously.
    /// </summary>
    /// <typeparam name="TOperation">The type of the operation.</typeparam>
    /// <typeparam name="TReturn">The type that the operation returns.</typeparam>
    public interface IAsyncReturningProtocol<TOperation, TReturn> : IProtocol<TOperation>
        where TOperation : ReturningOperationBase<TReturn>
    {
        /// <summary>
        /// Executes the operation.
        /// </summary>
        /// <param name="operation">The operation.</param>
        /// <returns>The result of the operation.</returns>
        Task<TReturn> ExecuteAsync(TOperation operation);
    }

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
