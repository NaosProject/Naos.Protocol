// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GetProtocolByTypeOp.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Protocol.Domain
{
    using System;
    using OBeautifulCode.Representation.System;
    using static System.FormattableString;

    /// <summary>
    /// Get a protocol by the specified type.
    /// </summary>
    public class GetProtocolByTypeOp : ReturningOperationBase<IProtocol>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetProtocolByTypeOp"/> class.
        /// </summary>
        /// <param name="protocolType">Type of the protocol.</param>
        /// <param name="missingProtocolStrategy">Strategy on how to respond to a missing protocol.</param>
        /// <exception cref="ArgumentNullException">protocolType.</exception>
        public GetProtocolByTypeOp(TypeRepresentation protocolType, MissingProtocolStrategy missingProtocolStrategy = MissingProtocolStrategy.Throw)
        {
            if (protocolType == null)
            {
                throw new ArgumentNullException(nameof(protocolType));
            }

            this.ProtocolType = protocolType;
            this.MissingProtocolStrategy = missingProtocolStrategy;
        }

        /// <summary>
        /// Gets the type of the protocol.
        /// </summary>
        /// <value>The type of the protocol.</value>
        public TypeRepresentation ProtocolType { get; private set; }

        /// <summary>
        /// Gets the missing protocol strategy.
        /// </summary>
        /// <value>The missing protocol strategy.</value>
        public MissingProtocolStrategy MissingProtocolStrategy { get; private set; }
    }
}
