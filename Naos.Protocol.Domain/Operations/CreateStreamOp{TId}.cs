// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CreateStreamOp{TId}.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Protocol.Domain
{
    using System;

    /// <summary>
    /// Create a stream's persistence.
    /// </summary>
    /// <typeparam name="TId">Type of ID being used.</typeparam>
    public class CreateStreamOp<TId> : VoidOperationBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateStreamOp{TId}"/> class.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <exception cref="ArgumentNullException">stream.</exception>
        public CreateStreamOp(IStream<TId> stream)
        {
            this.Stream = stream ?? throw new ArgumentNullException(nameof(stream));
        }

        /// <summary>
        /// Gets the stream.
        /// </summary>
        /// <value>The stream.</value>
        public IStream<TId> Stream { get; private set; }
    }
}
