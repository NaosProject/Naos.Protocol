// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SerializableMemberMemberBinding.cs" company="Naos Project">
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

    /// <summary>
    /// Serializable version of <see cref="MemberMemberBinding" />.
    /// </summary>
    public class SerializableMemberMemberBinding : SerializableMemberBindingBase
    {
        /// <summary>Initializes a new instance of the <see cref="SerializableMemberMemberBinding"/> class.</summary>
        /// <param name="type">The type.</param>
        /// <param name="memberHash">The member hash.</param>
        /// <param name="bindings">The bindings.</param>
        public SerializableMemberMemberBinding(TypeDescription type, string memberHash, IReadOnlyCollection<SerializableMemberBindingBase> bindings)
        : base(type, memberHash, MemberBindingType.MemberBinding)
        {
            this.Bindings = bindings;
        }

        /// <summary>Gets the bindings.</summary>
        /// <value>The bindings.</value>
        public IReadOnlyCollection<SerializableMemberBindingBase> Bindings { get; private set; }
    }

    /// <summary>
    /// Extensions to <see cref="SerializableMemberMemberBinding" />.
    /// </summary>
    public static class SerializableMemberMemberBindingExtensions
    {
        /// <summary>Converts to serializable.</summary>
        /// <param name="memberMemberBinding">The memberMemberBinding.</param>
        /// <returns>Serializable version.</returns>
        public static SerializableMemberMemberBinding ToSerializable(this MemberMemberBinding memberMemberBinding)
        {
            var type = memberMemberBinding.Member.DeclaringType.ToTypeDescription();
            var memberHash = memberMemberBinding.Member.GetSignatureHash();
            var bindings = memberMemberBinding.Bindings.ToSerializable();
            var result = new SerializableMemberMemberBinding(type, memberHash, bindings);
            return result;
        }

        /// <summary>From the serializable.</summary>
        /// <param name="memberMemberBinding">The memberMemberBinding.</param>
        /// <returns>Converted version.</returns>
        public static MemberMemberBinding FromSerializable(this SerializableMemberMemberBinding memberMemberBinding)
        {
            var type = memberMemberBinding.Type.ResolveFromLoadedTypes();
            var member = type.GetMembers().Single(_ => _.GetSignatureHash().Equals(memberMemberBinding.MemberHash, StringComparison.OrdinalIgnoreCase));
            var bindings = memberMemberBinding.Bindings.FromSerializable();

            var result = Expression.MemberBind(member, bindings);
            return result;
        }
    }
}
