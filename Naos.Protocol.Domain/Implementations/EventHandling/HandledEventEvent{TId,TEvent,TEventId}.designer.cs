﻿// --------------------------------------------------------------------------------------------------------------------
// <auto-generated>
//   Generated using OBeautifulCode.CodeGen.ModelObject (1.0.113.0)
// </auto-generated>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Protocol.Domain
{
    using global::System;
    using global::System.CodeDom.Compiler;
    using global::System.Collections.Concurrent;
    using global::System.Collections.Generic;
    using global::System.Collections.ObjectModel;
    using global::System.Diagnostics.CodeAnalysis;
    using global::System.Globalization;
    using global::System.Linq;

    using global::OBeautifulCode.Equality.Recipes;
    using global::OBeautifulCode.Type;
    using global::OBeautifulCode.Type.Recipes;

    using static global::System.FormattableString;

    [Serializable]
    public partial class HandledEventEvent<TId, TEvent, TEventId> : IModel<HandledEventEvent<TId, TEvent, TEventId>>
    {
        /// <summary>
        /// Determines whether two objects of type <see cref="HandledEventEvent{TId, TEvent, TEventId}"/> are equal.
        /// </summary>
        /// <param name="left">The object to the left of the equality operator.</param>
        /// <param name="right">The object to the right of the equality operator.</param>
        /// <returns>true if the two items are equal; otherwise false.</returns>
        public static bool operator ==(HandledEventEvent<TId, TEvent, TEventId> left, HandledEventEvent<TId, TEvent, TEventId> right)
        {
            if (ReferenceEquals(left, right))
            {
                return true;
            }

            if (ReferenceEquals(left, null) || ReferenceEquals(right, null))
            {
                return false;
            }

            var result = left.Equals(right);

            return result;
        }

        /// <summary>
        /// Determines whether two objects of type <see cref="HandledEventEvent{TId, TEvent, TEventId}"/> are not equal.
        /// </summary>
        /// <param name="left">The object to the left of the equality operator.</param>
        /// <param name="right">The object to the right of the equality operator.</param>
        /// <returns>true if the two items are not equal; otherwise false.</returns>
        public static bool operator !=(HandledEventEvent<TId, TEvent, TEventId> left, HandledEventEvent<TId, TEvent, TEventId> right) => !(left == right);

        /// <inheritdoc />
        public bool Equals(HandledEventEvent<TId, TEvent, TEventId> other)
        {
            if (ReferenceEquals(this, other))
            {
                return true;
            }

            if (ReferenceEquals(other, null))
            {
                return false;
            }

            var result = this.Id.IsEqualTo(other.Id)
                      && this.Tags.IsEqualTo(other.Tags)
                      && this.TimestampUtc.IsEqualTo(other.TimestampUtc)
                      && this.HandledEvent.IsEqualTo(other.HandledEvent);

            return result;
        }

        /// <inheritdoc />
        public override bool Equals(object obj) => this == (obj as HandledEventEvent<TId, TEvent, TEventId>);

        /// <inheritdoc />
        public override int GetHashCode() => HashCodeHelper.Initialize()
            .Hash(this.Id)
            .Hash(this.Tags)
            .Hash(this.TimestampUtc)
            .Hash(this.HandledEvent)
            .Value;

        /// <inheritdoc />
        public new HandledEventEvent<TId, TEvent, TEventId> DeepClone() => (HandledEventEvent<TId, TEvent, TEventId>)this.DeepCloneInternal();

        /// <inheritdoc />
        [SuppressMessage("Microsoft.Design", "CA1002: DoNotExposeGenericLists")]
        [SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly")]
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        [SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix")]
        [SuppressMessage("Microsoft.Naming", "CA1711:IdentifiersShouldNotHaveIncorrectSuffix")]
        [SuppressMessage("Microsoft.Naming", "CA1715:IdentifiersShouldHaveCorrectPrefix")]
        [SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords")]
        [SuppressMessage("Microsoft.Naming", "CA1719:ParameterNamesShouldNotMatchMemberNames")]
        [SuppressMessage("Microsoft.Naming", "CA1720:IdentifiersShouldNotContainTypeNames")]
        [SuppressMessage("Microsoft.Naming", "CA1722:IdentifiersShouldNotHaveIncorrectPrefix")]
        [SuppressMessage("Microsoft.Naming", "CA1725:ParameterNamesShouldMatchBaseDeclaration")]
        [SuppressMessage("Microsoft.Naming", "CA1726:UsePreferredTerms")]
        [SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly")]
        [SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
        public override EventBase<TId> DeepCloneWithId(TId id)
        {
            var result = new HandledEventEvent<TId, TEvent, TEventId>(
                                 id,
                                 this.TimestampUtc,
                                 DeepCloneGeneric(this.HandledEvent),
                                 this.Tags?.ToDictionary(k => k.Key?.Clone().ToString(), v => v.Value?.Clone().ToString()));

            return result;
        }

        /// <inheritdoc />
        [SuppressMessage("Microsoft.Design", "CA1002: DoNotExposeGenericLists")]
        [SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly")]
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        [SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix")]
        [SuppressMessage("Microsoft.Naming", "CA1711:IdentifiersShouldNotHaveIncorrectSuffix")]
        [SuppressMessage("Microsoft.Naming", "CA1715:IdentifiersShouldHaveCorrectPrefix")]
        [SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords")]
        [SuppressMessage("Microsoft.Naming", "CA1719:ParameterNamesShouldNotMatchMemberNames")]
        [SuppressMessage("Microsoft.Naming", "CA1720:IdentifiersShouldNotContainTypeNames")]
        [SuppressMessage("Microsoft.Naming", "CA1722:IdentifiersShouldNotHaveIncorrectPrefix")]
        [SuppressMessage("Microsoft.Naming", "CA1725:ParameterNamesShouldMatchBaseDeclaration")]
        [SuppressMessage("Microsoft.Naming", "CA1726:UsePreferredTerms")]
        [SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly")]
        [SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
        public override EventBase<TId> DeepCloneWithTags(IReadOnlyDictionary<string, string> tags)
        {
            var result = new HandledEventEvent<TId, TEvent, TEventId>(
                                 DeepCloneGeneric(this.Id),
                                 this.TimestampUtc,
                                 DeepCloneGeneric(this.HandledEvent),
                                 tags);

            return result;
        }

        /// <inheritdoc />
        [SuppressMessage("Microsoft.Design", "CA1002: DoNotExposeGenericLists")]
        [SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly")]
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        [SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix")]
        [SuppressMessage("Microsoft.Naming", "CA1711:IdentifiersShouldNotHaveIncorrectSuffix")]
        [SuppressMessage("Microsoft.Naming", "CA1715:IdentifiersShouldHaveCorrectPrefix")]
        [SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords")]
        [SuppressMessage("Microsoft.Naming", "CA1719:ParameterNamesShouldNotMatchMemberNames")]
        [SuppressMessage("Microsoft.Naming", "CA1720:IdentifiersShouldNotContainTypeNames")]
        [SuppressMessage("Microsoft.Naming", "CA1722:IdentifiersShouldNotHaveIncorrectPrefix")]
        [SuppressMessage("Microsoft.Naming", "CA1725:ParameterNamesShouldMatchBaseDeclaration")]
        [SuppressMessage("Microsoft.Naming", "CA1726:UsePreferredTerms")]
        [SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly")]
        [SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
        public override EventBase<TId> DeepCloneWithTimestampUtc(DateTime timestampUtc)
        {
            var result = new HandledEventEvent<TId, TEvent, TEventId>(
                                 DeepCloneGeneric(this.Id),
                                 timestampUtc,
                                 DeepCloneGeneric(this.HandledEvent),
                                 this.Tags?.ToDictionary(k => k.Key?.Clone().ToString(), v => v.Value?.Clone().ToString()));

            return result;
        }

        /// <summary>
        /// Deep clones this object with a new <see cref="HandledEvent" />.
        /// </summary>
        /// <param name="handledEvent">The new <see cref="HandledEvent" />.  This object will NOT be deep cloned; it is used as-is.</param>
        /// <returns>New <see cref="HandledEventEvent{TId, TEvent, TEventId}" /> using the specified <paramref name="handledEvent" /> for <see cref="HandledEvent" /> and a deep clone of every other property.</returns>
        [SuppressMessage("Microsoft.Design", "CA1002: DoNotExposeGenericLists")]
        [SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly")]
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        [SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix")]
        [SuppressMessage("Microsoft.Naming", "CA1711:IdentifiersShouldNotHaveIncorrectSuffix")]
        [SuppressMessage("Microsoft.Naming", "CA1715:IdentifiersShouldHaveCorrectPrefix")]
        [SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords")]
        [SuppressMessage("Microsoft.Naming", "CA1719:ParameterNamesShouldNotMatchMemberNames")]
        [SuppressMessage("Microsoft.Naming", "CA1720:IdentifiersShouldNotContainTypeNames")]
        [SuppressMessage("Microsoft.Naming", "CA1722:IdentifiersShouldNotHaveIncorrectPrefix")]
        [SuppressMessage("Microsoft.Naming", "CA1725:ParameterNamesShouldMatchBaseDeclaration")]
        [SuppressMessage("Microsoft.Naming", "CA1726:UsePreferredTerms")]
        [SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly")]
        [SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
        public HandledEventEvent<TId, TEvent, TEventId> DeepCloneWithHandledEvent(TEvent handledEvent)
        {
            var result = new HandledEventEvent<TId, TEvent, TEventId>(
                                 DeepCloneGeneric(this.Id),
                                 this.TimestampUtc,
                                 handledEvent,
                                 this.Tags?.ToDictionary(k => k.Key?.Clone().ToString(), v => v.Value?.Clone().ToString()));

            return result;
        }

        /// <inheritdoc />
        protected override EventBase<TId> DeepCloneInternal()
        {
            var result = new HandledEventEvent<TId, TEvent, TEventId>(
                                 DeepCloneGeneric(this.Id),
                                 this.TimestampUtc,
                                 DeepCloneGeneric(this.HandledEvent),
                                 this.Tags?.ToDictionary(k => k.Key?.Clone().ToString(), v => v.Value?.Clone().ToString()));

            return result;
        }

        private static TId DeepCloneGeneric(TId value)
        {
            TId result;

            var type = typeof(TId);

            if (type.IsValueType)
            {
                result = value;
            }
            else
            {
                if (ReferenceEquals(value, null))
                {
                    result = default;
                }
                else if (value is IDeepCloneable<TId> deepCloneableValue)
                {
                    result = deepCloneableValue.DeepClone();
                }
                else if (value is string valueAsString)
                {
                    result = (TId)(object)valueAsString.Clone().ToString();
                }
                else if (value is global::System.Version valueAsVersion)
                {
                    result = (TId)valueAsVersion.Clone();
                }
                else
                {
                    throw new NotSupportedException(Invariant($"I do not know how to deep clone an object of type '{type.ToStringReadable()}'"));
                }
            }

            return result;
        }

        private static TEvent DeepCloneGeneric(TEvent value)
        {
            TEvent result;

            var type = typeof(TEvent);

            if (type.IsValueType)
            {
                result = value;
            }
            else
            {
                if (ReferenceEquals(value, null))
                {
                    result = default;
                }
                else if (value is IDeepCloneable<TEvent> deepCloneableValue)
                {
                    result = deepCloneableValue.DeepClone();
                }
                else if (value is string valueAsString)
                {
                    result = (TEvent)(object)valueAsString.Clone().ToString();
                }
                else if (value is global::System.Version valueAsVersion)
                {
                    result = (TEvent)valueAsVersion.Clone();
                }
                else
                {
                    throw new NotSupportedException(Invariant($"I do not know how to deep clone an object of type '{type.ToStringReadable()}'"));
                }
            }

            return result;
        }

        private static TEventId DeepCloneGeneric(TEventId value)
        {
            TEventId result;

            var type = typeof(TEventId);

            if (type.IsValueType)
            {
                result = value;
            }
            else
            {
                if (ReferenceEquals(value, null))
                {
                    result = default;
                }
                else if (value is IDeepCloneable<TEventId> deepCloneableValue)
                {
                    result = deepCloneableValue.DeepClone();
                }
                else if (value is string valueAsString)
                {
                    result = (TEventId)(object)valueAsString.Clone().ToString();
                }
                else if (value is global::System.Version valueAsVersion)
                {
                    result = (TEventId)valueAsVersion.Clone();
                }
                else
                {
                    throw new NotSupportedException(Invariant($"I do not know how to deep clone an object of type '{type.ToStringReadable()}'"));
                }
            }

            return result;
        }

        /// <inheritdoc />
        [SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        public override string ToString()
        {
            var result = Invariant($"Naos.Protocol.Domain.{this.GetType().ToStringReadable()}: Id = {this.Id?.ToString() ?? "<null>"}, Tags = {this.Tags?.ToString() ?? "<null>"}, TimestampUtc = {this.TimestampUtc.ToString(CultureInfo.InvariantCulture) ?? "<null>"}, HandledEvent = {this.HandledEvent?.ToString() ?? "<null>"}.");

            return result;
        }
    }
}