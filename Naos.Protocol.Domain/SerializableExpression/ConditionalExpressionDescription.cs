// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ConditionalExpressionDescription.cs" company="Naos Project">
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
    public class ConditionalExpressionDescription : ExpressionDescriptionBase
    {
        /// <summary>Initializes a new instance of the <see cref="ConditionalExpressionDescription"/> class.</summary>
        /// <param name="type">The type of expression.</param>
        /// <param name="nodeType">Type of the node.</param>
        /// <param name="test">The test expression.</param>
        /// <param name="ifTrue">If true expression.</param>
        /// <param name="ifFalse">If false expression.</param>
        public ConditionalExpressionDescription(TypeDescription type, ExpressionType nodeType, ExpressionDescriptionBase test, ExpressionDescriptionBase ifTrue, ExpressionDescriptionBase ifFalse)
        : base(type, nodeType)
        {
            this.Test = test;
            this.IfTrue = ifTrue;
            this.IfFalse = ifFalse;
        }

        /// <summary>Gets the test expression.</summary>
        /// <value>The test expression.</value>
        public ExpressionDescriptionBase Test { get; private set; }

        /// <summary>Gets if true expression.</summary>
        /// <value>If true expression.</value>
        public ExpressionDescriptionBase IfTrue { get; private set; }

        /// <summary>Gets if false expression.</summary>
        /// <value>If false expression.</value>
        public ExpressionDescriptionBase IfFalse { get; private set; }
    }

    /// <summary>
    /// Extensions to <see cref="ConditionalExpressionDescription" />.
    /// </summary>
    public static class ConditionalExpressionDescriptionExtensions
    {
        /// <summary>Converts to serializable.</summary>
        /// <param name="conditionalExpression">The conditional expression.</param>
        /// <returns>Serializable expression.</returns>
        public static ConditionalExpressionDescription ToDescription(this ConditionalExpression conditionalExpression)
        {
            var type = conditionalExpression.Type.ToTypeDescription();
            var nodeType = conditionalExpression.NodeType;
            var test = conditionalExpression.Test.ToDescription();
            var ifTrue = conditionalExpression.IfTrue.ToDescription();
            var ifFalse = conditionalExpression.IfFalse.ToDescription();
            var result = new ConditionalExpressionDescription(type, nodeType, test, ifTrue, ifFalse);
            return result;
        }

        /// <summary>From the serializable.</summary>
        /// <param name="conditionalExpressionDescription">The conditional expression.</param>
        /// <returns>Converted expression.</returns>
        public static ConditionalExpression FromDescription(this ConditionalExpressionDescription conditionalExpressionDescription)
        {
            var result = Expression.Condition(
                conditionalExpressionDescription.Test.FromDescription(),
                conditionalExpressionDescription.IfTrue.FromDescription(),
                conditionalExpressionDescription.IfFalse.FromDescription());

            return result;
        }
    }
}