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
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1040:AvoidEmptyInterfaces", Justification = "Prefer interface over attribute here.")]
    public interface IGetProtocol
    {
    }

    /// <summary>
    /// Gets a protocol.
    /// </summary>
    /// <typeparam name="TOperation">The type of the operation.</typeparam>
    public interface IGetProtocol<TOperation> : IGetProtocol
        where TOperation : IOperation
    {
        /// <summary>
        /// Gets the protocol.
        /// </summary>
        /// <returns>
        /// The protocol.
        /// </returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Justification = "Prefer a method here.")]
        IProtocol<TOperation> GetProtocol();
    }

    /// <summary>
    /// Gets a protocol that executes a <see cref="VoidOperationBase"/>.
    /// </summary>
    /// <typeparam name="TOperation">The type of the operation.</typeparam>
    public interface IGetVoidProtocol<TOperation> : IGetProtocol
        where TOperation : VoidOperationBase
    {
        /// <summary>
        /// Gets the protocol.
        /// </summary>
        /// <returns>
        /// The protocol.
        /// </returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Justification = "Prefer a method here.")]
        IVoidProtocol<TOperation> GetProtocol();

        /// <summary>
        /// Gets the async protocol.
        /// </summary>
        /// <returns>
        /// The async protocol.
        /// </returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Justification = "Prefer a method.")]
        IAsyncVoidProtocol<TOperation> GetProtocolAsync();
    }

    /// <summary>
    /// Gets a protocol that executes a <see cref="ReturningOperationBase{TReturn}"/>.
    /// </summary>
    /// <typeparam name="TOperation">The type of the operation.</typeparam>
    /// <typeparam name="TReturn">The type that the operation return.</typeparam>
    public interface IGetReturningProtocol<TOperation, TReturn> : IGetProtocol
        where TOperation : ReturningOperationBase<TReturn>
    {
        /// <summary>
        /// Gets the protocol.
        /// </summary>
        /// <returns>
        /// The protocol.
        /// </returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Justification = "Prefer a method here.")]
        IReturningProtocol<TOperation, TReturn> GetProtocol();

        /// <summary>
        /// Gets the async protocol.
        /// </summary>
        /// <returns>
        /// The async protocol.
        /// </returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Justification = "Prefer a method.")]
        IAsyncReturningProtocol<TOperation, TReturn> GetAsync();
    }
}