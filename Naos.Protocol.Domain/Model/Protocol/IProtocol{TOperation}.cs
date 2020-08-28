// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IProtocol{TOperation}.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Protocol.Domain
{
    /// <summary>
    /// Executes an operation.
    /// </summary>
    /// <typeparam name="TOperation">The type of the operation.</typeparam>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1040:AvoidEmptyInterfaces", Justification = "Prefer interface over attribute here.")]
    public interface IProtocol<TOperation> : IProtocol
        where TOperation : IOperation
    {
    }
}
