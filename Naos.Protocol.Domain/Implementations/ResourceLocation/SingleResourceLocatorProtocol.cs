// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SingleResourceLocatorProtocol.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Protocol.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <summary>
    /// Implements <see cref="IProtocolResourceLocator"/> using a single provided <see cref="ResourceLocatorBase"/>.
    /// </summary>
    public sealed partial class SingleResourceLocatorProtocol
        : IProtocolResourceLocator
    {
        private readonly IResourceLocator resourceLocator;

        /// <summary>
        /// Initializes a new instance of the <see cref="SingleResourceLocatorProtocol"/> class.
        /// </summary>
        /// <param name="resourceLocator">The SQL stream locator.</param>
        public SingleResourceLocatorProtocol(IResourceLocator resourceLocator)
        {
            this.resourceLocator = resourceLocator ?? throw new ArgumentNullException(nameof(resourceLocator));
        }

        /// <inheritdoc />
        public IReadOnlyCollection<IResourceLocator> Execute(
            GetAllResourceLocatorsOp operation)
        {
            return new[]
                   {
                       this.resourceLocator,
                   };
        }

        /// <inheritdoc />
        public async Task<IReadOnlyCollection<IResourceLocator>> ExecuteAsync(
            GetAllResourceLocatorsOp operation)
        {
            return await Task.FromResult(this.Execute(operation));
        }

        /// <inheritdoc />
        public ISyncAndAsyncReturningProtocol<GetResourceLocatorByIdOp<TId>, IResourceLocator> GetResourceLocatorByIdProtocol<TId>()
        {
            return new LambdaReturningProtocol<GetResourceLocatorByIdOp<TId>, IResourceLocator>(_ => this.resourceLocator);
        }
    }
}
