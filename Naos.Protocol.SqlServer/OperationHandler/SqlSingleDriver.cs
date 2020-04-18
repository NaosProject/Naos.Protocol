// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SqlSingleDriver.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Protocol.SqlServer
{
    using System;
    using System.Collections.Generic;
    using Naos.Protocol.Domain;
    using Naos.Protocol.SqlServer.Internal;

    /// <summary>
    /// TODO: Starting point for new project.
    /// </summary>
    public sealed partial class SqlSingleDriver
        : IReturningProtocol<GetStreamLocatorByTypeOp, StreamLocatorBase>, IReturningProtocol<GetStreamLocatorByKeyOp<string>, StreamLocatorBase>, IReturningProtocol<GetAllStreamLocatorsOp, IReadOnlyCollection<StreamLocatorBase>>
    {
        private SqlStreamLocator sqlStreamLocator;

        /// <summary>
        /// Initializes a new instance of the <see cref="SqlSingleDriver"/> class.
        /// </summary>
        /// <param name="sqlStreamLocator">The SQL stream locator.</param>
        public SqlSingleDriver(SqlStreamLocator sqlStreamLocator)
        {
            this.sqlStreamLocator = sqlStreamLocator ?? throw new ArgumentNullException(nameof(sqlStreamLocator));
        }

        /// <inheritdoc />
        public StreamLocatorBase Execute(
            GetStreamLocatorByTypeOp operation)
        {
            return this.sqlStreamLocator;
        }

        /// <inheritdoc />
        public StreamLocatorBase Execute(
            GetStreamLocatorByKeyOp<string> operation)
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
