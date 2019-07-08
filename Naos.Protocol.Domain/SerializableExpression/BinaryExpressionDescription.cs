// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BinaryExpressionDescription.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Protocol.Domain
{
    using System.Linq.Expressions;
    using OBeautifulCode.Type;

    /// <summary>Serializable version of <see cref="BinaryExpression" />.</summary>
    public class BinaryExpressionDescription : ExpressionDescriptionBase
    {
        /// <summary>Initializes a new instance of the <see cref="BinaryExpressionDescription"/> class.</summary>
        /// <param name="type">The type of expression.</param>
        /// <param name="nodeType">Type of the node.</param>
        /// <param name="left">The left expression.</param>
        /// <param name="right">The right expression.</param>
        public BinaryExpressionDescription(TypeDescription type, ExpressionType nodeType, ExpressionDescriptionBase left, ExpressionDescriptionBase right)
            : base(type, nodeType)
        {
            this.Left = left;
            this.Right = right;
        }

        /// <summary>Gets the left expression.</summary>
        /// <value>The left expression.</value>
        public ExpressionDescriptionBase Left { get; private set; }

        /// <summary>Gets the right expression.</summary>
        /// <value>The right expression.</value>
        public ExpressionDescriptionBase Right { get; private set; }
    }

    /// <summary>
    /// Extensions to <see cref="BinaryExpressionDescription" />.
    /// </summary>
    public static class SerializableBinaryExpressionExtensions
    {
        /// <summary>Converts to serializable.</summary>
        /// <param name="binaryExpression">The binary expression.</param>
        /// <returns>The real expression</returns>
        public static BinaryExpressionDescription ToDescription(this BinaryExpression binaryExpression)
        {
            var type = binaryExpression.Type.ToTypeDescription();
            var nodeType = binaryExpression.NodeType;
            var left = binaryExpression.Left.ToDescription();
            var right = binaryExpression.Right.ToDescription();
            var result = new BinaryExpressionDescription(type, nodeType, left, right);
            return result;
        }

        /// <summary>
        /// Converts from serializable.
        /// </summary>
        /// <param name="binaryExpressionDescription">The binary expression.</param>
        /// <returns>The real expression.</returns>
        public static BinaryExpression FromDescription(this BinaryExpressionDescription binaryExpressionDescription)
        {
            return Expression.MakeBinary(binaryExpressionDescription.NodeType, binaryExpressionDescription.Left.FromDescription(), binaryExpressionDescription.Right.FromDescription());
        }
    }
}
