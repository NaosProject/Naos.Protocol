// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SerializableConstantExpression.cs" company="Naos Project">
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
    public class SerializableConstantExpression<T> : SerializableExpressionBase
    {
        /// <summary>Initializes a new instance of the <see cref="SerializableConstantExpression{T}"/> class.</summary>
        /// <param name="type">The type of expression.</param>
        /// <param name="value">The value.</param>
        public SerializableConstantExpression(TypeDescription type, object value)
            : base(type, ExpressionType.Constant)
        {
            this.Value = value;
        }

        /// <summary>Gets the value.</summary>
        /// <value>The value.</value>
        public object Value { get; private set; }
    }

    /// <summary>
    /// Extensions to <see cref="SerializableConstantExpression{T}" />.
    /// </summary>
    public static class SerializableConstantExpressionExtensions
    {
        /// <summary>Converts to serializable.</summary>
        /// <param name="constantExpression">The constant expression.</param>
        /// <returns>Converted expression.</returns>
        public static SerializableExpressionBase ToSerializable(this ConstantExpression constantExpression)
        {
            var type = constantExpression.Type.ToTypeDescription();
            var value = constantExpression.Value;
            var resultType = typeof(SerializableConstantExpression<>).MakeGenericType(value.GetType());
            var result = resultType.Construct(type, value);
            return (SerializableExpressionBase)result;
        }

        /// <summary>From the serializable.</summary>
        /// <param name="constantExpression">The constant expression.</param>
        /// <typeparam name="T">Type of constant.</typeparam>
        /// <returns>Converted expression.</returns>
        public static ConstantExpression FromSerializable<T>(this SerializableConstantExpression<T> constantExpression)
        {
            var result = Expression.Constant(constantExpression.Value);
            return result;
        }
    }
}