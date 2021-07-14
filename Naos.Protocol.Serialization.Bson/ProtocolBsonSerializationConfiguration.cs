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
    using OBeautifulCode.Type;

    /// <inheritdoc />
    public class ProtocolBsonSerializationConfiguration : BsonSerializationConfigurationBase
    {
        /// <inheritdoc />
        protected override IReadOnlyCollection<string> TypeToRegisterNamespacePrefixFilters => new[]
                                                                                               {
                                                                                                   Naos.Protocol.Domain.ProjectInfo.Namespace,
                                                                                               };

        /// <inheritdoc />
        protected override IReadOnlyCollection<TypeToRegisterForBson> TypesToRegisterForBson => new[]
                                                                                                {
                                                                                                    typeof(IModel).ToTypeToRegisterForBson(),
                                                                                                };
    }
}
