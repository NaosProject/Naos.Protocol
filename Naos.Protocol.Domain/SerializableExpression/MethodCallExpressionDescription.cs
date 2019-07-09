// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MethodCallExpressionDescription.cs" company="Naos Project">
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
    public class MethodCallExpressionDescription : ExpressionDescriptionBase
    {
        /// <summary>Initializes a new instance of the <see cref="MethodCallExpressionDescription"/> class.</summary>
        /// <param name="type">The type.</param>
        /// <param name="nodeType">Type of the node.</param>
        /// <param name="parentObject">The object.</param>
        /// <param name="method">The method.</param>
        /// <param name="arguments">The arguments.</param>
        public MethodCallExpressionDescription(
            TypeDescription type,
            ExpressionType nodeType,
            ExpressionDescriptionBase parentObject,
            MethodInfoDescription method,
            IReadOnlyCollection<ExpressionDescriptionBase> arguments)
        : base(type, nodeType)
        {
            this.ParentObject = parentObject;
            this.Method = method;
            this.Arguments = arguments;
        }

        /// <summary>Gets the object.</summary>
        /// <value>The object.</value>
        public ExpressionDescriptionBase ParentObject { get; private set; }

        /// <summary>Gets the method hash.</summary>
        /// <value>The method hash.</value>
        public MethodInfoDescription Method { get; private set; }

        /// <summary>Gets the arguments.</summary>
        /// <value>The arguments.</value>
        public IReadOnlyCollection<ExpressionDescriptionBase> Arguments { get; private set; }
    }

    /// <summary>
    /// Extensions to <see cref="MethodCallExpressionDescription" />.
    /// </summary>
    public static class MethodCallExpressionDescriptionExtensions
    {
        /// <summary>Converts to serializable.</summary>
        /// <param name="methodCallExpression">The methodCall expression.</param>
        /// <returns>Serializable expression.</returns>
        public static MethodCallExpressionDescription ToDescription(this MethodCallExpression methodCallExpression)
        {
            var type = methodCallExpression.Type.ToTypeDescription();
            var nodeType = methodCallExpression.NodeType;
            var parentObject = methodCallExpression.Object.ToDescription();
            var method = methodCallExpression.Method.ToDescription();
            var parameters = methodCallExpression.Arguments.ToDescription();

            var result = new MethodCallExpressionDescription(type, nodeType, parentObject, method, parameters);
            return result;
        }

        /// <summary>From the serializable.</summary>
        /// <param name="methodCallExpressionDescription">The methodCall expression.</param>
        /// <returns>Converted expression.</returns>
        public static MethodCallExpression FromDescription(this MethodCallExpressionDescription methodCallExpressionDescription)
        {
            var instance = methodCallExpressionDescription.ParentObject.FromDescription();
            var method = methodCallExpressionDescription.Method.FromDescription();
            var arguments = methodCallExpressionDescription.Arguments.FromDescription();
            var result = Expression.Call(
                instance,
                method,
                arguments);

            return result;
        }
    }
}
