// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SerializableMemberAssignment.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Protocol.Domain
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using OBeautifulCode.Type;

    /// <summary>
    /// Serializable version of <see cref="MemberAssignment" />.
    /// </summary>
    public class SerializableMemberAssignment : SerializableMemberBindingBase
    {
        /// <summary>Initializes a new instance of the <see cref="SerializableMemberAssignment"/> class.</summary>
        /// <param name="type">The type.</param>
        /// <param name="memberHash">The member hash.</param>
        /// <param name="expressionDescription">The expression.</param>
        public SerializableMemberAssignment(TypeDescription type, string memberHash, ExpressionDescriptionBase expressionDescription)
            : base(type, memberHash, MemberBindingType.Assignment)
        {
            this.ExpressionDescription = expressionDescription;
        }

        /// <summary>Gets the expression.</summary>
        /// <value>The expression.</value>
        public ExpressionDescriptionBase ExpressionDescription { get; private set; }
    }

    /// <summary>
    /// Extensions to <see cref="SerializableMemberAssignment" />.
    /// </summary>
    public static class SerializableMemberAssignmentExtensions
    {
        /// <summary>Converts to serializable.</summary>
        /// <param name="memberAssignment">The memberAssignment.</param>
        /// <returns>Serializable version.</returns>
        public static SerializableMemberAssignment ToDescription(this MemberAssignment memberAssignment)
        {
            var type = memberAssignment.Member.DeclaringType.ToTypeDescription();
            var expression = memberAssignment.Expression.ToDescription();
            var memberHash = memberAssignment.Member.GetSignatureHash();
            var result = new SerializableMemberAssignment(type, memberHash, expression);
            return result;
        }

        /// <summary>From the serializable.</summary>
        /// <param name="memberAssignment">The memberAssignment.</param>
        /// <returns>Converted version.</returns>
        public static MemberAssignment FromDescription(this SerializableMemberAssignment memberAssignment)
        {
            var type = memberAssignment.Type.ResolveFromLoadedTypes();
            var member = type.GetMembers().Single(_ => _.GetSignatureHash().Equals(memberAssignment.MemberHash, StringComparison.OrdinalIgnoreCase));
            var expression = memberAssignment.ExpressionDescription.FromDescription();

            var result = Expression.Bind(member, expression);
            return result;
        }
    }
}
