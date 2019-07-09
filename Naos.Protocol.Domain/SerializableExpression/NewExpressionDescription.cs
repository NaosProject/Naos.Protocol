// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NewExpressionDescription.cs" company="Naos Project">
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
    public class NewExpressionDescription : ExpressionDescriptionBase
    {
        /// <summary>Initializes a new instance of the <see cref="NewExpressionDescription"/> class.</summary>
        /// <param name="type">The type.</param>
        /// <param name="constructorHash">The constructor hash.</param>
        /// <param name="arguments">The arguments.</param>
        public NewExpressionDescription(
            TypeDescription type,
            string constructorHash,
            IReadOnlyCollection<ExpressionDescriptionBase> arguments)
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
        public IReadOnlyCollection<ExpressionDescriptionBase> Arguments { get; private set; }
    }

    /// <summary>
    /// Extensions to <see cref="NewExpressionDescription" />.
    /// </summary>
    public static class NewExpressionDescriptionExtensions
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
        public static NewExpressionDescription ToDescription(this NewExpression newExpression)
        {
            var type = newExpression.Type.ToTypeDescription();
            var constructorHash = newExpression.Constructor.GetSignatureHash();
            var arguments = newExpression.Arguments.ToDescription();
            var result = new NewExpressionDescription(type, constructorHash, arguments);
            return result;
        }

        /// <summary>From the serializable.</summary>
        /// <param name="newExpressionDescription">The new expression.</param>
        /// <returns>Converted expression.</returns>
        public static NewExpression FromDescription(this NewExpressionDescription newExpressionDescription)
        {
            var type = newExpressionDescription.Type.ResolveFromLoadedTypes();

            NewExpression result;
            if (!string.IsNullOrWhiteSpace(newExpressionDescription.ConstructorHash))
            {
                var constructor = type.GetConstructors().Single(_ => _.GetSignatureHash().Equals(newExpressionDescription.ConstructorHash, StringComparison.OrdinalIgnoreCase));
                var arguments = newExpressionDescription.Arguments.FromDescription();
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
