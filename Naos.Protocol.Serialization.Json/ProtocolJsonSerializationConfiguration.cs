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
    public class ProtocolJsonSerializationConfiguration : JsonSerializationConfigurationBase
    {
        /// <inheritdoc />
        protected override IReadOnlyCollection<TypeToRegisterForJson> TypesToRegisterForJson => new[]
                                                                                                {
                                                                                                    typeof(EventBase).ToTypeToRegisterForJson(),
                                                                                                    typeof(VoidOperationBase).ToTypeToRegisterForJson(),
                                                                                                    typeof(ReturningOperationBase<>).ToTypeToRegisterForJson(),
                                                                                                    typeof(StreamLocatorBase).ToTypeToRegisterForJson(),
                                                                                                };
    }
}
