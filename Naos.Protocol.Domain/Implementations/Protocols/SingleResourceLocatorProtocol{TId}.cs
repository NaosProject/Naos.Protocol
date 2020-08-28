// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SingleResourceLocatorProtocol{TId}.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Protocol.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <summary>
    /// Implements <see cref="IProtocolResourceLocator{TId}"/> using a single provided <see cref="ResourceLocatorBase"/>.
    /// </summary>
    /// <typeparam name="TId">Type of ID.</typeparam>
    public sealed partial class SingleResourceLocatorProtocol<TId>
        : IProtocolResourceLocator<TId>
    {
        private readonly ResourceLocatorBase resourceLocator;

        /// <summary>
        /// Initializes a new instance of the <see cref="SingleResourceLocatorProtocol{TId}"/> class.
        /// </summary>
        /// <param name="resourceLocator">The SQL stream locator.</param>
        public SingleResourceLocatorProtocol(ResourceLocatorBase resourceLocator)
        {
            this.resourceLocator = resourceLocator ?? throw new ArgumentNullException(nameof(resourceLocator));
        }

        /// <inheritdoc />
        public ResourceLocatorBase Execute(
            GetResourceLocatorByIdOp<TId> operation)
        {
            return this.resourceLocator;
        }

        /// <inheritdoc />
        public async Task<ResourceLocatorBase> ExecuteAsync(
            GetResourceLocatorByIdOp<TId> operation)
        {
            return await Task.FromResult(this.Execute(operation));
        }

        /// <inheritdoc />
        public IReadOnlyCollection<ResourceLocatorBase> Execute(
            GetAllResourceLocatorsOp operation)
        {
            return new[]
                   {
                       this.resourceLocator,
                   };
        }

        /// <inheritdoc />
        public async Task<IReadOnlyCollection<ResourceLocatorBase>> ExecuteAsync(
            GetAllResourceLocatorsOp operation)
        {
            return await Task.FromResult(this.Execute(operation));
        }
    }
}
