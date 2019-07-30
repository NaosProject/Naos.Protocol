// --------------------------------------------------------------------------------------------------------------------
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

        public void Execute<TOperation>(
            TOperation operation)
            where TOperation : OperationBase
        {
            var protocol = this.GetProtocol<TOperation>();
            protocol.Execute(operation);
        }

        public TReturn Execute<TOperation, TReturn>(
            TOperation operation)
            where TOperation : OperationBase<TReturn>
        {
            var protocol = this.GetProtocol<TOperation, TReturn>();
            return protocol.Execute<TReturn>(operation);
        }

        public IProtocol<TOperation> GetProtocol<TOperation>()
            where TOperation : OperationBase
        {
            throw new NotImplementedException();
        }

        public IProtocol<TOperation, TReturn> GetProtocol<TOperation, TReturn>()
            where TOperation : OperationBase<TReturn>
        {
            lock (SyncOperationToProtocolMap)
            {
                var operationType = typeof(TOperation);
                if (!OperationToProtocolMap.ContainsKey(operationType))
                {
                    throw new NotImplementedException("Make a real exception for this. = no protocol found");
                }

                var result = OperationToProtocolMap[operationType];

                return (IProtocol<TOperation, TReturn>)result.GetBuiltProtocol(this);
            }
        }

        //public IComposeProtocol<TOperation> ComposeFor(TOperation)

        public TComposer GetDependentComposer<TComposer>()
            where TComposer : ProtocolComposerBase
        {
            throw new NotImplementedException();
        }

        protected IProtocol<TObject> ComposeFromRegistrations<TObject>()
            where TObject : OperationBase
        {
            throw new NotImplementedException();
        }

        protected IProtocol<TOperation, TReturn> ReCompose<TOperation, TReturn>()
            where TOperation : OperationBase<TReturn>
        {
            var actual = ((IComposeProtocol<TOperation>)this).Compose();
            return (IProtocol<TOperation, TReturn>)actual;
        }
    }
}