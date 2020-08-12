﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GetIdAddIfNecessarySerializerRepresentationOp.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Protocol.Domain
{
    using System;
    using Naos.Protocol.SqlServer;
    using OBeautifulCode.Serialization;

    /// <summary>
    /// Find the identity of a <see cref="SerializerRepresentation"/>.
    /// </summary>
    public class GetIdAddIfNecessarySerializerRepresentationOp : ReturningOperationBase<int>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetIdAddIfNecessarySerializerRepresentationOp"/> class.
        /// </summary>
        /// <param name="streamLocator">Stream locator to inspect.</param>
        /// <param name="serializerRepresentation">The serialization description.</param>
        /// <param name="serializationFormat">The serialization format.</param>
        /// <exception cref="System.ArgumentNullException">serializerRepresentation.</exception>
        public GetIdAddIfNecessarySerializerRepresentationOp(
            SqlStreamLocator streamLocator,
            SerializerRepresentation serializerRepresentation,
            SerializationFormat serializationFormat)
        {
            this.StreamLocator = streamLocator ?? throw new ArgumentNullException(nameof(streamLocator));
            this.SerializerRepresentation = serializerRepresentation ?? throw new ArgumentNullException(nameof(serializerRepresentation));

            if (serializationFormat == SerializationFormat.Invalid)
            {
                throw new ArgumentException("Format cannot be 'Invalid'.", nameof(serializationFormat));
            }

            this.SerializationFormat = serializationFormat;
        }

        /// <summary>
        /// Gets the serialization description.
        /// </summary>
        /// <value>The serialization description.</value>
        public SerializerRepresentation SerializerRepresentation { get; private set; }

        /// <summary>
        /// Gets the serialization format.
        /// </summary>
        /// <value>The serialization format.</value>
        public SerializationFormat SerializationFormat { get; private set; }

        /// <summary>
        /// Gets the stream locator.
        /// </summary>
        /// <value>The stream locator.</value>
        public SqlStreamLocator StreamLocator { get; private set; }
    }
}