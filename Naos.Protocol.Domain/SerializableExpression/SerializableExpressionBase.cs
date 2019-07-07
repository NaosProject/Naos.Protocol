// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SerializableExpressionBase.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Protocol.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using OBeautifulCode.Type;
    using static System.FormattableString;

    /// <summary>
    /// Serializable version of <see cref="Expression" />.
    /// </summary>
    public abstract class SerializableExpressionBase
    {
        /// <summary>Initializes a new instance of the <see cref="SerializableExpressionBase"/> class.</summary>
        /// <param name="type">The type of expression.</param>
        /// <param name="nodeType">The node type.</param>
        protected SerializableExpressionBase(TypeDescription type, ExpressionType nodeType)
        {
            this.Type = type;
            this.NodeType = nodeType;
        }

        /// <summary>Gets the type of the node.</summary>
        /// <value>The type of the node.</value>
        public ExpressionType NodeType { get; private set; }

        /// <summary>Gets the type.</summary>
        /// <value>The type.</value>
        public TypeDescription Type { get; private set; }
    }

    /// <summary>
    /// Extensions to <see cref="SerializableExpressionBase" />.
    /// </summary>
    public static class SerializableExpressionExtensions
    {
        /// <summary>Converts to a serializable.</summary>
        /// <param name="expression">The expression.</param>
        /// <returns>Serializable expression.</returns>
        public static SerializableExpressionBase ToSerializable(this Expression expression)
        {
            if (expression is BinaryExpression binaryExpression)
            {
                return binaryExpression.ToSerializable();
            }
            else if (expression is ConditionalExpression conditionalExpression)
            {
                return conditionalExpression.ToSerializable();
            }
            else if (expression is ConstantExpression constantExpression)
            {
                return constantExpression.ToSerializable();
            }
            else if (expression is InvocationExpression invocationExpression)
            {
                return invocationExpression.ToSerializable();
            }
            else if (expression is LambdaExpression lambdaExpression)
            {
                return lambdaExpression.ToSerializable();
            }
            else if (expression is ListInitExpression listInitExpression)
            {
                return listInitExpression.ToSerializable();
            }
            else if (expression is MemberExpression memberExpression)
            {
                return memberExpression.ToSerializable();
            }
            else if (expression is MemberInitExpression memberInitExpression)
            {
                return memberInitExpression.ToSerializable();
            }
            else if (expression is MethodCallExpression methodCallExpression)
            {
                return methodCallExpression.ToSerializable();
            }
            else if (expression is NewArrayExpression newArrayExpression)
            {
                return newArrayExpression.ToSerializable();
            }
            else if (expression is NewExpression newExpression)
            {
                return newExpression.ToSerializable();
            }
            else if (expression is ParameterExpression parameterExpression)
            {
                return parameterExpression.ToSerializable();
            }
            else if (expression is TypeBinaryExpression typeBinaryExpression)
            {
                return typeBinaryExpression.ToSerializable();
            }
            else if (expression is UnaryExpression unaryExpression)
            {
                return unaryExpression.ToSerializable();
            }
            else
            {
                throw new NotSupportedException(Invariant($"Expression type '{expression.GetType()}' is not supported."));
            }
        }

        /// <summary>Converts from serializable.</summary>
        /// <param name="expression">The serializable expression.</param>
        /// <returns>Converted version.</returns>
        public static Expression FromSerializable(this SerializableExpressionBase expression)
        {
            if (expression is SerializableBinaryExpression binaryExpression)
            {
                return binaryExpression.FromSerializable();
            }
            else if (expression is SerializableConditionalExpression conditionalExpression)
            {
                return conditionalExpression.FromSerializable();
            }
            else if (expression.GetType().IsGenericType && expression.GetType().GetGenericTypeDefinition() == typeof(SerializableConstantExpression<>))
            {
                throw new NotImplementedException();
                //return constantExpression.FromSerializable();
            }
            else if (expression is SerializableInvocationExpression invocationExpression)
            {
                return invocationExpression.FromSerializable();
            }
            else if (expression is SerializableLambdaExpression lambdaExpression)
            {
                return lambdaExpression.FromSerializable();
            }
            else if (expression is SerializableListInitExpression listInitExpression)
            {
                return listInitExpression.FromSerializable();
            }
            else if (expression is SerializableMemberExpression memberExpression)
            {
                return memberExpression.FromSerializable();
            }
            else if (expression is SerializableMemberInitExpression memberInitExpression)
            {
                return memberInitExpression.FromSerializable();
            }
            else if (expression is SerializableMethodCallExpression methodCallExpression)
            {
                return methodCallExpression.FromSerializable();
            }
            else if (expression is SerializableNewArrayExpression newArrayExpression)
            {
                return newArrayExpression.FromSerializable();
            }
            else if (expression is SerializableNewExpression newExpression)
            {
                return newExpression.FromSerializable();
            }
            else if (expression is SerializableParameterExpression parameterExpression)
            {
                return parameterExpression.FromSerializable();
            }
            else if (expression is SerializableTypeBinaryExpression typeBinaryExpression)
            {
                return typeBinaryExpression.FromSerializable();
            }
            else if (expression is SerializableUnaryExpression unaryExpression)
            {
                return unaryExpression.FromSerializable();
            }
            else
            {
                throw new NotSupportedException(Invariant($"Expression type '{expression.GetType()}' is not supported."));
            }
        }

        /// <summary>Converts to serializable.</summary>
        /// <param name="expressions">The expressions.</param>
        /// <returns>Converted expressions.</returns>
        public static IReadOnlyCollection<SerializableExpressionBase> ToSerializable(
            this IReadOnlyCollection<Expression> expressions)
        {
            var result = expressions.Select(_ => _.ToSerializable()).ToList();
            return result;
        }

        /// <summary>Froms the serializable.</summary>
        /// <param name="expressions">The expressions.</param>
        /// <returns>Converted expressions.</returns>
        public static IReadOnlyCollection<Expression> FromSerializable(
            this IReadOnlyCollection<SerializableExpressionBase> expressions)
        {
            var result = expressions.Select(_ => _.FromSerializable()).ToList();
            return result;
        }
    }
}
