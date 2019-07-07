// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SerializableMemberInitExpression.cs" company="Naos Project">
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
    public class SerializableMemberInitExpression : SerializableExpressionBase
    {
        /// <summary>Initializes a new instance of the <see cref="SerializableMemberInitExpression"/> class.</summary>
        /// <param name="type">The type.</param>
        /// <param name="newExpression">The new expression.</param>
        /// <param name="bindings">The bindings.</param>
        public SerializableMemberInitExpression(TypeDescription type, SerializableNewExpression newExpression, IReadOnlyCollection<SerializableMemberBindingBase> bindings)
            : base(type, ExpressionType.MemberInit)
        {
            this.NewExpression = newExpression;
            this.Bindings = bindings;
        }

        /// <summary>Gets the new expression.</summary>
        /// <value>The new expression.</value>
        public SerializableNewExpression NewExpression { get; private set; }

        /// <summary>Gets the bindings.</summary>
        /// <value>The bindings.</value>
        public IReadOnlyCollection<SerializableMemberBindingBase> Bindings { get; private set; }
    }

    /// <summary>
    /// Extensions to <see cref="SerializableMemberInitExpression" />.
    /// </summary>
    public static class SerializableMemberInitExpressionExtensions
    {
        /// <summary>Converts to serializable.</summary>
        /// <param name="memberInitExpression">The memberInit expression.</param>
        /// <returns>Serializable expression.</returns>
        public static SerializableMemberInitExpression ToSerializable(this MemberInitExpression memberInitExpression)
        {
            var type = memberInitExpression.Type.ToTypeDescription();
            var newExpression = memberInitExpression.NewExpression.ToSerializable();
            var bindings = memberInitExpression.Bindings.ToSerializable();
            var result = new SerializableMemberInitExpression(type, newExpression, bindings);
            return result;
        }

        /// <summary>From the serializable.</summary>
        /// <param name="memberInitExpression">The memberInit expression.</param>
        /// <returns>Converted expression.</returns>
        public static MemberInitExpression FromSerializable(this SerializableMemberInitExpression memberInitExpression)
        {
            var newExpression = memberInitExpression.NewExpression.FromSerializable();
            var bindings = memberInitExpression.Bindings.FromSerializable();
            var result = Expression.MemberInit(newExpression, bindings);

            return result;
        }
    }
}
