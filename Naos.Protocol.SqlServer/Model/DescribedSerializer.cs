// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DescribedSerializer.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Protocol.SqlServer
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Threading.Tasks;
    using Dapper;
    using Naos.Protocol.Domain;
    using Naos.Protocol.SqlServer.Internal;
    using Naos.Recipes.RunWithRetry;
    using OBeautifulCode.Assertion.Recipes;
    using OBeautifulCode.Serialization;
    using OBeautifulCode.Serialization.Bson;
    using OBeautifulCode.Type;
    using OBeautifulCode.Type.Recipes;
    using SerializationFormat = OBeautifulCode.Serialization.SerializationFormat;

    /// <summary>
    /// Existing serializer with database ID.
    /// </summary>
    public partial class DescribedSerializer
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DescribedSerializer"/> class.
        /// </summary>
        /// <param name="serializerDescription">The serializer description.</param>
        /// <param name="serializationFormat">The serialization format.</param>
        /// <param name="serializer">The serializer.</param>
        /// <param name="serializerDescriptionId">The serializer description identifier.</param>
        public DescribedSerializer(SerializerRepresentation serializerDescription, SerializationFormat serializationFormat, ISerializeAndDeserialize serializer, int serializerDescriptionId)
        {
            serializerDescription.MustForArg(nameof(serializerDescription)).NotBeNull();
            serializer.MustForArg(nameof(serializer)).NotBeNull();
            this.SerializerRepresentation = serializerDescription;
            this.SerializationFormat = serializationFormat;
            this.Serializer = serializer;
            this.SerializerRepresentationId = serializerDescriptionId;
        }

        /// <summary>
        /// Gets the serializer description.
        /// </summary>
        /// <value>The serializer description.</value>
        public SerializerRepresentation SerializerRepresentation { get; private set; }

        /// <summary>
        /// Gets the serialization format.
        /// </summary>
        /// <value>The serialization format.</value>
        public SerializationFormat SerializationFormat { get; private set; }

        /// <summary>
        /// Gets the serializer.
        /// </summary>
        /// <value>The serializer.</value>
        public ISerializeAndDeserialize Serializer { get; private set; }

        /// <summary>
        /// Gets the serializer description identifier.
        /// </summary>
        /// <value>The serializer description identifier.</value>
        public int SerializerRepresentationId { get; private set; }
    }
}
