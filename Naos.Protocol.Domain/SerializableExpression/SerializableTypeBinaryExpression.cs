// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SerializableTypeBinaryExpression.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Protocol.Domain
{
    using System.Linq.Expressions;
    using OBeautifulCode.Type;

    /// <summary>
    /// Serializable version of <see cref="TypeBinaryExpression" />.
    /// </summary>
    public class SerializableTypeBinaryExpression : SerializableExpressionBase
    {
        /// <summary>Initializes a new instance of the <see cref="SerializableTypeBinaryExpression"/> class.</summary>
        /// <param name="type">The type.</param>
        /// <param name="expression">The expression.</param>
        public SerializableTypeBinaryExpression(TypeDescription type, SerializableExpressionBase expression)
            : base(type, ExpressionType.TypeIs)
        {
            this.Expression = expression;
        }

        /// <summary>Gets the expression.</summary>
        /// <value>The expression.</value>
        public SerializableExpressionBase Expression { get; private set; }
    }

    /// <summary>
    /// Extensions to <see cref="SerializableTypeBinaryExpression" />.
    /// </summary>
    public static class SerializableTypeBinaryExpressionExtensions
    {
        /// <summary>Converts to serializable.</summary>
        /// <param name="typeBinaryExpression">The typeBinary expression.</param>
        /// <returns>Serializable expression.</returns>
        public static SerializableTypeBinaryExpression ToSerializable(this TypeBinaryExpression typeBinaryExpression)
        {
            var type = typeBinaryExpression.Type.ToTypeDescription();
            var expression = typeBinaryExpression.Expression.ToSerializable();
            var result = new SerializableTypeBinaryExpression(type, expression);
            return result;
        }

        /// <summary>From the serializable.</summary>
        /// <param name="typeBinaryExpression">The typeBinary expression.</param>
        /// <returns>Converted expression.</returns>
        public static Expression FromSerializable(this SerializableTypeBinaryExpression typeBinaryExpression)
        {
            var type = typeBinaryExpression.Type.ResolveFromLoadedTypes();
            var expression = typeBinaryExpression.Expression.FromSerializable();
            var result = Expression.TypeIs(expression, type);

            return result;
        }
    }
}
