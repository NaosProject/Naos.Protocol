﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StreamRepresentation{TId}.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Protocol.Domain
{
    using Naos.Protocol.Domain.Internal;
    using OBeautifulCode.Assertion.Recipes;

    /// <summary>
    /// Stream description to allow the <see cref="StreamFactory{TId}"/> to produce the correct stream.
    /// </summary>
    /// <typeparam name="TId">The type of ID of the stream.</typeparam>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1711:IdentifiersShouldNotHaveIncorrectSuffix", Justification = NaosSuppressBecause.CA1711_IdentifiersShouldNotHaveIncorrectSuffix_TypeNameAddedAsSuffixForTestsWhereTypeIsPrimaryConcern)]
    public class StreamRepresentation<TId>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StreamRepresentation{TId}"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        public StreamRepresentation(string name)
        {
            name.MustForArg(nameof(name)).NotBeNullNorWhiteSpace();
            this.Name = name;
        }

        /// <summary>
        /// Gets the name of the stream.
        /// </summary>
        /// <value>The name of the stream.</value>
        public string Name { get; private set; }
    }
}
