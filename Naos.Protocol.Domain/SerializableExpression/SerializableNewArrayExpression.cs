// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SerializableNewArrayExpression.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Protocol.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using OBeautifulCode.Type;
    using static System.FormattableString;

    /// <summary>
    /// Serializable version of <see cref="NewArrayExpression" />.
    /// </summary>
    public class SerializableNewArrayExpression : SerializableExpressionBase
    {
        /// <summary>Initializes a new instance of the <see cref="SerializableNewArrayExpression"/> class.</summary>
        /// <param name="type">The type.</param>
        /// <param name="nodeType">Type of the node.</param>
        /// <param name="expressions">The expressions.</param>
        public SerializableNewArrayExpression(
            TypeDescription type,
            ExpressionType nodeType,
            IReadOnlyCollection<SerializableExpressionBase> expressions)
            : base(type, nodeType)
        {
            this.Expressions = expressions;
        }

        /// <summary>Gets the expressions.</summary>
        /// <value>The expressions.</value>
        public IReadOnlyCollection<SerializableExpressionBase> Expressions { get; private set; }
    }

    /// <summary>
    /// Extensions to <see cref="SerializableNewArrayExpression" />.
    /// </summary>
    public static class SerializableNewArrayExpressionExtensions
    {
        /// <summary>Converts to serializable.</summary>
        /// <param name="newArrayExpression">The newArray expression.</param>
        /// <returns>Serializable expression.</returns>
        public static SerializableNewArrayExpression ToSerializable(this NewArrayExpression newArrayExpression)
        {
            var type = newArrayExpression.Type.ToTypeDescription();
            var nodeType = newArrayExpression.NodeType;
            var expressions = newArrayExpression.Expressions.ToSerializable();
            var result = new SerializableNewArrayExpression(type, nodeType, expressions);
            return result;
        }

        /// <summary>From the serializable.</summary>
        /// <param name="newArrayExpression">The newArray expression.</param>
        /// <returns>Converted expression.</returns>
        public static NewArrayExpression FromSerializable(this SerializableNewArrayExpression newArrayExpression)
        {
            NewArrayExpression result;
            var nodeType = newArrayExpression.NodeType;
            switch (nodeType)
            {
                case ExpressionType.NewArrayBounds:
                    result = Expression.NewArrayBounds(newArrayExpression.Type.ResolveFromLoadedTypes(), newArrayExpression.Expressions.FromSerializable());
                    break;
                case ExpressionType.NewArrayInit:
                    result = Expression.NewArrayInit(newArrayExpression.Type.ResolveFromLoadedTypes(), newArrayExpression.Expressions.FromSerializable());
                    break;
                default:
                    throw new NotSupportedException(Invariant($"{nameof(newArrayExpression.NodeType)} '{nodeType}' is not supported."));
            }

            return result;
        }
    }
}
