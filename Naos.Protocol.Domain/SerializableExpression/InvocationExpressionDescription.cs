// --------------------------------------------------------------------------------------------------------------------
// <copyright file="InvocationExpressionDescription.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Protocol.Domain
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using OBeautifulCode.Type;

    /// <summary>
    /// Serializable version of <see cref="InvocationExpression" />.
    /// </summary>
    public class InvocationExpressionDescription : ExpressionDescriptionBase
    {
        /// <summary>Initializes a new instance of the <see cref="InvocationExpressionDescription"/> class.</summary>
        /// <param name="type">The type of expression.</param>
        /// <param name="expressionDescription">The expression to invoke.</param>
        /// <param name="arguments">The arguments to invoke with.</param>
        public InvocationExpressionDescription(TypeDescription type, ExpressionDescriptionBase expressionDescription, IReadOnlyCollection<ExpressionDescriptionBase> arguments)
            : base(type, ExpressionType.Invoke)
        {
            this.ExpressionDescription = expressionDescription;
            this.Arguments = arguments;
        }

        /// <summary>Gets the expression to invoke.</summary>
        /// <value>The expression to invoke.</value>
        public ExpressionDescriptionBase ExpressionDescription { get; private set; }

        /// <summary>Gets the arguments for the expression.</summary>
        /// <value>The arguments for the expression.</value>
        public IReadOnlyCollection<ExpressionDescriptionBase> Arguments { get; private set; }
    }

    /// <summary>
    /// Extensions to <see cref="InvocationExpressionDescription" />.
    /// </summary>
    public static class InvocationExpressionDescriptionExtensions
    {
        /// <summary>Converts to serializable.</summary>
        /// <param name="invocationExpression">The invocation expression.</param>
        /// <returns>Serializable expression.</returns>
        public static InvocationExpressionDescription ToDescription(this InvocationExpression invocationExpression)
        {
            var type = invocationExpression.Type.ToTypeDescription();
            var expression = invocationExpression.Expression.ToDescription();
            var arguments = invocationExpression.Arguments.ToDescription();
            var result = new InvocationExpressionDescription(type, expression, arguments);
            return result;
        }

        /// <summary>From the serializable.</summary>
        /// <param name="invocationExpressionDescription">The invocation expression.</param>
        /// <returns>Converted expression.</returns>
        public static InvocationExpression FromDescription(this InvocationExpressionDescription invocationExpressionDescription)
        {
            var expression = invocationExpressionDescription.ExpressionDescription.FromDescription();
            var arguments = invocationExpressionDescription.Arguments.FromDescription();
            var result = Expression.Invoke(expression, arguments);

            return result;
        }
    }
}
