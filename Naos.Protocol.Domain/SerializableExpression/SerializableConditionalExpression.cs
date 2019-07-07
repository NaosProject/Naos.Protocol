// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SerializableConditionalExpression.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Protocol.Domain
{
    using System.Linq.Expressions;
    using OBeautifulCode.Type;

    /// <summary>
    /// Serializable version of <see cref="ConditionalExpression" />.
    /// </summary>
    public class SerializableConditionalExpression : SerializableExpressionBase
    {
        /// <summary>Initializes a new instance of the <see cref="SerializableConditionalExpression"/> class.</summary>
        /// <param name="type">The type of expression.</param>
        /// <param name="nodeType">Type of the node.</param>
        /// <param name="test">The test expression.</param>
        /// <param name="ifTrue">If true expression.</param>
        /// <param name="ifFalse">If false expression.</param>
        public SerializableConditionalExpression(TypeDescription type, ExpressionType nodeType, SerializableExpressionBase test, SerializableExpressionBase ifTrue, SerializableExpressionBase ifFalse)
        : base(type, nodeType)
        {
            this.Test = test;
            this.IfTrue = ifTrue;
            this.IfFalse = ifFalse;
        }

        /// <summary>Gets the test expression.</summary>
        /// <value>The test expression.</value>
        public SerializableExpressionBase Test { get; private set; }

        /// <summary>Gets if true expression.</summary>
        /// <value>If true expression.</value>
        public SerializableExpressionBase IfTrue { get; private set; }

        /// <summary>Gets if false expression.</summary>
        /// <value>If false expression.</value>
        public SerializableExpressionBase IfFalse { get; private set; }
    }

    /// <summary>
    /// Extensions to <see cref="SerializableConditionalExpression" />.
    /// </summary>
    public static class SerializableConditionalExpressionExtensions
    {
        /// <summary>Converts to serializable.</summary>
        /// <param name="conditionalExpression">The conditional expression.</param>
        /// <returns>Serializable expression.</returns>
        public static SerializableConditionalExpression ToSerializable(this ConditionalExpression conditionalExpression)
        {
            var type = conditionalExpression.Type.ToTypeDescription();
            var nodeType = conditionalExpression.NodeType;
            var test = conditionalExpression.Test.ToSerializable();
            var ifTrue = conditionalExpression.IfTrue.ToSerializable();
            var ifFalse = conditionalExpression.IfFalse.ToSerializable();
            var result = new SerializableConditionalExpression(type, nodeType, test, ifTrue, ifFalse);
            return result;
        }

        /// <summary>From the serializable.</summary>
        /// <param name="conditionalExpression">The conditional expression.</param>
        /// <returns>Converted expression.</returns>
        public static ConditionalExpression FromSerializable(this SerializableConditionalExpression conditionalExpression)
        {
            var result = Expression.Condition(
                conditionalExpression.Test.FromSerializable(),
                conditionalExpression.IfTrue.FromSerializable(),
                conditionalExpression.IfFalse.FromSerializable());

            return result;
        }
    }
}