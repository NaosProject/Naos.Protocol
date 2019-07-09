// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UnaryExpressionDescription.cs" company="Naos Project">
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
    public class UnaryExpressionDescription : ExpressionDescriptionBase
    {
        /// <summary>Initializes a new instance of the <see cref="UnaryExpressionDescription"/> class.</summary>
        /// <param name="type">The type.</param>
        /// <param name="nodeType">Type of the node.</param>
        /// <param name="operand">The operand.</param>
        public UnaryExpressionDescription(TypeDescription type, ExpressionType nodeType, ExpressionDescriptionBase operand)
            : base(type, nodeType)
        {
            this.Operand = operand;
        }

        /// <summary>Gets the operand.</summary>
        /// <value>The operand.</value>
        public ExpressionDescriptionBase Operand { get; private set; }
    }

    /// <summary>
    /// Extensions to <see cref="UnaryExpressionDescription" />.
    /// </summary>
    public static class UnaryExpressionDescriptionExtensions
    {
        /// <summary>Converts to serializable.</summary>
        /// <param name="unaryExpression">The unary expression.</param>
        /// <returns>Serializable expression.</returns>
        public static UnaryExpressionDescription ToDescription(this UnaryExpression unaryExpression)
        {
            var type = unaryExpression.Type.ToTypeDescription();
            var nodeType = unaryExpression.NodeType;
            var operand = unaryExpression.Operand.ToDescription();

            var result = new UnaryExpressionDescription(type, nodeType, operand);
            return result;
        }

        /// <summary>From the serializable.</summary>
        /// <param name="unaryExpressionDescription">The unary expression.</param>
        /// <returns>Converted expression.</returns>
        public static Expression FromDescription(this UnaryExpressionDescription unaryExpressionDescription)
        {
            var nodeType = unaryExpressionDescription.NodeType;
            switch (nodeType)
            {
                case ExpressionType.UnaryPlus:
                    return Expression.UnaryPlus(unaryExpressionDescription.Operand.FromDescription());
                default:
                    return Expression.MakeUnary(nodeType, unaryExpressionDescription.Operand.FromDescription(), unaryExpressionDescription.Type.ResolveFromLoadedTypes());
            }
        }
    }
}