// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MemberMemberBindingDescription.cs" company="Naos Project">
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
    public class MemberMemberBindingDescription : MemberBindingDescriptionBase
    {
        /// <summary>Initializes a new instance of the <see cref="MemberMemberBindingDescription"/> class.</summary>
        /// <param name="type">The type.</param>
        /// <param name="memberHash">The member hash.</param>
        /// <param name="bindings">The bindings.</param>
        public MemberMemberBindingDescription(TypeDescription type, string memberHash, IReadOnlyCollection<MemberBindingDescriptionBase> bindings)
        : base(type, memberHash, MemberBindingType.MemberBinding)
        {
            this.Bindings = bindings;
        }

        /// <summary>Gets the bindings.</summary>
        /// <value>The bindings.</value>
        public IReadOnlyCollection<MemberBindingDescriptionBase> Bindings { get; private set; }
    }

    /// <summary>
    /// Extensions to <see cref="MemberMemberBindingDescription" />.
    /// </summary>
    public static class MemberMemberBindingDescriptionExtensions
    {
        /// <summary>Converts to serializable.</summary>
        /// <param name="memberMemberBinding">The memberMemberBindingDescription.</param>
        /// <returns>Serializable version.</returns>
        public static MemberMemberBindingDescription ToDescription(this MemberMemberBinding memberMemberBinding)
        {
            var type = memberMemberBinding.Member.DeclaringType.ToTypeDescription();
            var memberHash = memberMemberBinding.Member.GetSignatureHash();
            var bindings = memberMemberBinding.Bindings.ToDescription();
            var result = new MemberMemberBindingDescription(type, memberHash, bindings);
            return result;
        }

        /// <summary>From the serializable.</summary>
        /// <param name="memberMemberBindingDescription">The memberMemberBindingDescription.</param>
        /// <returns>Converted version.</returns>
        public static MemberMemberBinding FromDescription(this MemberMemberBindingDescription memberMemberBindingDescription)
        {
            var type = memberMemberBindingDescription.Type.ResolveFromLoadedTypes();
            var member = type.GetMembers().Single(_ => _.GetSignatureHash().Equals(memberMemberBindingDescription.MemberHash, StringComparison.OrdinalIgnoreCase));
            var bindings = memberMemberBindingDescription.Bindings.FromDescription();

            var result = Expression.MemberBind(member, bindings);
            return result;
        }
    }
}
