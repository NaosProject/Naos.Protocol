// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SqlStreamLocatorExtensions.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Protocol.SqlServer
{
    using System.Data.SqlClient;
    using System.Threading;
    using Naos.Protocol.Domain;
    using Naos.Protocol.Domain.Internal;
    using OBeautifulCode.Assertion.Recipes;
    using OBeautifulCode.Type;

    using static System.FormattableString;

    /// <summary>
    /// Extensions to <see cref="SqlStreamLocator"/>.
    /// </summary>
    public static class SqlStreamLocatorExtensions
    {
        /// <summary>
        /// Opens the <see cref="SqlConnection"/> from the provided <see cref="SqlStreamLocator"/>.
        /// </summary>
        /// <param name="streamLocator">The stream locator.</param>
        /// <returns>SqlConnection.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope", Justification = "Should be disposed outside of this.")]
        public static SqlConnection OpenSqlConnection(
            this SqlStreamLocator streamLocator)
        {
            var instanceName = string.IsNullOrWhiteSpace(streamLocator.InstanceName) ? string.Empty : Invariant($"\\{streamLocator.InstanceName}");
            var connectionString = Invariant($"Server={streamLocator.ServerName}{instanceName};database={streamLocator.DatabaseName}; user id={streamLocator.UserName}; password={streamLocator.Password}");

            var result = new SqlConnection(connectionString);
            result.Open();
            return result;
        }
    }
}
