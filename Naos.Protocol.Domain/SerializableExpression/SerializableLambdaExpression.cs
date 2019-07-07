// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SerializableLambdaExpression.cs" company="Naos Project">
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
    /// Serializable version of <see cref="LambdaExpression" />.
    /// </summary>
    public class SerializableLambdaExpression : SerializableExpressionBase
    {
        /// <summary>Initializes a new instance of the <see cref="SerializableLambdaExpression"/> class.</summary>
        /// <param name="type">The type of expression.</param>
        /// <param name="body">The body.</param>
        /// <param name="parameters">The parameters.</param>
        public SerializableLambdaExpression(TypeDescription type, SerializableExpressionBase body, IReadOnlyCollection<SerializableParameterExpression> parameters)
        : base(type, ExpressionType.Lambda)
        {
            this.Body = body;
            this.Parameters = parameters;
        }

        /// <summary>Gets the body.</summary>
        /// <value>The body.</value>
        public SerializableExpressionBase Body { get; private set; }

        /// <summary>Gets the parameters.</summary>
        /// <value>The parameters.</value>
        public IReadOnlyCollection<SerializableParameterExpression> Parameters { get; private set; }
    }

    /// <summary>
    /// Extensions to <see cref="SerializableLambdaExpression" />.
    /// </summary>
    public static class SerializableLambdaExpressionExtensions
    {
        /// <summary>Converts to serializable.</summary>
        /// <param name="lambdaExpression">The lambda expression.</param>
        /// <returns>Serializable expression.</returns>
        public static SerializableLambdaExpression ToSerializable(this LambdaExpression lambdaExpression)
        {
            var type = lambdaExpression.Type.ToTypeDescription();
            var body = lambdaExpression.Body.ToSerializable();
            var parameters = lambdaExpression.Parameters.ToSerializable();
            var result = new SerializableLambdaExpression(type, body, parameters);
            return result;
        }

        /// <summary>From the serializable.</summary>
        /// <param name="lambdaExpression">The lambda expression.</param>
        /// <returns>Converted expression.</returns>
        public static LambdaExpression FromSerializable(this SerializableLambdaExpression lambdaExpression)
        {
            var body = lambdaExpression.Body.FromSerializable();
            var parameters = lambdaExpression.Parameters.FromSerializable();

            var result = Expression.Lambda(lambdaExpression.Type.ResolveFromLoadedTypes(), body, parameters);
            return result;
        }
    }
}
