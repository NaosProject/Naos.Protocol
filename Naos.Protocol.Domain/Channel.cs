// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Channel.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Protocol.Domain
{
    using OBeautifulCode.Validation.Recipes;

    /// <summary>
    /// Channel to route a <see cref="DispatchedOperationSequence" />.
    /// </summary>
    public class Channel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Channel"/> class.
        /// </summary>
        /// <param name="name">Name of the channel.</param>
        public Channel(string name)
        {
            new { name }.Must().NotBeNullNorWhiteSpace();

            this.Name = name;
        }

        /// <summary>
        /// Gets the channel name.
        /// </summary>
        public string Name { get; private set; }
    }
}