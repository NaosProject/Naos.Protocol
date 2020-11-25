﻿// --------------------------------------------------------------------------------------------------------------------
// <auto-generated>
//   Generated using OBeautifulCode.CodeGen.ModelObject (1.0.137.0)
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
    public partial class ExecuteOperationRequestedEvent<TId, TOperation> : IModel<ExecuteOperationRequestedEvent<TId, TOperation>>
    {
        /// <summary>
        /// Determines whether two objects of type <see cref="ExecuteOperationRequestedEvent{TId, TOperation}"/> are equal.
        /// </summary>
        /// <param name="left">The object to the left of the equality operator.</param>
        /// <param name="right">The object to the right of the equality operator.</param>
        /// <returns>true if the two items are equal; otherwise false.</returns>
        public static bool operator ==(ExecuteOperationRequestedEvent<TId, TOperation> left, ExecuteOperationRequestedEvent<TId, TOperation> right)
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
        /// Determines whether two objects of type <see cref="ExecuteOperationRequestedEvent{TId, TOperation}"/> are not equal.
        /// </summary>
        /// <param name="left">The object to the left of the equality operator.</param>
        /// <param name="right">The object to the right of the equality operator.</param>
        /// <returns>true if the two items are not equal; otherwise false.</returns>
        public static bool operator !=(ExecuteOperationRequestedEvent<TId, TOperation> left, ExecuteOperationRequestedEvent<TId, TOperation> right) => !(left == right);

        /// <inheritdoc />
        public bool Equals(ExecuteOperationRequestedEvent<TId, TOperation> other)
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
                      && this.TimestampUtc.IsEqualTo(other.TimestampUtc)
                      && this.OperationToExecute.IsEqualTo(other.OperationToExecute)
                      && this.Tags.IsEqualTo(other.Tags);

            return result;
        }

        /// <inheritdoc />
        public override bool Equals(object obj) => this == (obj as ExecuteOperationRequestedEvent<TId, TOperation>);

        /// <inheritdoc />
        public override int GetHashCode() => HashCodeHelper.Initialize()
            .Hash(this.Id)
            .Hash(this.TimestampUtc)
            .Hash(this.OperationToExecute)
            .Hash(this.Tags)
            .Value;

        /// <inheritdoc />
        public new ExecuteOperationRequestedEvent<TId, TOperation> DeepClone() => (ExecuteOperationRequestedEvent<TId, TOperation>)this.DeepCloneInternal();

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
            var result = new ExecuteOperationRequestedEvent<TId, TOperation>(
                                 id,
                                 this.TimestampUtc,
                                 DeepCloneGeneric(this.OperationToExecute),
                                 this.Tags?.ToDictionary(k => k.Key?.DeepClone(), v => v.Value?.DeepClone()));

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
            var result = new ExecuteOperationRequestedEvent<TId, TOperation>(
                                 DeepCloneGeneric(this.Id),
                                 timestampUtc,
                                 DeepCloneGeneric(this.OperationToExecute),
                                 this.Tags?.ToDictionary(k => k.Key?.DeepClone(), v => v.Value?.DeepClone()));

            return result;
        }

        /// <summary>
        /// Deep clones this object with a new <see cref="OperationToExecute" />.
        /// </summary>
        /// <param name="operationToExecute">The new <see cref="OperationToExecute" />.  This object will NOT be deep cloned; it is used as-is.</param>
        /// <returns>New <see cref="ExecuteOperationRequestedEvent{TId, TOperation}" /> using the specified <paramref name="operationToExecute" /> for <see cref="OperationToExecute" /> and a deep clone of every other property.</returns>
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
        public ExecuteOperationRequestedEvent<TId, TOperation> DeepCloneWithOperationToExecute(TOperation operationToExecute)
        {
            var result = new ExecuteOperationRequestedEvent<TId, TOperation>(
                                 DeepCloneGeneric(this.Id),
                                 this.TimestampUtc,
                                 operationToExecute,
                                 this.Tags?.ToDictionary(k => k.Key?.DeepClone(), v => v.Value?.DeepClone()));

            return result;
        }

        /// <summary>
        /// Deep clones this object with a new <see cref="Tags" />.
        /// </summary>
        /// <param name="tags">The new <see cref="Tags" />.  This object will NOT be deep cloned; it is used as-is.</param>
        /// <returns>New <see cref="ExecuteOperationRequestedEvent{TId, TOperation}" /> using the specified <paramref name="tags" /> for <see cref="Tags" /> and a deep clone of every other property.</returns>
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
        public ExecuteOperationRequestedEvent<TId, TOperation> DeepCloneWithTags(IReadOnlyDictionary<string, string> tags)
        {
            var result = new ExecuteOperationRequestedEvent<TId, TOperation>(
                                 DeepCloneGeneric(this.Id),
                                 this.TimestampUtc,
                                 DeepCloneGeneric(this.OperationToExecute),
                                 tags);

            return result;
        }

        /// <inheritdoc />
        protected override EventBaseBase DeepCloneInternal()
        {
            var result = new ExecuteOperationRequestedEvent<TId, TOperation>(
                                 DeepCloneGeneric(this.Id),
                                 this.TimestampUtc,
                                 DeepCloneGeneric(this.OperationToExecute),
                                 this.Tags?.ToDictionary(k => k.Key?.DeepClone(), v => v.Value?.DeepClone()));

            return result;
        }

        private static TId DeepCloneGeneric(TId value)
        {
            object result;

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
                    result = valueAsString.DeepClone();
                }
                else if (value is global::System.Version valueAsVersion)
                {
                    result = valueAsVersion.DeepClone();
                }
                else if (value is global::System.Uri valueAsUri)
                {
                    result = valueAsUri.DeepClone();
                }
                else
                {
                    throw new NotSupportedException(Invariant($"I do not know how to deep clone an object of type '{type.ToStringReadable()}'"));
                }
            }

            return (TId)result;
        }

        private static TOperation DeepCloneGeneric(TOperation value)
        {
            object result;

            var type = typeof(TOperation);

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
                else if (value is IDeepCloneable<TOperation> deepCloneableValue)
                {
                    result = deepCloneableValue.DeepClone();
                }
                else if (value is string valueAsString)
                {
                    result = valueAsString.DeepClone();
                }
                else if (value is global::System.Version valueAsVersion)
                {
                    result = valueAsVersion.DeepClone();
                }
                else if (value is global::System.Uri valueAsUri)
                {
                    result = valueAsUri.DeepClone();
                }
                else
                {
                    throw new NotSupportedException(Invariant($"I do not know how to deep clone an object of type '{type.ToStringReadable()}'"));
                }
            }

            return (TOperation)result;
        }

        /// <inheritdoc />
        [SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        public override string ToString()
        {
            var result = Invariant($"Naos.Protocol.Domain.{this.GetType().ToStringReadable()}: Id = {this.Id?.ToString() ?? "<null>"}, TimestampUtc = {this.TimestampUtc.ToString(CultureInfo.InvariantCulture) ?? "<null>"}, OperationToExecute = {this.OperationToExecute?.ToString() ?? "<null>"}, Tags = {this.Tags?.ToString() ?? "<null>"}.");

            return result;
        }
    }
}