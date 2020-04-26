// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SqlLocatorExtensions.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Protocol.SqlServer
{
    using System;
    using System.Data.SqlClient;
    using OBeautifulCode.Assertion.Recipes;
    using static System.FormattableString;

    /// <summary>
    /// Extensions to <see cref="SqlStreamLocator"/>.
    /// </summary>
    public static class SqlLocatorExtensions
    {
        /// <summary>
        /// Opens the <see cref="SqlConnection"/> from the provided <see cref="ISqlLocator"/>.
        /// </summary>
        /// <param name="sqlLocator">The SQL locator.</param>
        /// <param name="connectionTimeout">Timeout for the connection.</param>
        /// <returns>SqlConnection.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope", Justification = "Should be disposed outside of this.")]
        public static SqlConnection OpenSqlConnection(
            this ISqlLocator sqlLocator,
            TimeSpan connectionTimeout)
        {
            sqlLocator.MustForArg(nameof(sqlLocator)).NotBeNull();
            var connectionString = sqlLocator.BuildConnectionString(connectionTimeout);
            var result = new SqlConnection(connectionString);

            result.Open();
            return result;
        }

        /// <summary>
        /// Builds the connection string.
        /// </summary>
        /// <param name="sqlLocator">The SQL locator.</param>
        /// <param name="connectionTimeout">Timeout for the connection.</param>
        /// <returns>SQL Connection string.</returns>
        public static string BuildConnectionString(
            this ISqlLocator sqlLocator,
            TimeSpan connectionTimeout)
        {
            sqlLocator.MustForArg(nameof(sqlLocator)).NotBeNull();

            var instanceName = string.IsNullOrWhiteSpace(sqlLocator.InstanceName) ? string.Empty : Invariant($"\\{sqlLocator.InstanceName}");
            var connectionString = Invariant($"Server={sqlLocator.ServerName}{instanceName};database={sqlLocator.DatabaseName}; user id={sqlLocator.UserName}; password={sqlLocator.Password}; connect timeout={connectionTimeout.TotalSeconds};");
            return connectionString;
        }
    }
}
