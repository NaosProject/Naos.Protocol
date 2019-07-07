// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SerializableInvocationExpression.cs" company="Naos Project">
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
    public class SerializableInvocationExpression : SerializableExpressionBase
    {
        /// <summary>Initializes a new instance of the <see cref="SerializableInvocationExpression"/> class.</summary>
        /// <param name="type">The type of expression.</param>
        /// <param name="expression">The expression to invoke.</param>
        /// <param name="arguments">The arguments to invoke with.</param>
        public SerializableInvocationExpression(TypeDescription type, SerializableExpressionBase expression, IReadOnlyCollection<SerializableExpressionBase> arguments)
            : base(type, ExpressionType.Invoke)
        {
            this.Expression = expression;
            this.Arguments = arguments;
        }

        /// <summary>Gets the expression to invoke.</summary>
        /// <value>The expression to invoke.</value>
        public SerializableExpressionBase Expression { get; private set; }

        /// <summary>Gets the arguments for the expression.</summary>
        /// <value>The arguments for the expression.</value>
        public IReadOnlyCollection<SerializableExpressionBase> Arguments { get; private set; }
    }

    /// <summary>
    /// Extensions to <see cref="SerializableInvocationExpression" />.
    /// </summary>
    public static class SerializableInvocationExpressionExtensions
    {
        /// <summary>Converts to serializable.</summary>
        /// <param name="invocationExpression">The invocation expression.</param>
        /// <returns>Serializable expression.</returns>
        public static SerializableInvocationExpression ToSerializable(this InvocationExpression invocationExpression)
        {
            var type = invocationExpression.Type.ToTypeDescription();
            var expression = invocationExpression.Expression.ToSerializable();
            var arguments = invocationExpression.Arguments.ToSerializable();
            var result = new SerializableInvocationExpression(type, expression, arguments);
            return result;
        }

        /// <summary>From the serializable.</summary>
        /// <param name="invocationExpression">The invocation expression.</param>
        /// <returns>Converted expression.</returns>
        public static InvocationExpression FromSerializable(this SerializableInvocationExpression invocationExpression)
        {
            var expression = invocationExpression.Expression.FromSerializable();
            var arguments = invocationExpression.Arguments.FromSerializable();
            var result = Expression.Invoke(expression, arguments);

            return result;
        }
    }
}
