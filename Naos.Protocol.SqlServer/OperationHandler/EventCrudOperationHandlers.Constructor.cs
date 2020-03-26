// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EventCrudOperationHandlers.Constructor.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Protocol.SqlServer
{
    using System;
    using Naos.Protocol.Domain;
    using Naos.Protocol.SqlServer.Internal;

#pragma warning disable CS1710 // XML comment has a duplicate typeparam tag
    /// <summary>
    /// TODO: Starting point for new project.
    /// </summary>
    /// <typeparam name="TKey">The type of key.</typeparam>
    /// <typeparam name="TObject">The type of payload.</typeparam>
    public sealed partial class CrudOperationHandlers<TKey, TObject>
#pragma warning restore CS1710 // XML comment has a duplicate typeparam tag
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields", Justification = "Needed in future.")]
        private readonly IReturningProtocol<DetermineLocatorByKeyOp<TKey, LocatorBase>, LocatorBase> keyLocatorProtocol;

        /// <summary>
        /// Initializes a new instance of the <see cref="CrudOperationHandlers{TKey, TObject}"/> class.
        /// </summary>
        /// <param name="keyLocatorProtocol">Protocol to determine the locator by key (supports sharding).</param>
        public CrudOperationHandlers(IReturningProtocol<DetermineLocatorByKeyOp<TKey, LocatorBase>, LocatorBase> keyLocatorProtocol)
        {
            this.keyLocatorProtocol = keyLocatorProtocol ?? throw new ArgumentNullException(nameof(keyLocatorProtocol));
        }
    }
}
