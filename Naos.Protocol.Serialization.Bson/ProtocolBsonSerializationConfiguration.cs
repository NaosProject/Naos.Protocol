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
    public class ProtocolBsonSerializationConfiguration : BsonSerializationConfigurationBase
    {
        /// <inheritdoc />
        protected override IReadOnlyCollection<TypeToRegisterForBson> TypesToRegisterForBson => new[]
                                                                                                {
                                                                                                    typeof(EventBase).ToTypeToRegisterForBson(),
                                                                                                    typeof(VoidOperationBase).ToTypeToRegisterForBson(),
                                                                                                    typeof(ReturningOperationBase<>).ToTypeToRegisterForBson(),
                                                                                                    typeof(ResourceLocatorBase).ToTypeToRegisterForBson(),
                                                                                                };
    }
}
