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
    public class SerializableMemberListBinding : SerializableMemberBindingBase
    {
        /// <summary>Initializes a new instance of the <see cref="SerializableMemberListBinding"/> class.</summary>
        /// <param name="type">The type.</param>
        /// <param name="memberHash">The member hash.</param>
        /// <param name="initializers">The initializers.</param>
        public SerializableMemberListBinding(TypeDescription type, string memberHash, IReadOnlyCollection<SerializableElementInit> initializers)
            : base(type, memberHash, MemberBindingType.ListBinding)
        {
            this.Initializers = initializers;
        }

        /// <summary>Gets the initializers.</summary>
        /// <value>The initializers.</value>
        public IReadOnlyCollection<SerializableElementInit> Initializers { get; private set; }
    }

    /// <summary>
    /// Extensions to <see cref="SerializableMemberListBinding" />.
    /// </summary>
    public static class SerializableMemberListBindingExtensions
    {
        /// <summary>Converts to serializable.</summary>
        /// <param name="memberListBinding">The member list binding.</param>
        /// <returns>Serializable version.</returns>
        public static SerializableMemberListBinding ToSerializable(this MemberListBinding memberListBinding)
        {
            var type = memberListBinding.Member.DeclaringType.ToTypeDescription();
            var memberHash = memberListBinding.Member.GetSignatureHash();
            var initializers = memberListBinding.Initializers.ToSerializable();
            var result = new SerializableMemberListBinding(type, memberHash, initializers);
            return result;
        }

        /// <summary>From the serializable.</summary>
        /// <param name="memberListBinding">The memberListBinding.</param>
        /// <returns>Converted version.</returns>
        public static MemberListBinding FromSerializable(this SerializableMemberListBinding memberListBinding)
        {
            var type = memberListBinding.Type.ResolveFromLoadedTypes();
            var member = type.GetMembers().Single(_ => _.GetSignatureHash().Equals(memberListBinding.MemberHash, StringComparison.OrdinalIgnoreCase));
            var initializers = memberListBinding.Initializers.FromSerializable();

            var result = Expression.ListBind(member, initializers);
            return result;
        }
    }
}
