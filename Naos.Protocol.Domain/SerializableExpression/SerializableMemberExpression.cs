// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SerializableMemberExpression.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Protocol.Domain
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using OBeautifulCode.Type;
    using static System.FormattableString;

    /// <summary>
    /// Serializable version of <see cref="MemberExpression" />.
    /// </summary>
    public class SerializableMemberExpression : SerializableExpressionBase
    {
        /// <summary>Initializes a new instance of the <see cref="SerializableMemberExpression"/> class.</summary>
        /// <param name="type">The type.</param>
        /// <param name="expression">The expresion.</param>
        /// <param name="memberHash">The member hash.</param>
        public SerializableMemberExpression(TypeDescription type, SerializableExpressionBase expression, string memberHash)
            : base(type, ExpressionType.MemberAccess)
        {
            this.Expression = expression;
            this.MemberHash = memberHash;
        }

        /// <summary>Gets the expression.</summary>
        /// <value>The expression.</value>
        public SerializableExpressionBase Expression { get; private set; }

        /// <summary>Gets the member hash.</summary>
        /// <value>The member hash.</value>
        public string MemberHash { get; private set; }
    }

    /// <summary>
    /// Extensions to <see cref="SerializableMemberExpression" />.
    /// </summary>
    public static class SerializableMemberExpressionExtensions
    {
        /// <summary>Gets the signature hash.</summary>
        /// <param name="memberInfo">The member information.</param>
        /// <returns>Hash of signature.</returns>
        public static string GetSignatureHash(this MemberInfo memberInfo)
        {
            var declaringType = memberInfo.DeclaringType?.FullName ?? "<Unknown-MaybeDynamic>";
            var memberName = memberInfo.Name;
            var result = Invariant($"{declaringType}->{memberName})");
            return result;
        }

        /// <summary>Converts to serializable.</summary>
        /// <param name="memberExpression">The member expression.</param>
        /// <returns>Serializable expression.</returns>
        public static SerializableMemberExpression ToSerializable(this MemberExpression memberExpression)
        {
            var type = memberExpression.Type.ToTypeDescription();
            var expression = memberExpression.Expression.ToSerializable();
            var memberHash = memberExpression.Member.GetSignatureHash();
            var result = new SerializableMemberExpression(type, expression, memberHash);
            return result;
        }

        /// <summary>From the serializable.</summary>
        /// <param name="memberExpression">The member expression.</param>
        /// <returns>Converted expression.</returns>
        public static MemberExpression FromSerializable(this SerializableMemberExpression memberExpression)
        {
            var expression = memberExpression.Expression.FromSerializable();
            var type = memberExpression.Type.ResolveFromLoadedTypes();
            var member = type.GetMembers().Single(_ => _.GetSignatureHash().Equals(memberExpression.MemberHash, StringComparison.OrdinalIgnoreCase));
            var result = Expression.MakeMemberAccess(expression, member);

            return result;
        }
    }
}
