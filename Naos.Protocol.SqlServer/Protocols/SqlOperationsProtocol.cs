// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SqlOperationsProtocol.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Protocol.SqlServer
{
    using System;
    using OBeautifulCode.Assertion.Recipes;

    /// <summary>
    /// Sql Operation Protocol.
    /// </summary>
    public partial class SqlOperationsProtocol : IProtocolSqlOperations
    {
        private readonly ISqlLocator sqlLocator;
        private readonly TimeSpan defaultConnectionTimeout;
        private readonly TimeSpan defaultCommandTimeout;

        /// <summary>
        /// Initializes a new instance of the <see cref="SqlOperationsProtocol"/> class.
        /// </summary>
        /// <param name="sqlLocator">The SQL locator.</param>
        /// <param name="defaultConnectionTimeout">The default connection timeout.</param>
        /// <param name="defaultCommandTimeout">The default command timeout.</param>
        public SqlOperationsProtocol(
            ISqlLocator sqlLocator,
            TimeSpan defaultConnectionTimeout,
            TimeSpan defaultCommandTimeout)
        {
            sqlLocator.MustForArg(nameof(sqlLocator)).NotBeNull();

            this.sqlLocator = sqlLocator;

            this.defaultConnectionTimeout = defaultConnectionTimeout;
            this.defaultCommandTimeout = defaultCommandTimeout;
        }
    }
}