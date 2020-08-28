// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IGetProtocol{TOperation}.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Protocol.Domain
{
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
}
