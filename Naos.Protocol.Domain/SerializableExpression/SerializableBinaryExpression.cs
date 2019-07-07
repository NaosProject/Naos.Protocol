// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SerializableBinaryExpression.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Protocol.Domain
{
    using System.Linq.Expressions;
    using OBeautifulCode.Type;

    /// <summary>Serializable version of <see cref="BinaryExpression" />.</summary>
    public class SerializableBinaryExpression : SerializableExpressionBase
    {
        /// <summary>Initializes a new instance of the <see cref="SerializableBinaryExpression"/> class.</summary>
        /// <param name="type">The type of expression.</param>
        /// <param name="nodeType">Type of the node.</param>
        /// <param name="left">The left expression.</param>
        /// <param name="right">The right expression.</param>
        public SerializableBinaryExpression(TypeDescription type, ExpressionType nodeType, SerializableExpressionBase left, SerializableExpressionBase right)
            : base(type, nodeType)
        {
            this.Left = left;
            this.Right = right;
        }

        /// <summary>Gets the left expression.</summary>
        /// <value>The left expression.</value>
        public SerializableExpressionBase Left { get; private set; }

        /// <summary>Gets the right expression.</summary>
        /// <value>The right expression.</value>
        public SerializableExpressionBase Right { get; private set; }
    }

    /// <summary>
    /// Extensions to <see cref="SerializableBinaryExpression" />.
    /// </summary>
    public static class SerializableBinaryExpressionExtensions
    {
        /// <summary>Converts to serializable.</summary>
        /// <param name="binaryExpression">The binary expression.</param>
        /// <returns>The real expression</returns>
        public static SerializableBinaryExpression ToSerializable(this BinaryExpression binaryExpression)
        {
            var type = binaryExpression.Type.ToTypeDescription();
            var nodeType = binaryExpression.NodeType;
            var left = binaryExpression.Left.ToSerializable();
            var right = binaryExpression.Right.ToSerializable();
            var result = new SerializableBinaryExpression(type, nodeType, left, right);
            return result;
        }

        /// <summary>
        /// Converts from serializable.
        /// </summary>
        /// <param name="binaryExpression">The binary expression.</param>
        /// <returns>The real expression.</returns>
        public static BinaryExpression FromSerializable(this SerializableBinaryExpression binaryExpression)
        {
            return Expression.MakeBinary(binaryExpression.NodeType, binaryExpression.Left.FromSerializable(), binaryExpression.Right.FromSerializable());
        }
    }
}
