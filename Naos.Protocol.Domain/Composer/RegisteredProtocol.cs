// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ProtocolToRegister.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Protocol.Domain {
    using System;
    using OBeautifulCode.Representation;

    /// <summary>
    /// Protocol that has been registered.
    /// </summary>
    public class RegisteredProtocol
    {
        private readonly object instanceSync = new object();
        private          object instance;

        public RegisteredProtocol(
            ProtocolPrototype protocolRegistration,
            TypeRepresentation protocolType)
        {
            this.ProtocolRegistration = protocolRegistration ?? throw new ArgumentNullException(nameof(protocolRegistration));
            this.ProtocolType         = protocolType         ?? throw new ArgumentNullException(nameof(protocolType));
        }

        public object GetBuiltProtocol(ProtocolComposerBase supportingProtocolComposer)
        {
            lock (this.instanceSync)
            {
                if (this.instance == null)
                {
                    var realProtocolType = this.ProtocolType.ResolveFromLoadedTypes();
                    throw new NotImplementedException();
                    //var constructors = ;// make sure all constructor parameters can be resolved as operation handlers of operations in supportingProtocolComposer
                    //this.instance = protocolType.Construct();
                }
                else
                {
                    return this.instance;
                }
            }
        }

        public ProtocolPrototype ProtocolRegistration { get; private set; }
        public TypeRepresentation ProtocolType         { get; }
    }
}