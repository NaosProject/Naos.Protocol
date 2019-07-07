// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SerializableNewExpression.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Protocol.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using OBeautifulCode.Type;
    using static System.FormattableString;

    /// <summary>
    /// Serializable version of <see cref="NewExpression" />.
    /// </summary>
    public class SerializableNewExpression : SerializableExpressionBase
    {
        /// <summary>Initializes a new instance of the <see cref="SerializableNewExpression"/> class.</summary>
        /// <param name="type">The type.</param>
        /// <param name="constructorHash">The constructor hash.</param>
        /// <param name="arguments">The arguments.</param>
        public SerializableNewExpression(
            TypeDescription type,
            string constructorHash,
            IReadOnlyCollection<SerializableExpressionBase> arguments)
            : base(type, ExpressionType.New)
        {
            this.ConstructorHash = constructorHash;
            this.Arguments = arguments;
        }

        /// <summary>Gets the constructor hash.</summary>
        /// <value>The constructor hash.</value>
        public string ConstructorHash { get; private set; }

        /// <summary>Gets the arguments.</summary>
        /// <value>The arguments.</value>
        public IReadOnlyCollection<SerializableExpressionBase> Arguments { get; private set; }
    }

    /// <summary>
    /// Extensions to <see cref="SerializableNewExpression" />.
    /// </summary>
    public static class SerializableNewExpressionExtensions
    {
        /// <summary>Gets the signature hash.</summary>
        /// <param name="constructorInfo">The constructor information.</param>
        /// <returns>Hash of signature.</returns>
        public static string GetSignatureHash(this ConstructorInfo constructorInfo)
        {
            var declaringType = constructorInfo.DeclaringType?.FullName ?? "<Unknown-MaybeDynamic>";
            var methodName = constructorInfo.Name;
            var generics = constructorInfo.IsGenericMethod ? string.Join(",", constructorInfo.GetGenericArguments().Select(_ => _.FullName)) : null;
            var genericsAddIn = generics == null ? string.Empty : Invariant($"<{generics}>");
            var parameters = string.Join(",", constructorInfo.GetParameters().Select(_ => Invariant($"{_.ParameterType}-{_.Name}")));
            var result = Invariant($"{declaringType}->{methodName}{genericsAddIn}({parameters})");
            return result;
        }

        /// <summary>Converts to serializable.</summary>
        /// <param name="newExpression">The new expression.</param>
        /// <returns>Serializable expression.</returns>
        public static SerializableNewExpression ToSerializable(this NewExpression newExpression)
        {
            var type = newExpression.Type.ToTypeDescription();
            var constructorHash = newExpression.Constructor.GetSignatureHash();
            var arguments = newExpression.Arguments.ToSerializable();
            var result = new SerializableNewExpression(type, constructorHash, arguments);
            return result;
        }

        /// <summary>From the serializable.</summary>
        /// <param name="newExpression">The new expression.</param>
        /// <returns>Converted expression.</returns>
        public static NewExpression FromSerializable(this SerializableNewExpression newExpression)
        {
            var type = newExpression.Type.ResolveFromLoadedTypes();

            NewExpression result;
            if (!string.IsNullOrWhiteSpace(newExpression.ConstructorHash))
            {
                var constructor = type.GetConstructors().Single(_ => _.GetSignatureHash().Equals(newExpression.ConstructorHash, StringComparison.OrdinalIgnoreCase));
                var arguments = newExpression.Arguments.FromSerializable();
                result = Expression.New(constructor, arguments);
            }
            else
            {
                result = Expression.New(type);
            }

            return result;
        }
    }
}
