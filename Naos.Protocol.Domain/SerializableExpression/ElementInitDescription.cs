// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ElementInitDescription.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Protocol.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using OBeautifulCode.Type;

    /// <summary>
    /// Serializable version of <see cref="ElementInit" />.
    /// </summary>
    public class ElementInitDescription
    {
        /// <summary>Initializes a new instance of the <see cref="ElementInitDescription"/> class.</summary>
        /// <param name="type">Type with method.</param>
        /// <param name="addMethodHash">The add method.</param>
        /// <param name="arguments">The arguments.</param>
        public ElementInitDescription(TypeDescription type, string addMethodHash, IReadOnlyCollection<ExpressionDescriptionBase> arguments)
        {
            this.Type = type;
            this.AddMethodHash = addMethodHash;
            this.Arguments = arguments;
        }

        /// <summary>Gets the type.</summary>
        /// <value>The type.</value>
        public TypeDescription Type { get; private set; }

        /// <summary>Gets the add method.</summary>
        /// <value>The add method.</value>
        public string AddMethodHash { get; private set; }

        /// <summary>Gets the arguments.</summary>
        /// <value>The arguments.</value>
        public IReadOnlyCollection<ExpressionDescriptionBase> Arguments { get; private set; }
    }

    /// <summary>
    /// Extensions to <see cref="ElementInitDescription" />.
    /// </summary>
    public static class ElementInitDescriptionExtensions
    {
        /// <summary>Converts to serializable.</summary>
        /// <param name="elementInit">The elementInitDescription.</param>
        /// <returns>Serializable version.</returns>
        public static ElementInitDescription ToDescription(this ElementInit elementInit)
        {
            var type = elementInit.AddMethod.DeclaringType.ToTypeDescription();
            var addMethodHash = elementInit.AddMethod.GetSignatureHash();
            var arguments = elementInit.Arguments.ToDescription();
            var result = new ElementInitDescription(type, addMethodHash, arguments);
            return result;
        }

        /// <summary>From the serializable.</summary>
        /// <param name="elementInitDescription">The elementInitDescription.</param>
        /// <returns>Converted version.</returns>
        public static ElementInit FromDescription(this ElementInitDescription elementInitDescription)
        {
            var type = elementInitDescription.Type.ResolveFromLoadedTypes();
            var addMethod = type.GetMethods().Single(_ => _.GetSignatureHash().Equals(elementInitDescription.AddMethodHash, StringComparison.OrdinalIgnoreCase));
            var arguments = elementInitDescription.Arguments.FromDescription();

            var result = Expression.ElementInit(addMethod, arguments);
            return result;
        }

        /// <summary>Converts to serializable.</summary>
        /// <param name="elementInits">The elementInitDescription.</param>
        /// <returns>Serializable version.</returns>
        public static IReadOnlyCollection<ElementInitDescription> ToDescription(this IReadOnlyCollection<ElementInit> elementInits)
        {
            var result = elementInits.Select(_ => _.ToDescription()).ToList();
            return result;
        }

        /// <summary>From the serializable.</summary>
        /// <param name="elementInits">The elementInitDescription.</param>
        /// <returns>Converted version.</returns>
        public static IReadOnlyCollection<ElementInit> FromDescription(this IReadOnlyCollection<ElementInitDescription> elementInits)
        {
            var result = elementInits.Select(_ => _.FromDescription()).ToList();
            return result;
        }
    }
}
