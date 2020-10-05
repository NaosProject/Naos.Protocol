﻿// --------------------------------------------------------------------------------------------------------------------
// <auto-generated>
//   Generated using OBeautifulCode.CodeGen.ModelObject (1.0.116.0)
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
    public partial class GetIdFromObjectOp<TId, TObject> : IModel<GetIdFromObjectOp<TId, TObject>>
    {
        /// <summary>
        /// Determines whether two objects of type <see cref="GetIdFromObjectOp{TId, TObject}"/> are equal.
        /// </summary>
        /// <param name="left">The object to the left of the equality operator.</param>
        /// <param name="right">The object to the right of the equality operator.</param>
        /// <returns>true if the two items are equal; otherwise false.</returns>
        public static bool operator ==(GetIdFromObjectOp<TId, TObject> left, GetIdFromObjectOp<TId, TObject> right)
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
        /// Determines whether two objects of type <see cref="GetIdFromObjectOp{TId, TObject}"/> are not equal.
        /// </summary>
        /// <param name="left">The object to the left of the equality operator.</param>
        /// <param name="right">The object to the right of the equality operator.</param>
        /// <returns>true if the two items are not equal; otherwise false.</returns>
        public static bool operator !=(GetIdFromObjectOp<TId, TObject> left, GetIdFromObjectOp<TId, TObject> right) => !(left == right);

        /// <inheritdoc />
        public bool Equals(GetIdFromObjectOp<TId, TObject> other)
        {
            if (ReferenceEquals(this, other))
            {
                return true;
            }

            if (ReferenceEquals(other, null))
            {
                return false;
            }

            var result = this.ObjectToDetermineIdFrom.IsEqualTo(other.ObjectToDetermineIdFrom);

            return result;
        }

        /// <inheritdoc />
        public override bool Equals(object obj) => this == (obj as GetIdFromObjectOp<TId, TObject>);

        /// <inheritdoc />
        public override int GetHashCode() => HashCodeHelper.Initialize()
            .Hash(this.ObjectToDetermineIdFrom)
            .Value;

        /// <inheritdoc />
        public new GetIdFromObjectOp<TId, TObject> DeepClone() => (GetIdFromObjectOp<TId, TObject>)this.DeepCloneInternal();

        /// <summary>
        /// Deep clones this object with a new <see cref="ObjectToDetermineIdFrom" />.
        /// </summary>
        /// <param name="objectToDetermineIdFrom">The new <see cref="ObjectToDetermineIdFrom" />.  This object will NOT be deep cloned; it is used as-is.</param>
        /// <returns>New <see cref="GetIdFromObjectOp{TId, TObject}" /> using the specified <paramref name="objectToDetermineIdFrom" /> for <see cref="ObjectToDetermineIdFrom" /> and a deep clone of every other property.</returns>
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
        public GetIdFromObjectOp<TId, TObject> DeepCloneWithObjectToDetermineIdFrom(TObject objectToDetermineIdFrom)
        {
            var result = new GetIdFromObjectOp<TId, TObject>(
                                 objectToDetermineIdFrom);

            return result;
        }

        /// <inheritdoc />
        protected override OperationBase DeepCloneInternal()
        {
            var result = new GetIdFromObjectOp<TId, TObject>(
                                 DeepCloneGeneric(this.ObjectToDetermineIdFrom));

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

        private static TObject DeepCloneGeneric(TObject value)
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
                else if (value is global::System.Version valueAsVersion)
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
            var result = Invariant($"Naos.Protocol.Domain.{this.GetType().ToStringReadable()}: ObjectToDetermineIdFrom = {this.ObjectToDetermineIdFrom?.ToString() ?? "<null>"}.");

            return result;
        }
    }
}