// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GetIdAddIfNecessarySerializerDescriptionOp.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Protocol.Domain
{
    using System;
    using Naos.Protocol.SqlServer;
    using OBeautifulCode.Serialization;

    /// <summary>
    /// Find the identity of a <see cref="SerializationDescription"/>.
    /// </summary>
    public class GetIdAddIfNecessarySerializerDescriptionOp : ReturningOperationBase<Guid>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetIdAddIfNecessarySerializerDescriptionOp"/> class.
        /// </summary>
        /// <param name="streamLocator">Stream locator to inspect.</param>
        /// <param name="serializationDescription">The serialization description.</param>
        /// <exception cref="System.ArgumentNullException">serializationDescription.</exception>
        public GetIdAddIfNecessarySerializerDescriptionOp(
            SqlStreamLocator streamLocator,
            SerializationDescription serializationDescription)
        {
            this.StreamLocator = streamLocator;
            this.SerializationDescription = serializationDescription ?? throw new ArgumentNullException(nameof(serializationDescription));
        }

        /// <summary>
        /// Gets or sets the serialization description.
        /// </summary>
        /// <value>The serialization description.</value>
        public SerializationDescription SerializationDescription { get; set; }

        /// <summary>
        /// Gets the stream locator.
        /// </summary>
        /// <value>The stream locator.</value>
        public SqlStreamLocator StreamLocator { get; private set; }
    }
}