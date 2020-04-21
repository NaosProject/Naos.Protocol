// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SingleStreamLocatorProtocol{TId}.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Protocol.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <summary>
    /// Implements <see cref="IProtocolStreamLocator{TId}"/> using a single provided <see cref="StreamLocatorBase"/>.
    /// </summary>
    /// <typeparam name="TId">Type of ID.</typeparam>
    public sealed partial class SingleStreamLocatorProtocol<TId>
        : IProtocolStreamLocator<TId>
    {
        private readonly StreamLocatorBase streamLocator;

        /// <summary>
        /// Initializes a new instance of the <see cref="SingleStreamLocatorProtocol{TId}"/> class.
        /// </summary>
        /// <param name="streamLocator">The SQL stream locator.</param>
        public SingleStreamLocatorProtocol(StreamLocatorBase streamLocator)
        {
            this.streamLocator = streamLocator ?? throw new ArgumentNullException(nameof(streamLocator));
        }

        /// <inheritdoc />
        public StreamLocatorBase Execute(
            GetStreamLocatorByIdOp<TId> operation)
        {
            return this.streamLocator;
        }

        /// <inheritdoc />
        public async Task<StreamLocatorBase> ExecuteAsync(
            GetStreamLocatorByIdOp<TId> operation)
        {
            return await Task.FromResult(this.Execute(operation));
        }

        /// <inheritdoc />
        public IReadOnlyCollection<StreamLocatorBase> Execute(
            GetAllStreamLocatorsOp operation)
        {
            return new[]
                   {
                       this.streamLocator,
                   };
        }

        /// <inheritdoc />
        public async Task<IReadOnlyCollection<StreamLocatorBase>> ExecuteAsync(
            GetAllStreamLocatorsOp operation)
        {
            return await Task.FromResult(this.Execute(operation));
        }
    }
}
