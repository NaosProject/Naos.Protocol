// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CacheResult{TOperation,TReturn}.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Protocol.Domain
{
    using System;
    using OBeautifulCode.Type;
    using static System.FormattableString;

    /// <summary>
    /// Model for the result of a cache query with metadata.
    /// </summary>
    /// <typeparam name="TOperation">Type of the operation.</typeparam>
    /// <typeparam name="TReturn">Type of return type of the operation being cached.</typeparam>
    public partial class CacheResult<TOperation, TReturn> : IModelViaCodeGen
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CacheResult{TOperation, TReturn}"/> class.
        /// </summary>
        /// <param name="operation">The operation.</param>
        /// <param name="cachedObject">The cached object.</param>
        /// <param name="freshnessInUtc">The freshness in UTC.</param>
        public CacheResult(
            TOperation operation,
            TReturn cachedObject,
            DateTime freshnessInUtc)
        {
            if (operation == null)
            {
                throw new ArgumentNullException(nameof(operation));
            }

            this.Operation = operation;
            this.CachedObject = cachedObject;
            this.FreshnessInUtc = freshnessInUtc;
        }

        /// <summary>
        /// Gets the operation.
        /// </summary>
        /// <value>The operation.</value>
        public TOperation Operation { get; private set; }

        /// <summary>
        /// Gets the cached object.
        /// </summary>
        /// <value>The cached object.</value>
        public TReturn CachedObject { get; private set; }

        /// <summary>
        /// Gets the freshness in UTC.
        /// </summary>
        /// <value>The freshness in UTC.</value>
        public DateTime FreshnessInUtc { get; private set; }
    }
}
