// --------------------------------------------------------------------------------------------------------------------
// <copyright file="OperationBase.cs" company="Naos Project">
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

    public abstract class OperationBase
    {
    }

    public abstract class OperationNoReturnBase
    {
        public void ExecuteNoReturn<TOperation>(
            IProtocolNoReturn<TOperation> protocol)
            where TOperation : OperationNoReturnBase
        {
            protocol.ExecuteNoReturn((TOperation)this);
        }
    }

    /// <summary>
    /// Abstract base of an operation.
    /// </summary>
    public abstract class OperationWithReturnBase<TReturn> : OperationNoReturnBase
    {
        public TReturn ExecuteScalar<TOperation>(
            IProtocolWithReturn<TOperation, TReturn> protocol)
            where TOperation : OperationWithReturnBase<TReturn>
        {
            return protocol.ExecuteScalar((TOperation)this);
        }
    }

    /// <summary>
    /// Fake class to be the necessary return type for an operation.
    /// </summary>
    public class NoReturnType
    {
    }
}