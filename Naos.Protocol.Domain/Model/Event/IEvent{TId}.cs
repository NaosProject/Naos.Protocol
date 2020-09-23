// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IEvent{TId}.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Protocol.Domain
{
    using OBeautifulCode.Type;

    /// <summary>
    /// Interface IEvent to force events to have minimal specific data.
    /// Implements the <see cref="OBeautifulCode.Type.IIdentifiableBy{TId}" />.
    /// Implements the <see cref="OBeautifulCode.Type.IHaveTags" />.
    /// Implements the <see cref="OBeautifulCode.Type.IHaveTimestampUtc" />.
    /// </summary>
    /// <typeparam name="TId">The type of the identifier.</typeparam>
    /// <seealso cref="OBeautifulCode.Type.IIdentifiableBy{TId}" />
    /// <seealso cref="OBeautifulCode.Type.IHaveTags" />
    /// <seealso cref="OBeautifulCode.Type.IHaveTimestampUtc" />
    public interface IEvent<TId> : IIdentifiableBy<TId>, IHaveTags, IHaveTimestampUtc
    {
    }
}