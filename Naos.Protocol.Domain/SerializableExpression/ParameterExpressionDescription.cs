// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ParameterExpressionDescription.cs" company="Naos Project">
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
    /// Serializable version of <see cref="ParameterExpression" />.
    /// </summary>
    public class ParameterExpressionDescription : ExpressionDescriptionBase
    {

        /// <summary>Initializes a new instance of the <see cref="ParameterExpressionDescription"/> class.</summary>
        /// <param name="type">The type.</param>
        /// <param name="name">The name.</param>
        public ParameterExpressionDescription(TypeDescription type, string name)
            : base(type, ExpressionType.Parameter)
        {
            this.Name = name;
        }

        /// <summary>Gets the name.</summary>
        /// <value>The name.</value>
        public string Name { get; private set; }
    }

    /// <summary>
    /// Extensions to <see cref="ParameterExpressionDescription" />.
    /// </summary>
    public static class SerializableParameterExpressionExtensions
    {
        /// <summary>Converts to serializable.</summary>
        /// <param name="parameterExpression">The parameter expression.</param>
        /// <returns>Serializable expression.</returns>
        public static ParameterExpressionDescription ToDescription(this ParameterExpression parameterExpression)
        {
            var type = parameterExpression.Type.ToTypeDescription();
            var name = parameterExpression.Name;

            var result = new ParameterExpressionDescription(type, name);
            return result;
        }

        /// <summary>From the serializable.</summary>
        /// <param name="parameterExpressionDescription">The parameter expression.</param>
        /// <returns>Converted expression.</returns>
        public static ParameterExpression FromDescription(this ParameterExpressionDescription parameterExpressionDescription)
        {
            var type = parameterExpressionDescription.Type.ResolveFromLoadedTypes();
            var name = parameterExpressionDescription.Name;

            var result = Expression.Parameter(type, name);
            return result;
        }


        /// <summary>Converts to serializable.</summary>
        /// <param name="expressions">The expressions.</param>
        /// <returns>Converted expressions.</returns>
        public static IReadOnlyCollection<ParameterExpressionDescription> ToDescription(
            this IReadOnlyCollection<ParameterExpression> expressions)
        {
            var result = expressions.Select(_ => _.ToDescription()).ToList();
            return result;
        }

        /// <summary>From the serializable.</summary>
        /// <param name="expressions">The expressions.</param>
        /// <returns>Converted expressions.</returns>
        public static IReadOnlyCollection<ParameterExpression> FromDescription(
            this IReadOnlyCollection<ParameterExpressionDescription> expressions)
        {
            var result = expressions.Select(_ => _.FromDescription()).ToList();
            return result;
        }
    }
}
