// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SingleStreamLocatorProtocol{TId}.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Protocol.Domain
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// TODO: Starting point for new project.
    /// </summary>
    /// <typeparam name="TId">Type of key.</typeparam>
    public sealed partial class SingleStreamLocatorProtocol<TId>
        : IProtocolStreamLocator<TId>
    {
        private readonly StreamLocatorBase sqlStreamLocator;

        /// <summary>
        /// Initializes a new instance of the <see cref="SingleStreamLocatorProtocol{TId}"/> class.
        /// </summary>
        /// <param name="streamLocator">The SQL stream locator.</param>
        public SingleStreamLocatorProtocol(StreamLocatorBase streamLocator)
        {
            this.sqlStreamLocator = streamLocator ?? throw new ArgumentNullException(nameof(streamLocator));
        }

        /// <inheritdoc />
        public StreamLocatorBase Execute(
            GetStreamLocatorByIdOp<TId> operation)
        {
            return this.sqlStreamLocator;
        }

        /// <inheritdoc />
        public IReadOnlyCollection<StreamLocatorBase> Execute(
            GetAllStreamLocatorsOp operation)
        {
            return new[]
                   {
                       this.sqlStreamLocator,
                   };
        }
    }
}
