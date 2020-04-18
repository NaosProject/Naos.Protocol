// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StreamSchema.Sprocs.Put.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Protocol.SqlServer
{
    using System;
    using Naos.Protocol.SqlServer.Internal;

    /// <summary>
    /// Object table schema.
    /// </summary>
    public static partial class StreamSchema
    {
        /// <summary>
        /// Builds the name of the put stored procedure.
        /// </summary>
        /// <param name="streamName">Name of the stream.</param>
        /// <returns>Name of the put stored procedure.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Sproc", Justification = NaosSuppressBecause.CA1704_IdentifiersShouldBeSpelledCorrectly_SpellingIsCorrectInContextOfTheDomain)]
        public static string BuildPutSprocName(
            string streamName)
        {
            return streamName;
        }
    }
}
