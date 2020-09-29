// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CacheStatusResult.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Protocol.Domain
{
    using System;
    using OBeautifulCode.Type;
    using static System.FormattableString;

    /// <summary>
    /// Status result of the cache.
    /// </summary>
    public partial class CacheStatusResult : IModelViaCodeGen
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CacheStatusResult"/> class.
        /// </summary>
        /// <param name="size">The size.</param>
        /// <param name="dateRangeOfCachedObjectsUtc">The date range of the cached objects (using their retrieval <see cref="DateTime"/> in UTC.</param>
        public CacheStatusResult(
            long size,
            UtcDateTimeRangeInclusive dateRangeOfCachedObjectsUtc)
        {
            this.Size = size;
            this.DateRangeOfCachedObjectsUtc = dateRangeOfCachedObjectsUtc;
        }

        /// <summary>
        /// Gets the size.
        /// </summary>
        /// <value>The size.</value>
        public long Size { get; private set; }

        /// <summary>
        /// Gets the date range of cached objects in UTC.
        /// </summary>
        /// <value>The date range of cached objects in UTC.</value>
        public UtcDateTimeRangeInclusive DateRangeOfCachedObjectsUtc { get; private set; }
    }
}
