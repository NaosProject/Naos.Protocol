// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IVoidOperation.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Protocol.Domain
{
    using Naos.Protocol.Domain.Internal;

    /// <summary>
    /// Interface necessary for a void operation to connect to a protocol.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1040:AvoidEmptyInterfaces", Justification = NaosSuppressBecause.CA1040_AvoidEmptyInterfaces_NeedToIdentifyGroupOfTypesAndPreferInterfaceOverAttribute)]
    public interface IVoidOperation : IOperation
    {
    }
}