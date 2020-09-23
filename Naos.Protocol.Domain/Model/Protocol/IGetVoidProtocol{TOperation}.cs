// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IGetVoidProtocol{TOperation}.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Protocol.Domain
{
    /// <summary>
    /// Gets a protocol that executes a <see cref="IVoidOperation"/>.
    /// </summary>
    /// <typeparam name="TOperation">The type of the operation.</typeparam>
    public interface IGetVoidProtocol<TOperation> : IGetProtocol
        where TOperation : IVoidOperation
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
        /// Gets the protocol.
        /// </summary>
        /// <param name="operation">The operation needing a protocol.</param>
        /// <returns>
        /// The protocol.
        /// </returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Justification = "Prefer a method here.")]
        IVoidProtocol<TOperation> GetProtocol(TOperation operation);

        /// <summary>
        /// Gets the async protocol.
        /// </summary>
        /// <returns>
        /// The async protocol.
        /// </returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Justification = "Prefer a method.")]
        IAsyncVoidProtocol<TOperation> GetProtocolAsync();

        /// <summary>
        /// Gets the async protocol.
        /// </summary>
        /// <param name="operation">The operation needing a protocol.</param>
        /// <returns>
        /// The async protocol.
        /// </returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Justification = "Prefer a method.")]
        IAsyncVoidProtocol<TOperation> GetProtocolAsync(TOperation operation);
    }
}