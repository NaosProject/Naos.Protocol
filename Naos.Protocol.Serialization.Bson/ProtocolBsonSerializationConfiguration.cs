// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ProtocolBsonSerializationConfiguration.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Protocol.Serialization.Bson
{
    using System;
    using System.Collections.Generic;
    using Naos.Protocol.Domain;
    using OBeautifulCode.Serialization.Bson;

    /// <inheritdoc />
    public class ProtocolBsonSerializationConfiguration : BsonConfigurationBase
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
