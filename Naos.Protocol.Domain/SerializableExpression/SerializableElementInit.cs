// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SerializableElementInit.cs" company="Naos Project">
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
    public class SerializableElementInit
    {
        /// <summary>Initializes a new instance of the <see cref="SerializableElementInit"/> class.</summary>
        /// <param name="type">Type with method.</param>
        /// <param name="addMethodHash">The add method.</param>
        /// <param name="arguments">The arguments.</param>
        public SerializableElementInit(TypeDescription type, string addMethodHash, IReadOnlyCollection<SerializableExpressionBase> arguments)
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
        public IReadOnlyCollection<SerializableExpressionBase> Arguments { get; private set; }
    }

    /// <summary>
    /// Extensions to <see cref="SerializableElementInit" />.
    /// </summary>
    public static class SerializableElementInitExtensions
    {
        /// <summary>Converts to serializable.</summary>
        /// <param name="elementInit">The elementInit.</param>
        /// <returns>Serializable version.</returns>
        public static SerializableElementInit ToSerializable(this ElementInit elementInit)
        {
            var type = elementInit.AddMethod.DeclaringType.ToTypeDescription();
            var addMethodHash = elementInit.AddMethod.GetSignatureHash();
            var arguments = elementInit.Arguments.ToSerializable();
            var result = new SerializableElementInit(type, addMethodHash, arguments);
            return result;
        }

        /// <summary>From the serializable.</summary>
        /// <param name="elementInit">The elementInit.</param>
        /// <returns>Converted version.</returns>
        public static ElementInit FromSerializable(this SerializableElementInit elementInit)
        {
            var type = elementInit.Type.ResolveFromLoadedTypes();
            var addMethod = type.GetMethods().Single(_ => _.GetSignatureHash().Equals(elementInit.AddMethodHash, StringComparison.OrdinalIgnoreCase));
            var arguments = elementInit.Arguments.FromSerializable();

            var result = Expression.ElementInit(addMethod, arguments);
            return result;
        }

        /// <summary>Converts to serializable.</summary>
        /// <param name="elementInits">The elementInit.</param>
        /// <returns>Serializable version.</returns>
        public static IReadOnlyCollection<SerializableElementInit> ToSerializable(this IReadOnlyCollection<ElementInit> elementInits)
        {
            var result = elementInits.Select(_ => _.ToSerializable()).ToList();
            return result;
        }

        /// <summary>From the serializable.</summary>
        /// <param name="elementInits">The elementInit.</param>
        /// <returns>Converted version.</returns>
        public static IReadOnlyCollection<ElementInit> FromSerializable(this IReadOnlyCollection<SerializableElementInit> elementInits)
        {
            var result = elementInits.Select(_ => _.FromSerializable()).ToList();
            return result;
        }
    }
}
