// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IEvent{TId}.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Protocol.Domain
{
    using Naos.CodeAnalysis.Recipes;
    using OBeautifulCode.Type;

    /// <summary>
    /// Most basic declaration of an event.
    /// </summary>
    /// <typeparam name="TId">Type of the identifier.</typeparam>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1040:AvoidEmptyInterfaces", Justification = NaosSuppressBecause.CA1040_AvoidEmptyInterfaces_NeedToIdentifyGroupOfTypesAndPreferInterfaceOverAttribute)]
    public interface IEvent<TId> : IEvent, IIdentifiableBy<TId>
    {
    }
}