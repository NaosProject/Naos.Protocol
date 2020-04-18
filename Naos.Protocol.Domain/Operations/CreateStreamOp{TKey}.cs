﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CreateStreamOp{TKey}.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Protocol.Domain
{
    using System;
    using static System.FormattableString;

    /// <summary>
    /// Abstract base of an operation.
    /// </summary>
    /// <typeparam name="TKey">Type of key being used.</typeparam>
    public class CreateStreamOp<TKey> : VoidOperationBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateStreamOp{TKey}"/> class.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <exception cref="ArgumentNullException">stream.</exception>
        public CreateStreamOp(IStream<TKey> stream)
        {
            this.Stream = stream ?? throw new ArgumentNullException(nameof(stream));
        }

        /// <summary>
        /// Gets the stream.
        /// </summary>
        /// <value>The stream.</value>
        public IStream<TKey> Stream { get; private set; }
    }
}
