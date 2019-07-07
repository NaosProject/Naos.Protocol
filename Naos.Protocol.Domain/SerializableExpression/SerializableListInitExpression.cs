// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SerializableListInitExpression.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Protocol.Domain
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using OBeautifulCode.Type;

    public class SerializableListInitExpression : SerializableExpressionBase
    {
        /// <summary>Initializes a new instance of the <see cref="SerializableListInitExpression"/> class.</summary>
        /// <param name="type">The type of expression.</param>
        /// <param name="initializers">The initializers.</param>
        public SerializableListInitExpression(
            TypeDescription type,
            SerializableNewExpression newExpression,
            IReadOnlyCollection<SerializableElementInit> initializers)
            : base(type, ExpressionType.ListInit)
        {
            this.NewExpression = newExpression;
            this.Initializers = initializers;
        }

        public SerializableNewExpression NewExpression { get; private set; }

        public IReadOnlyCollection<SerializableElementInit> Initializers { get; private set; }
    }

    /// <summary>
    /// Extensions to <see cref="SerializableListInitExpression" />.
    /// </summary>
    public static class SerializableListInitExpressionExtensions
    {
        /// <summary>Converts to serializable.</summary>
        /// <param name="listInitExpression">The listInit expression.</param>
        /// <returns>Serializable expression.</returns>
        public static SerializableListInitExpression ToSerializable(this ListInitExpression listInitExpression)
        {
            var type = listInitExpression.Type.ToTypeDescription();
            var newExpresion = listInitExpression.NewExpression.ToSerializable();
            var initializers = listInitExpression.Initializers.ToSerializable();
            var result = new SerializableListInitExpression(type, newExpresion, initializers);
            return result;
        }

        /// <summary>From the serializable.</summary>
        /// <param name="listInitExpression">The listInit expression.</param>
        /// <returns>Converted expression.</returns>
        public static ListInitExpression FromSerializable(this SerializableListInitExpression listInitExpression)
        {
            var result = Expression.ListInit(
                listInitExpression.NewExpression.FromSerializable(),
                listInitExpression.Initializers.FromSerializable().ToArray());

            return result;
        }
    }
}
