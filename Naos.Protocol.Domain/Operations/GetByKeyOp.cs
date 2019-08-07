// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GetByKeyOp.cs" company="Naos Project">
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

    public class GetByKeyOp<TKey, TObject> : ReturningOperationBase<TObject>
        where TObject : class
    {
        public GetByKeyOp(TKey key)
        {
            this.Key = key;
        }

        public TKey Key { get; }
    }
}