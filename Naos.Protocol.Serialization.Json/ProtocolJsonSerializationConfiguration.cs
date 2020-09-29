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
                                                                                                    typeof(IEvent<>).ToTypeToRegisterForJson(),
                                                                                                    typeof(IOperation).ToTypeToRegisterForJson(),
                                                                                                    typeof(IResourceLocator).ToTypeToRegisterForJson(),
                                                                                                };
    }
}
