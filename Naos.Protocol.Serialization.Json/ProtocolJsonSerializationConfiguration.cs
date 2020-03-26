// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ProtocolJsonSerializationConfiguration.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Protocol.Serialization.Json
{
    using System;
    using System.Collections.Generic;
    using Naos.Protocol.Domain;
    using OBeautifulCode.Serialization.Json;

    /// <inheritdoc />
    public class ProtocolJsonSerializationConfiguration : JsonConfigurationBase
    {
        /// <inheritdoc />
        protected override IReadOnlyCollection<Type> TypesToAutoRegister => new Type[]
        {
            typeof(EventBase),
            typeof(VoidOperationBase),
            typeof(ReturningOperationBase<>),
            typeof(StreamLocatorBase),
        };
    }
}
