// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IReturningOperation{TReturn}.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Protocol.Domain
{
    using Naos.CodeAnalysis.Recipes;

    /// <summary>
    /// Interface necessary for a returning operation to connect to a protocol.
    /// </summary>
    /// <typeparam name="TReturn">The type of the return of the execution of the operation.</typeparam>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1040:AvoidEmptyInterfaces", Justification = NaosSuppressBecause.CA1040_AvoidEmptyInterfaces_NeedToIdentifyGroupOfTypesAndPreferInterfaceOverAttribute)]
    public interface IReturningOperation<TReturn> : IOperation
    {
    }
}