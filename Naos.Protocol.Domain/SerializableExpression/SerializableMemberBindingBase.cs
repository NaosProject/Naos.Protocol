﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SerializableMemberBindingBase.cs" company="Naos Project">
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
    using static System.FormattableString;

    /// <summary>
    /// Serializable version of <see cref="MemberBinding" />.
    /// </summary>
    public abstract class SerializableMemberBindingBase
    {
        /// <summary>Initializes a new instance of the <see cref="SerializableMemberBindingBase"/> class.</summary>
        /// <param name="type">The type with member.</param>
        /// <param name="memberHash">The member hash.</param>
        /// <param name="bindingType">Type of the binding.</param>
        protected SerializableMemberBindingBase(TypeDescription type, string memberHash, MemberBindingType bindingType)
        {
            this.Type = type;
            this.MemberHash = memberHash;
            this.BindingType = bindingType;
        }

        /// <summary>Gets the type with member.</summary>
        /// <value>The type with member.</value>
        public TypeDescription Type { get; private set; }

        /// <summary>Gets or sets the member hash.</summary>
        /// <value>The member hash.</value>
        public string MemberHash { get; private set; }

        /// <summary>Gets the type of the binding.</summary>
        /// <value>The type of the binding.</value>
        public MemberBindingType BindingType { get; private set; }
    }

    /// <summary>
    /// Extensions to <see cref="SerializableMemberBindingBase" />.
    /// </summary>
    public static class SerializableMemberBindingExtensions
    {
        /// <summary>Converts to serializable.</summary>
        /// <param name="memberBinding">The memberBindings.</param>
        /// <returns>Serializable version.</returns>
        public static SerializableMemberBindingBase ToSerializable(this MemberBinding memberBinding)
        {
            if (memberBinding is MemberAssignment memberAssignment)
            {
                return memberAssignment.ToSerializable();
            }
            else if (memberBinding is MemberListBinding memberListBinding)
            {
                return memberListBinding.ToSerializable();
            }
            else if (memberBinding is MemberMemberBinding memberMemberBinding)
            {
                return memberMemberBinding.ToSerializable();
            }
            else
            {
                throw new NotSupportedException(Invariant($"Type of {nameof(MemberBinding)} '{memberBinding.GetType()}' is not supported."));
            }
        }

        /// <summary>From the serializable.</summary>
        /// <param name="memberBinding">The memberBindings.</param>
        /// <returns>Converted version.</returns>
        public static MemberBinding FromSerializable(this SerializableMemberBindingBase memberBinding)
        {
            if (memberBinding is SerializableMemberAssignment memberAssignment)
            {
                return memberAssignment.FromSerializable();
            }
            else if (memberBinding is SerializableMemberListBinding memberListBinding)
            {
                return memberListBinding.FromSerializable();
            }
            else if (memberBinding is SerializableMemberMemberBinding memberMemberBinding)
            {
                return memberMemberBinding.FromSerializable();
            }
            else
            {
                throw new NotSupportedException(Invariant($"Type of {nameof(SerializableMemberBindingBase)} '{memberBinding.GetType()}' is not supported."));
            }
        }

        /// <summary>Converts to serializable.</summary>
        /// <param name="memberBindings">The memberBindings.</param>
        /// <returns>Serializable version.</returns>
        public static IReadOnlyCollection<SerializableMemberBindingBase> ToSerializable(this IReadOnlyCollection<MemberBinding> memberBindings)
        {
            var result = memberBindings.Select(_ => _.ToSerializable()).ToList();
            return result;
        }

        /// <summary>From the serializable.</summary>
        /// <param name="memberBindings">The memberBindings.</param>
        /// <returns>Converted version.</returns>
        public static IReadOnlyCollection<MemberBinding> FromSerializable(this IReadOnlyCollection<SerializableMemberBindingBase> memberBindings)
        {
            var result = memberBindings.Select(_ => _.FromSerializable()).ToList();
            return result;
        }
    }
}
