﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ProtocolComposerBase.cs" company="Naos Project">
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

    /// <summary>
    /// Abstract base for Protocol Factories to derive from and provide default connecting functionality.
    /// </summary>
    public abstract class ProtocolComposerBase
    {
        private static readonly object SyncOperationToProtocolMap = new object();

        private static readonly Dictionary<Type, RegisteredProtocol> OperationToProtocolMap = new Dictionary<Type, RegisteredProtocol>();

        protected ProtocolComposerBase()
        {
            // build the world here
        }

        /// <summary>
        /// Gets the dependent factory types.
        /// </summary>
        /// <value>The dependent factory types.</value>
        public virtual IReadOnlyCollection<Type> DependentComposerTypes => new Type[0];

        public IProtocol<TOperation> GetProtocol<TOperation>()
            where TOperation : OperationBase
        {
            lock (SyncOperationToProtocolMap)
            {
                var operationType = typeof(TOperation);
                if (!OperationToProtocolMap.ContainsKey(operationType))
                {
                    throw new NotImplementedException("Make a real exception for this. = no protocol found");
                }

                var result = OperationToProtocolMap[operationType];

                return (IProtocol<TOperation>)result.GetBuiltProtocol(this);
            }
        }

        public IProtocol<TOperation> ReCompose<TOperation>()
            where TOperation : OperationBase
        {
            throw new NotImplementedException();
        }

        public TComposer GetDependentComposer<TComposer>()
            where TComposer : ProtocolComposerBase
        {
            throw new NotImplementedException();
        }

        public IProtocol<TOperation> Compose<TOperation>()
            where TOperation : OperationBase
        {
            var actual = ((IComposeProtocol<TOperation>)this).Compose();
            return (IProtocol<TOperation>)actual;
        }

        public void ExecuteNoReturn<TOperation>(
            TOperation operation)
            where TOperation : OperationBase
        {
            var protocol = this.ReCompose<TOperation>();
            if (protocol is IProtocolWithoutReturn<TOperation> protocolWithoutReturn)
            {
                protocolWithoutReturn.ExecuteNoReturn(operation);
            }
            else
            {
                throw new ArgumentException(Invariant($"Cannot '{nameof(this.ExecuteNoReturn)}' unless the protocol '{protocol}' implements '{nameof(IProtocolWithoutReturn<TOperation>)}'."));
            }
        }

        public TReturn ExecuteScalar<TOperation, TReturn>(
            TOperation operation)
            where TOperation : OperationBase<TReturn>
        {
            var protocol = this.ReCompose<TOperation>();
            if (protocol is IProtocolWithReturn<TOperation, TReturn> protocolWithoutReturn)
            {
                var result = protocolWithoutReturn.ExecuteScalar<TReturn>(operation);
                return result;
            }
            else
            {
                throw new ArgumentException(Invariant($"Cannot '{nameof(this.ExecuteScalar)}' unless the protocol '{protocol}' implements '{nameof(IProtocolWithReturn<TOperation, TReturn>)}'."));
            }
        }
    }
}