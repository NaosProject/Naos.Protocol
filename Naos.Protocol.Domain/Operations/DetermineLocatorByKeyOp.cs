// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DetermineLocatorByKeyOp.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
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

    public class DetermineLocatorByKeyOp<TKey, TLocator> : ReturningOperationBase<TLocator>
        where TLocator : LocatorBase
    {
        public DetermineLocatorByKeyOp(
            TKey key)
        {
            this.Key = key;
        }

        public TKey Key { get; }
    }
}