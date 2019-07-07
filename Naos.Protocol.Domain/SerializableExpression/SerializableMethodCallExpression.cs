// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SerializableMethodCallExpression.cs" company="Naos Project">
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
    /// Serializable version of <see cref="MethodCallExpression" />.
    /// </summary>
    public class SerializableMethodCallExpression : SerializableExpressionBase
    {
        /// <summary>Initializes a new instance of the <see cref="SerializableMethodCallExpression"/> class.</summary>
        /// <param name="type">The type.</param>
        /// <param name="nodeType">Type of the node.</param>
        /// <param name="parentObject">The object.</param>
        /// <param name="methodHash">The method hash.</param>
        /// <param name="arguments">The arguments.</param>
        public SerializableMethodCallExpression(
            TypeDescription type,
            ExpressionType nodeType,
            SerializableExpressionBase parentObject,
            string methodHash,
            IReadOnlyCollection<SerializableExpressionBase> arguments)
        : base(type, nodeType)
        {
            this.ParentObject = parentObject;
            this.MethodHash = methodHash;
            this.Arguments = arguments;
        }

        /// <summary>Gets the object.</summary>
        /// <value>The object.</value>
        public SerializableExpressionBase ParentObject { get; private set; }

        /// <summary>Gets the method hash.</summary>
        /// <value>The method hash.</value>
        public string MethodHash { get; private set; }

        /// <summary>Gets the arguments.</summary>
        /// <value>The arguments.</value>
        public IReadOnlyCollection<SerializableExpressionBase> Arguments { get; private set; }
    }

    /// <summary>
    /// Extensions to <see cref="SerializableMethodCallExpression" />.
    /// </summary>
    public static class SerializableMethodCallExpressionExtensions
    {
        /// <summary>Gets the signature hash.</summary>
        /// <param name="methodInfo">The method information.</param>
        /// <returns>Hash of method signature.</returns>
        public static string GetSignatureHash(this MethodInfo methodInfo)
        {
            var declaringType = methodInfo.DeclaringType?.FullName ?? "<Unknown-MaybeDynamic>";
            var methodName = methodInfo.Name;
            var generics = methodInfo.IsGenericMethod ? string.Join(",", methodInfo.GetGenericArguments().Select(_ => _.FullName)) : null;
            var genericsAddIn = generics == null ? string.Empty : Invariant($"<{generics}>");
            var parameters = string.Join(",", methodInfo.GetParameters().Select(_ => Invariant($"{_.ParameterType}-{_.Name}")));
            var result = Invariant($"{declaringType}->{methodName}{genericsAddIn}({parameters})");
            return result;
        }

        /// <summary>Converts to serializable.</summary>
        /// <param name="methodCallExpression">The methodCall expression.</param>
        /// <returns>Serializable expression.</returns>
        public static SerializableMethodCallExpression ToSerializable(this MethodCallExpression methodCallExpression)
        {
            var type = methodCallExpression.Type.ToTypeDescription();
            var nodeType = methodCallExpression.NodeType;
            var parentObject = methodCallExpression.Object.ToSerializable();
            var methodHash = methodCallExpression.Method.GetSignatureHash();
            var parameters = methodCallExpression.Arguments.ToSerializable();

            var result = new SerializableMethodCallExpression(type, nodeType, parentObject, methodHash, parameters);
            return result;
        }

        /// <summary>From the serializable.</summary>
        /// <param name="methodCallExpression">The methodCall expression.</param>
        /// <returns>Converted expression.</returns>
        public static MethodCallExpression FromSerializable(this SerializableMethodCallExpression methodCallExpression)
        {
            var instance = methodCallExpression.ParentObject.FromSerializable();
            var method = instance.Type.GetMethods().Single(_ => _.GetSignatureHash().Equals(methodCallExpression.MethodHash, StringComparison.OrdinalIgnoreCase));
            var arguments = methodCallExpression.Arguments.FromSerializable();
            var result = Expression.Call(
                instance,
                method,
                arguments);

            return result;
        }
    }
}
