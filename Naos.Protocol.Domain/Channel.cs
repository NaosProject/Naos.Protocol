// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Channel.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace Naos.Protocol.Domain
{
    using System;

    /// <summary>
    /// Channel to route a <see cref="DispatchedOperationSequence" />.
    /// </summary>
    public class Channel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Channel"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        public Channel(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Cannot be null or whitespace.", nameof(name));
            }

            this.Name = name;
        }

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>The name.</value>
        public string Name { get; private set; }
    }
}