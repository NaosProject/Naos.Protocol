// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IGetReturningProtocol{TOperation,TReturn}.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Protocol.Domain
{
    /// <summary>
    /// Gets a protocol that executes a <see cref="ReturningOperationBase{TReturn}"/>.
    /// </summary>
    /// <typeparam name="TOperation">The type of the operation.</typeparam>
    /// <typeparam name="TReturn">The type that the operation return.</typeparam>
    public interface IGetReturningProtocol<TOperation, TReturn> : IGetProtocol
        where TOperation : IReturningOperation<TReturn>
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
        /// Gets the protocol.
        /// </summary>
        /// <param name="operation">The operation needing a protocol.</param>
        /// <returns>
        /// The protocol.
        /// </returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Justification = "Prefer a method here.")]
        IReturningProtocol<TOperation, TReturn> GetProtocol(TOperation operation);

        /// <summary>
        /// Gets the async protocol.
        /// </summary>
        /// <returns>
        /// The async protocol.
        /// </returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Justification = "Prefer a method.")]
        IAsyncReturningProtocol<TOperation, TReturn> GetProtocolAsync();

        /// <summary>
        /// Gets the async protocol.
        /// </summary>
        /// <param name="operation">The operation needing a protocol.</param>
        /// <returns>
        /// The async protocol.
        /// </returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Justification = "Prefer a method.")]
        IAsyncReturningProtocol<TOperation, TReturn> GetProtocolAsync(TOperation operation);
    }
}
