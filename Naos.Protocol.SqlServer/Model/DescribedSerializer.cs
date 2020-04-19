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

    /// <summary>
    /// SQL implementation of an <see cref="IStream{TKey}" />.
    /// </summary>
    public partial class DescribedSerializer
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DescribedSerializer"/> class.
        /// </summary>
        /// <param name="serializerDescription">The serializer description.</param>
        /// <param name="serializer">The serializer.</param>
        /// <param name="serializerDescriptionId">The serializer description identifier.</param>
        public DescribedSerializer(SerializationDescription serializerDescription, ISerializeAndDeserialize serializer, Guid serializerDescriptionId)
        {
            serializerDescription.MustForArg(nameof(serializerDescription)).NotBeNull();
            serializer.MustForArg(nameof(serializer)).NotBeNull();
            this.SerializerDescription = serializerDescription;
            this.Serializer = serializer;
            this.SerializerDescriptionId = serializerDescriptionId;
        }

        /// <summary>
        /// Gets the serializer description.
        /// </summary>
        /// <value>The serializer description.</value>
        public SerializationDescription SerializerDescription { get; private set; }

        /// <summary>
        /// Gets the serializer.
        /// </summary>
        /// <value>The serializer.</value>
        public ISerializeAndDeserialize Serializer { get; private set; }

        /// <summary>
        /// Gets the serializer description identifier.
        /// </summary>
        /// <value>The serializer description identifier.</value>
        public Guid SerializerDescriptionId { get; private set; }
    }
}
