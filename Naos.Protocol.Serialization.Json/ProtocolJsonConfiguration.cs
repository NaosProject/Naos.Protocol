// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ProtocolJsonConfiguration.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Protocol.Serialization.Json
{
    using System;
    using System.Collections.Generic;
    using Naos.Protocol.Domain;
    using Naos.Serialization.Json;

    /// <summary>
    /// Protocol implementation of <see cref="JsonConfigurationBase" />.
    /// </summary>
    public class ProtocolJsonConfiguration : JsonConfigurationBase
    {
        /// <inheritdoc />
        protected override IReadOnlyCollection<Type> TypesToAutoRegisterWithDiscovery =>
            new[]
            {
                typeof(ProtocolComposerBase),
                typeof(EventBase),
                typeof(OperationBase<>),
                typeof(StreamLocator),
            };
    }
}
