// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LambdaExpressionDescription.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Protocol.Domain
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Runtime.InteropServices;
    using OBeautifulCode.Type;

    /// <summary>
    /// Serializable version of <see cref="LambdaExpression" />.
    /// </summary>
    public class LambdaExpressionDescription : ExpressionDescriptionBase
    {
        /// <summary>Initializes a new instance of the <see cref="LambdaExpressionDescription"/> class.</summary>
        /// <param name="type">The type of expression.</param>
        /// <param name="body">The body.</param>
        /// <param name="parameters">The parameters.</param>
        public LambdaExpressionDescription(TypeDescription type, ExpressionDescriptionBase body, IReadOnlyCollection<ParameterExpressionDescription> parameters)
        : base(type, ExpressionType.Lambda)
        {
            this.Body = body;
            this.Parameters = parameters;
        }

        /// <summary>Gets the body.</summary>
        /// <value>The body.</value>
        public ExpressionDescriptionBase Body { get; private set; }

        /// <summary>Gets the parameters.</summary>
        /// <value>The parameters.</value>
        public IReadOnlyCollection<ParameterExpressionDescription> Parameters { get; private set; }
    }

    /// <summary>
    /// Extensions to <see cref="LambdaExpressionDescription" />.
    /// </summary>
    public static class LambdaExpressionDescriptionExtensions
    {
        /// <summary>Converts to serializable.</summary>
        /// <param name="lambdaExpression">The lambda expression.</param>
        /// <returns>Serializable expression.</returns>
        public static LambdaExpressionDescription ToDescription(this LambdaExpression lambdaExpression)
        {
            var type = lambdaExpression.Type.ToTypeDescription();
            var body = lambdaExpression.Body.ToDescription();
            var parameters = lambdaExpression.Parameters.ToDescription();
            var result = new LambdaExpressionDescription(type, body, parameters);
            return result;
        }

        /// <summary>From the serializable.</summary>
        /// <param name="lambdaExpressionDescription">The lambda expression.</param>
        /// <returns>Converted expression.</returns>
        public static LambdaExpression FromDescription(this LambdaExpressionDescription lambdaExpressionDescription)
        {
            var type = lambdaExpressionDescription.Type.ResolveFromLoadedTypes();
            var body = lambdaExpressionDescription.Body.FromDescription();
            var parameters = lambdaExpressionDescription.Parameters.FromDescription().ToList();

            var allParametersFromBody = body.VisitAllNodes().Where(_ => _ is ParameterExpression).Cast<ParameterExpression>().ToList();

            var matchingParametersFromBody = new List<ParameterExpression>();
            foreach (var parameter in parameters)
            {
                var parameterFromBody = allParametersFromBody.SingleOrDefault(allParameter =>
                    parameter.Name == allParameter.Name && parameter.Type == allParameter.Type);

                if (parameterFromBody != null)
                {
                    matchingParametersFromBody.Add(parameterFromBody);
                }
            }

            LambdaExpression result;
            if (matchingParametersFromBody.Any())
            {
                result = Expression.Lambda(type, body, matchingParametersFromBody);
            }
            else
            {
                result = Expression.Lambda(type, body);
            }

            return result;
        }
    }
}
