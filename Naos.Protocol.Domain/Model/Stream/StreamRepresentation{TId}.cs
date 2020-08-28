// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StreamRepresentation{TId}.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Protocol.Domain
{
    using System;
    using Naos.Protocol.Domain.Internal;
    using OBeautifulCode.Assertion.Recipes;

    /// <summary>
    /// Stream description to allow the <see cref="StreamFactory{TId}"/> to produce the correct stream.
    /// </summary>
    /// <typeparam name="TId">The type of ID of the stream.</typeparam>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1711:IdentifiersShouldNotHaveIncorrectSuffix", Justification = NaosSuppressBecause.CA1711_IdentifiersShouldNotHaveIncorrectSuffix_TypeNameAddedAsSuffixForTestsWhereTypeIsPrimaryConcern)]
    public class StreamRepresentation<TId> : IEquatable<StreamRepresentation<TId>>
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

        /// <inheritdoc />
        public bool Equals(
            StreamRepresentation<TId> other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            return this.Name == other.Name;
        }

        /// <inheritdoc />
        public override bool Equals(
            object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (obj.GetType() != this.GetType())
            {
                return false;
            }

            return this.Equals((StreamRepresentation<TId>)obj);
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            return this.Name != null ? this.Name.GetHashCode() : 0;
        }

        /// <summary>
        /// Implements the == operator.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator ==(
            StreamRepresentation<TId> left,
            StreamRepresentation<TId> right)
        {
            return Equals(left, right);
        }

        /// <summary>
        /// Implements the != operator.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator !=(
            StreamRepresentation<TId> left,
            StreamRepresentation<TId> right)
        {
            return !Equals(left, right);
        }
    }
}
