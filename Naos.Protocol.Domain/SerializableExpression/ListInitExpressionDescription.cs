// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ListInitExpressionDescription.cs" company="Naos Project">
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
    /// Serializable version of <see cref="ListInitExpression" />.
    /// </summary>
    /// <seealso cref="Naos.Protocol.Domain.ExpressionDescriptionBase" />
    public class ListInitExpressionDescription : ExpressionDescriptionBase
    {
        /// <summary>Initializes a new instance of the <see cref="ListInitExpressionDescription"/> class.</summary>
        /// <param name="type">The type of expression.</param>
        /// <param name="newExpressionDescription">The new expression.</param>
        /// <param name="initializers">The initializers.</param>
        public ListInitExpressionDescription(
            TypeDescription type,
            NewExpressionDescription newExpressionDescription,
            IReadOnlyCollection<ElementInitDescription> initializers)
            : base(type, ExpressionType.ListInit)
        {
            this.NewExpressionDescription = newExpressionDescription;
            this.Initializers = initializers;
        }

        /// <summary>Creates new expression description.</summary>
        /// <value>The new expression description.</value>
        public NewExpressionDescription NewExpressionDescription { get; private set; }

        /// <summary>Gets the initializers.</summary>
        /// <value>The initializers.</value>
        public IReadOnlyCollection<ElementInitDescription> Initializers { get; private set; }
    }

    /// <summary>
    /// Extensions to <see cref="ListInitExpressionDescription" />.
    /// </summary>
    public static class ListInitExpressionDescriptionExtensions
    {
        /// <summary>Converts to serializable.</summary>
        /// <param name="listInitExpression">The listInit expression.</param>
        /// <returns>Serializable expression.</returns>
        public static ListInitExpressionDescription ToDescription(this ListInitExpression listInitExpression)
        {
            var type = listInitExpression.Type.ToTypeDescription();
            var newExpression = listInitExpression.NewExpression.ToDescription();
            var initializers = listInitExpression.Initializers.ToDescription();
            var result = new ListInitExpressionDescription(type, newExpression, initializers);
            return result;
        }

        /// <summary>From the serializable.</summary>
        /// <param name="listInitExpressionDescription">The listInit expression.</param>
        /// <returns>Converted expression.</returns>
        public static ListInitExpression FromDescription(this ListInitExpressionDescription listInitExpressionDescription)
        {
            var result = Expression.ListInit(
                listInitExpressionDescription.NewExpressionDescription.FromDescription(),
                listInitExpressionDescription.Initializers.FromDescription().ToArray());

            return result;
        }
    }
}
