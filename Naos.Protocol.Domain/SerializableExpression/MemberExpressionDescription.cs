// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MemberExpressionDescription.cs" company="Naos Project">
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
    public class MemberExpressionDescription : ExpressionDescriptionBase
    {
        /// <summary>Initializes a new instance of the <see cref="MemberExpressionDescription"/> class.</summary>
        /// <param name="type">The type.</param>
        /// <param name="expressionDescription">The expresion.</param>
        /// <param name="memberHash">The member hash.</param>
        public MemberExpressionDescription(TypeDescription type, ExpressionDescriptionBase expressionDescription, string memberHash)
            : base(type, ExpressionType.MemberAccess)
        {
            this.ExpressionDescription = expressionDescription;
            this.MemberHash = memberHash;
        }

        /// <summary>Gets the expression.</summary>
        /// <value>The expression.</value>
        public ExpressionDescriptionBase ExpressionDescription { get; private set; }

        /// <summary>Gets the member hash.</summary>
        /// <value>The member hash.</value>
        public string MemberHash { get; private set; }
    }

    /// <summary>
    /// Extensions to <see cref="MemberExpressionDescription" />.
    /// </summary>
    public static class MemberExpressionDescriptionExtensions
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
        public static MemberExpressionDescription ToDescription(this MemberExpression memberExpression)
        {
            var type = memberExpression.Type.ToTypeDescription();
            var expression = memberExpression.Expression.ToDescription();
            var memberHash = memberExpression.Member.GetSignatureHash();
            var result = new MemberExpressionDescription(type, expression, memberHash);
            return result;
        }

        /// <summary>From the serializable.</summary>
        /// <param name="memberExpressionDescription">The member expression.</param>
        /// <returns>Converted expression.</returns>
        public static MemberExpression FromDescription(this MemberExpressionDescription memberExpressionDescription)
        {
            var expression = memberExpressionDescription.ExpressionDescription.FromDescription();
            var type = memberExpressionDescription.Type.ResolveFromLoadedTypes();
            var member = type.GetMembers().Single(_ => _.GetSignatureHash().Equals(memberExpressionDescription.MemberHash, StringComparison.OrdinalIgnoreCase));
            var result = Expression.MakeMemberAccess(expression, member);

            return result;
        }
    }
}
