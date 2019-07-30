// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ProtocolToRegister.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Protocol.Domain {
    using System;
    using OBeautifulCode.Reflection.Recipes;
    using OBeautifulCode.Representation;

    public abstract class ProtocolPrototype
    {
    }

    public class ProtocolPrototype<TOperation>: ProtocolPrototype where TOperation: OperationBase
    {
        public ProtocolPrototype(Func<ProtocolComposerBase, IProtocol<TOperation>> protocolBuilder)
        {
            throw new System.NotImplementedException();
        }
    }
}