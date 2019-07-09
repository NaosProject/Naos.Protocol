// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Channel.cs" company="Naos Project">
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
    /// Serializable version of <see cref="MemberListBinding" />.
    /// </summary>
    public class MemberListBindingDescription : MemberBindingDescriptionBase
    {
        /// <summary>Initializes a new instance of the <see cref="MemberListBindingDescription"/> class.</summary>
        /// <param name="type">The type.</param>
        /// <param name="memberHash">The member hash.</param>
        /// <param name="initializers">The initializers.</param>
        public MemberListBindingDescription(TypeDescription type, string memberHash, IReadOnlyCollection<ElementInitDescription> initializers)
            : base(type, memberHash, MemberBindingType.ListBinding)
        {
            this.Initializers = initializers;
        }

        /// <summary>Gets the initializers.</summary>
        /// <value>The initializers.</value>
        public IReadOnlyCollection<ElementInitDescription> Initializers { get; private set; }
    }

    /// <summary>
    /// Extensions to <see cref="MemberListBindingDescription" />.
    /// </summary>
    public static class MemberListBindingDescriptionExtensions
    {
        /// <summary>Converts to serializable.</summary>
        /// <param name="memberListBinding">The member list binding.</param>
        /// <returns>Serializable version.</returns>
        public static MemberListBindingDescription ToDescription(this MemberListBinding memberListBinding)
        {
            var type = memberListBinding.Member.DeclaringType.ToTypeDescription();
            var memberHash = memberListBinding.Member.GetSignatureHash();
            var initializers = memberListBinding.Initializers.ToDescription();
            var result = new MemberListBindingDescription(type, memberHash, initializers);
            return result;
        }

        /// <summary>From the serializable.</summary>
        /// <param name="memberListBindingDescription">The memberListBindingDescription.</param>
        /// <returns>Converted version.</returns>
        public static MemberListBinding FromDescription(this MemberListBindingDescription memberListBindingDescription)
        {
            var type = memberListBindingDescription.Type.ResolveFromLoadedTypes();
            var member = type.GetMembers().Single(_ => _.GetSignatureHash().Equals(memberListBindingDescription.MemberHash, StringComparison.OrdinalIgnoreCase));
            var initializers = memberListBindingDescription.Initializers.FromDescription();

            var result = Expression.ListBind(member, initializers);
            return result;
        }
    }
}
