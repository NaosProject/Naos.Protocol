// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SerializableUnaryExpression.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Protocol.Domain
{
    using System.Linq.Expressions;
    using OBeautifulCode.Type;

    /// <summary>
    /// Serializable version of <see cref="UnaryExpression" />.
    /// </summary>
    public class SerializableUnaryExpression : SerializableExpressionBase
    {
        /// <summary>Initializes a new instance of the <see cref="SerializableUnaryExpression"/> class.</summary>
        /// <param name="type">The type.</param>
        /// <param name="nodeType">Type of the node.</param>
        /// <param name="operand">The operand.</param>
        public SerializableUnaryExpression(TypeDescription type, ExpressionType nodeType, SerializableExpressionBase operand)
            : base(type, nodeType)
        {
            this.Operand = operand;
        }

        /// <summary>Gets the operand.</summary>
        /// <value>The operand.</value>
        public SerializableExpressionBase Operand { get; private set; }
    }

    /// <summary>
    /// Extensions to <see cref="SerializableUnaryExpression" />.
    /// </summary>
    public static class SerializableUnaryExpressionExtensions
    {
        /// <summary>Converts to serializable.</summary>
        /// <param name="unaryExpression">The unary expression.</param>
        /// <returns>Serializable expression.</returns>
        public static SerializableUnaryExpression ToSerializable(this UnaryExpression unaryExpression)
        {
            var type = unaryExpression.Type.ToTypeDescription();
            var nodeType = unaryExpression.NodeType;
            var operand = unaryExpression.Operand.ToSerializable();

            var result = new SerializableUnaryExpression(type, nodeType, operand);
            return result;
        }

        /// <summary>From the serializable.</summary>
        /// <param name="unaryExpression">The unary expression.</param>
        /// <returns>Converted expression.</returns>
        public static Expression FromSerializable(this SerializableUnaryExpression unaryExpression)
        {
            var nodeType = unaryExpression.NodeType;
            switch (nodeType)
            {
                case ExpressionType.UnaryPlus:
                    return Expression.UnaryPlus(unaryExpression.Operand.FromSerializable());
                default:
                    return Expression.MakeUnary(nodeType, unaryExpression.Operand.FromSerializable(), unaryExpression.Type.ResolveFromLoadedTypes());
            }
        }
    }
}