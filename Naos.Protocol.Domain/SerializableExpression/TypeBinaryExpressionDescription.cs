// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TypeBinaryExpressionDescription.cs" company="Naos Project">
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
    public class TypeBinaryExpressionDescription : ExpressionDescriptionBase
    {
        /// <summary>Initializes a new instance of the <see cref="TypeBinaryExpressionDescription"/> class.</summary>
        /// <param name="type">The type.</param>
        /// <param name="expressionDescription">The expression.</param>
        public TypeBinaryExpressionDescription(TypeDescription type, ExpressionDescriptionBase expressionDescription)
            : base(type, ExpressionType.TypeIs)
        {
            this.ExpressionDescription = expressionDescription;
        }

        /// <summary>Gets the expression.</summary>
        /// <value>The expression.</value>
        public ExpressionDescriptionBase ExpressionDescription { get; private set; }
    }

    /// <summary>
    /// Extensions to <see cref="TypeBinaryExpressionDescription" />.
    /// </summary>
    public static class TypeBinaryExpressionDescriptionExtensions
    {
        /// <summary>Converts to serializable.</summary>
        /// <param name="typeBinaryExpression">The typeBinary expression.</param>
        /// <returns>Serializable expression.</returns>
        public static TypeBinaryExpressionDescription ToDescription(this TypeBinaryExpression typeBinaryExpression)
        {
            var type = typeBinaryExpression.Type.ToTypeDescription();
            var expression = typeBinaryExpression.Expression.ToDescription();
            var result = new TypeBinaryExpressionDescription(type, expression);
            return result;
        }

        /// <summary>From the serializable.</summary>
        /// <param name="typeBinaryExpressionDescription">The typeBinary expression.</param>
        /// <returns>Converted expression.</returns>
        public static Expression FromDescription(this TypeBinaryExpressionDescription typeBinaryExpressionDescription)
        {
            var type = typeBinaryExpressionDescription.Type.ResolveFromLoadedTypes();
            var expression = typeBinaryExpressionDescription.ExpressionDescription.FromDescription();
            var result = Expression.TypeIs(expression, type);

            return result;
        }
    }
}
