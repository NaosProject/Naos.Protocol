// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DetermineLocatorByKeyOp.cs" company="Naos Project">
//     Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Protocol.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using OBeautifulCode.Representation;
    using OBeautifulCode.Type;
    using static System.FormattableString;

    /// <summary>
    /// Operation to get a <see cref="LocatorBase" /> using a provided key.
    /// </summary>
    /// <typeparam name="TKey">The type of the t key.</typeparam>
    /// <typeparam name="TLocator">The type of the t locator.</typeparam>
    /// <seealso cref="Naos.Protocol.Domain.ReturningOperationBase{TLocator}" />
    public class DetermineLocatorByKeyOp<TKey, TLocator> : ReturningOperationBase<TLocator>
        where TLocator : LocatorBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DetermineLocatorByKeyOp{TKey, TLocator}"/> class.
        /// </summary>
        /// <param name="key">The key.</param>
        public DetermineLocatorByKeyOp(
            TKey key)
        {
            this.Key = key;
        }

        /// <summary>
        /// Gets the key.
        /// </summary>
        /// <value>The key.</value>
        public TKey Key { get; }
    }
}