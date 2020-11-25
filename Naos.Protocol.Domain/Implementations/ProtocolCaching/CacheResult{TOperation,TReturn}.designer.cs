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
    public partial class CacheResult<TOperation, TReturn> : IModel<CacheResult<TOperation, TReturn>>
    {
        /// <summary>
        /// Determines whether two objects of type <see cref="CacheResult{TOperation, TReturn}"/> are equal.
        /// </summary>
        /// <param name="left">The object to the left of the equality operator.</param>
        /// <param name="right">The object to the right of the equality operator.</param>
        /// <returns>true if the two items are equal; otherwise false.</returns>
        public static bool operator ==(CacheResult<TOperation, TReturn> left, CacheResult<TOperation, TReturn> right)
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
        /// Determines whether two objects of type <see cref="CacheResult{TOperation, TReturn}"/> are not equal.
        /// </summary>
        /// <param name="left">The object to the left of the equality operator.</param>
        /// <param name="right">The object to the right of the equality operator.</param>
        /// <returns>true if the two items are not equal; otherwise false.</returns>
        public static bool operator !=(CacheResult<TOperation, TReturn> left, CacheResult<TOperation, TReturn> right) => !(left == right);

        /// <inheritdoc />
        public bool Equals(CacheResult<TOperation, TReturn> other)
        {
            if (ReferenceEquals(this, other))
            {
                return true;
            }

            if (ReferenceEquals(other, null))
            {
                return false;
            }

            var result = this.Operation.IsEqualTo(other.Operation)
                      && this.CachedObject.IsEqualTo(other.CachedObject)
                      && this.FreshnessInUtc.IsEqualTo(other.FreshnessInUtc);

            return result;
        }

        /// <inheritdoc />
        public override bool Equals(object obj) => this == (obj as CacheResult<TOperation, TReturn>);

        /// <inheritdoc />
        public override int GetHashCode() => HashCodeHelper.Initialize()
            .Hash(this.Operation)
            .Hash(this.CachedObject)
            .Hash(this.FreshnessInUtc)
            .Value;

        /// <inheritdoc />
        public object Clone() => this.DeepClone();

        /// <inheritdoc />
        public CacheResult<TOperation, TReturn> DeepClone()
        {
            var result = new CacheResult<TOperation, TReturn>(
                                 DeepCloneGeneric(this.Operation),
                                 DeepCloneGeneric(this.CachedObject),
                                 this.FreshnessInUtc);

            return result;
        }

        /// <summary>
        /// Deep clones this object with a new <see cref="Operation" />.
        /// </summary>
        /// <param name="operation">The new <see cref="Operation" />.  This object will NOT be deep cloned; it is used as-is.</param>
        /// <returns>New <see cref="CacheResult{TOperation, TReturn}" /> using the specified <paramref name="operation" /> for <see cref="Operation" /> and a deep clone of every other property.</returns>
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
        public CacheResult<TOperation, TReturn> DeepCloneWithOperation(TOperation operation)
        {
            var result = new CacheResult<TOperation, TReturn>(
                                 operation,
                                 DeepCloneGeneric(this.CachedObject),
                                 this.FreshnessInUtc);

            return result;
        }

        /// <summary>
        /// Deep clones this object with a new <see cref="CachedObject" />.
        /// </summary>
        /// <param name="cachedObject">The new <see cref="CachedObject" />.  This object will NOT be deep cloned; it is used as-is.</param>
        /// <returns>New <see cref="CacheResult{TOperation, TReturn}" /> using the specified <paramref name="cachedObject" /> for <see cref="CachedObject" /> and a deep clone of every other property.</returns>
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
        public CacheResult<TOperation, TReturn> DeepCloneWithCachedObject(TReturn cachedObject)
        {
            var result = new CacheResult<TOperation, TReturn>(
                                 DeepCloneGeneric(this.Operation),
                                 cachedObject,
                                 this.FreshnessInUtc);

            return result;
        }

        /// <summary>
        /// Deep clones this object with a new <see cref="FreshnessInUtc" />.
        /// </summary>
        /// <param name="freshnessInUtc">The new <see cref="FreshnessInUtc" />.  This object will NOT be deep cloned; it is used as-is.</param>
        /// <returns>New <see cref="CacheResult{TOperation, TReturn}" /> using the specified <paramref name="freshnessInUtc" /> for <see cref="FreshnessInUtc" /> and a deep clone of every other property.</returns>
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
        public CacheResult<TOperation, TReturn> DeepCloneWithFreshnessInUtc(DateTime freshnessInUtc)
        {
            var result = new CacheResult<TOperation, TReturn>(
                                 DeepCloneGeneric(this.Operation),
                                 DeepCloneGeneric(this.CachedObject),
                                 freshnessInUtc);

            return result;
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

        private static TReturn DeepCloneGeneric(TReturn value)
        {
            object result;

            var type = typeof(TReturn);

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
                else if (value is IDeepCloneable<TReturn> deepCloneableValue)
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

            return (TReturn)result;
        }

        /// <inheritdoc />
        [SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        public override string ToString()
        {
            var result = Invariant($"Naos.Protocol.Domain.{this.GetType().ToStringReadable()}: Operation = {this.Operation?.ToString() ?? "<null>"}, CachedObject = {this.CachedObject?.ToString() ?? "<null>"}, FreshnessInUtc = {this.FreshnessInUtc.ToString(CultureInfo.InvariantCulture) ?? "<null>"}.");

            return result;
        }
    }
}