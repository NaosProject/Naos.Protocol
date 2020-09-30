// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SerializationConfigurationTypes.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Protocol.Domain.Test
{
    using System;
    using System.Threading.Tasks;
    using Naos.Protocol.Serialization.Bson;
    using Naos.Protocol.Serialization.Json;
    using OBeautifulCode.Assertion.Recipes;
    using OBeautifulCode.Serialization.Bson;
    using OBeautifulCode.Serialization.Json;
    using Xunit;

    /// <summary>
    /// Serialization properties to use for testing.
    /// </summary>
    public static partial class SerializationConfigurationTypes
    {
        /// <summary>
        /// Gets the type of the bson serialization configuration.
        /// </summary>
        /// <value>The type of the bson serialization configuration.</value>
        public static BsonSerializationConfigurationType BsonSerializationConfigurationType =>
            typeof(ProtocolBsonSerializationConfiguration).ToBsonSerializationConfigurationType();

        /// <summary>
        /// Gets the type of the json serialization configuration.
        /// </summary>
        /// <value>The type of the json serialization configuration.</value>
        public static JsonSerializationConfigurationType JsonSerializationConfigurationType
            => typeof(ProtocolJsonSerializationConfiguration).ToJsonSerializationConfigurationType();
    }
}
