// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MemberInitExpressionDescription.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Protocol.Domain
{
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using OBeautifulCode.Type;

    /// <summary>
    /// Serializable version of <see cref="MemberInitExpression" />.
    /// </summary>
    public class MemberInitExpressionDescription : ExpressionDescriptionBase
    {
        /// <summary>Initializes a new instance of the <see cref="MemberInitExpressionDescription"/> class.</summary>
        /// <param name="type">The type.</param>
        /// <param name="newExpressionDescription">The new expression.</param>
        /// <param name="bindings">The bindings.</param>
        public MemberInitExpressionDescription(TypeDescription type, NewExpressionDescription newExpressionDescription, IReadOnlyCollection<SerializableMemberBindingBase> bindings)
            : base(type, ExpressionType.MemberInit)
        {
            this.NewExpressionDescription = newExpressionDescription;
            this.Bindings = bindings;
        }

        /// <summary>Gets the new expression.</summary>
        /// <value>The new expression.</value>
        public NewExpressionDescription NewExpressionDescription { get; private set; }

        /// <summary>Gets the bindings.</summary>
        /// <value>The bindings.</value>
        public IReadOnlyCollection<SerializableMemberBindingBase> Bindings { get; private set; }
    }

    /// <summary>
    /// Extensions to <see cref="MemberInitExpressionDescription" />.
    /// </summary>
    public static class SerializableMemberInitExpressionExtensions
    {
        /// <summary>Converts to serializable.</summary>
        /// <param name="memberInitExpression">The memberInit expression.</param>
        /// <returns>Serializable expression.</returns>
        public static MemberInitExpressionDescription ToDescription(this MemberInitExpression memberInitExpression)
        {
            var type = memberInitExpression.Type.ToTypeDescription();
            var newExpression = memberInitExpression.NewExpression.ToDescription();
            var bindings = memberInitExpression.Bindings.ToDescription();
            var result = new MemberInitExpressionDescription(type, newExpression, bindings);
            return result;
        }

        /// <summary>From the serializable.</summary>
        /// <param name="memberInitExpressionDescription">The memberInit expression.</param>
        /// <returns>Converted expression.</returns>
        public static MemberInitExpression FromDescription(this MemberInitExpressionDescription memberInitExpressionDescription)
        {
            var newExpression = memberInitExpressionDescription.NewExpressionDescription.FromDescription();
            var bindings = memberInitExpressionDescription.Bindings.FromDescription();
            var result = Expression.MemberInit(newExpression, bindings);

            return result;
        }
    }
}
