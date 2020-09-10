﻿// --------------------------------------------------------------------------------------------------------------------
// <auto-generated>
//   Generated using OBeautifulCode.CodeGen.ModelObject (1.0.104.0)
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
    public partial class GetTagsFromObjectOp<TObject> : IModel<GetTagsFromObjectOp<TObject>>
    {
        /// <summary>
        /// Determines whether two objects of type <see cref="GetTagsFromObjectOp{TObject}"/> are equal.
        /// </summary>
        /// <param name="left">The object to the left of the equality operator.</param>
        /// <param name="right">The object to the right of the equality operator.</param>
        /// <returns>true if the two items are equal; otherwise false.</returns>
        public static bool operator ==(GetTagsFromObjectOp<TObject> left, GetTagsFromObjectOp<TObject> right)
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
        /// Determines whether two objects of type <see cref="GetTagsFromObjectOp{TObject}"/> are not equal.
        /// </summary>
        /// <param name="left">The object to the left of the equality operator.</param>
        /// <param name="right">The object to the right of the equality operator.</param>
        /// <returns>true if the two items are not equal; otherwise false.</returns>
        public static bool operator !=(GetTagsFromObjectOp<TObject> left, GetTagsFromObjectOp<TObject> right) => !(left == right);

        /// <inheritdoc />
        public bool Equals(GetTagsFromObjectOp<TObject> other)
        {
            if (ReferenceEquals(this, other))
            {
                return true;
            }

            if (ReferenceEquals(other, null))
            {
                return false;
            }

            var result = this.ObjectToDetermineTagsFrom.IsEqualTo(other.ObjectToDetermineTagsFrom);

            return result;
        }

        /// <inheritdoc />
        public override bool Equals(object obj) => this == (obj as GetTagsFromObjectOp<TObject>);

        /// <inheritdoc />
        public override int GetHashCode() => HashCodeHelper.Initialize()
            .Hash(this.ObjectToDetermineTagsFrom)
            .Value;

        /// <inheritdoc />
        public new GetTagsFromObjectOp<TObject> DeepClone() => (GetTagsFromObjectOp<TObject>)this.DeepCloneInternal();

        /// <summary>
        /// Deep clones this object with a new <see cref="ObjectToDetermineTagsFrom" />.
        /// </summary>
        /// <param name="objectToDetermineTagsFrom">The new <see cref="ObjectToDetermineTagsFrom" />.  This object will NOT be deep cloned; it is used as-is.</param>
        /// <returns>New <see cref="GetTagsFromObjectOp{TObject}" /> using the specified <paramref name="objectToDetermineTagsFrom" /> for <see cref="ObjectToDetermineTagsFrom" /> and a deep clone of every other property.</returns>
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
        public GetTagsFromObjectOp<TObject> DeepCloneWithObjectToDetermineTagsFrom(TObject objectToDetermineTagsFrom)
        {
            var result = new GetTagsFromObjectOp<TObject>(
                                 objectToDetermineTagsFrom);

            return result;
        }

        /// <inheritdoc />
        protected override ReturningOperationBase<IReadOnlyDictionary<string, string>> DeepCloneInternal()
        {
            var result = new GetTagsFromObjectOp<TObject>(
                                 DeepCloneGeneric(this.ObjectToDetermineTagsFrom));

            return result;
        }

        private TObject DeepCloneGeneric(TObject value)
        {
            TObject result;

            var type = typeof(TObject);

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
                else if (value is IDeepCloneable<TObject> deepCloneableValue)
                {
                    result = deepCloneableValue.DeepClone();
                }
                else if (value is string valueAsString)
                {
                    result = (TObject)(object)valueAsString.Clone().ToString();
                }
                else if (value is System.Version valueAsVersion)
                {
                    result = (TObject)valueAsVersion.Clone();
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
            var result = Invariant($"Naos.Protocol.Domain.{this.GetType().ToStringReadable()}: ObjectToDetermineTagsFrom = {this.ObjectToDetermineTagsFrom?.ToString() ?? "<null>"}.");

            return result;
        }
    }
}