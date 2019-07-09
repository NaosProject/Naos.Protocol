// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ConstantExpressionDescription.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Protocol.Domain
{
    using System.Linq.Expressions;
    using OBeautifulCode.Reflection.Recipes;
    using OBeautifulCode.Type;

    /// <summary>
    /// Serializable version of <see cref="ConstantExpression" />.
    /// </summary>
    /// <typeparam name="T">Type of the value.</typeparam>
    public class ConstantExpressionDescription<T> : ExpressionDescriptionBase
    {
        /// <summary>Initializes a new instance of the <see cref="ConstantExpressionDescription{T}"/> class.</summary>
        /// <param name="type">The type of expression.</param>
        /// <param name="value">The value.</param>
        public ConstantExpressionDescription(TypeDescription type, T value)
            : base(type, ExpressionType.Constant)
        {
            this.Value = value;
        }

        /// <summary>Gets the value.</summary>
        /// <value>The value.</value>
        public T Value { get; private set; }
    }

    /// <summary>
    /// Extensions to <see cref="ConstantExpressionDescription{T}" />.
    /// </summary>
    public static class ConstantExpressionDescriptionExtensions
    {
        /// <summary>Converts to serializable.</summary>
        /// <param name="constantExpression">The constant expression.</param>
        /// <returns>Converted expression.</returns>
        public static ExpressionDescriptionBase ToDescription(this ConstantExpression constantExpression)
        {
            var type = constantExpression.Type.ToTypeDescription();
            var value = constantExpression.Value;
            var resultType = typeof(ConstantExpressionDescription<>).MakeGenericType(value.GetType());
            var result = resultType.Construct(type, value);
            return (ExpressionDescriptionBase)result;
        }

        /// <summary>From the serializable.</summary>
        /// <param name="constantExpressionDescription">The constant expression.</param>
        /// <typeparam name="T">Type of constant.</typeparam>
        /// <returns>Converted expression.</returns>
        public static ConstantExpression FromDescription<T>(this ConstantExpressionDescription<T> constantExpressionDescription)
        {
            var result = Expression.Constant(constantExpressionDescription.Value);
            return result;
        }
    }
}