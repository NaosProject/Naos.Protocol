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
    using OBeautifulCode.Type;

    /// <inheritdoc />
    public class ProtocolJsonSerializationConfiguration : JsonSerializationConfigurationBase
    {
        /// <inheritdoc />
        protected override IReadOnlyCollection<string> TypeToRegisterNamespacePrefixFilters => new[]
                                                                                               {
                                                                                                   Naos.Protocol.Domain.ProjectInfo.Namespace,
                                                                                               };

        /// <inheritdoc />
        protected override IReadOnlyCollection<TypeToRegisterForJson> TypesToRegisterForJson => new[]
                                                                                                {
                                                                                                    typeof(IModel).ToTypeToRegisterForJson(),
                                                                                                    typeof(MissingProtocolStrategy).ToTypeToRegisterForJson(),
                                                                                                    typeof(TypeVersionMatchStrategy).ToTypeToRegisterForJson(),
                                                                                                };
    }
}
