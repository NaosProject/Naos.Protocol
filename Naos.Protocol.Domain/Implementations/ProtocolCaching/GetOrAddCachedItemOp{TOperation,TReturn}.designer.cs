﻿// --------------------------------------------------------------------------------------------------------------------
// <auto-generated>
//   Generated using OBeautifulCode.CodeGen.ModelObject (1.0.112.0)
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
    public partial class GetOrAddCachedItemOp<TOperation, TReturn> : IModel<GetOrAddCachedItemOp<TOperation, TReturn>>
    {
        /// <summary>
        /// Determines whether two objects of type <see cref="GetOrAddCachedItemOp{TOperation, TReturn}"/> are equal.
        /// </summary>
        /// <param name="left">The object to the left of the equality operator.</param>
        /// <param name="right">The object to the right of the equality operator.</param>
        /// <returns>true if the two items are equal; otherwise false.</returns>
        public static bool operator ==(GetOrAddCachedItemOp<TOperation, TReturn> left, GetOrAddCachedItemOp<TOperation, TReturn> right)
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
        /// Determines whether two objects of type <see cref="GetOrAddCachedItemOp{TOperation, TReturn}"/> are not equal.
        /// </summary>
        /// <param name="left">The object to the left of the equality operator.</param>
        /// <param name="right">The object to the right of the equality operator.</param>
        /// <returns>true if the two items are not equal; otherwise false.</returns>
        public static bool operator !=(GetOrAddCachedItemOp<TOperation, TReturn> left, GetOrAddCachedItemOp<TOperation, TReturn> right) => !(left == right);

        /// <inheritdoc />
        public bool Equals(GetOrAddCachedItemOp<TOperation, TReturn> other)
        {
            if (ReferenceEquals(this, other))
            {
                return true;
            }

            if (ReferenceEquals(other, null))
            {
                return false;
            }

            var result = this.Operation.IsEqualTo(other.Operation);

            return result;
        }

        /// <inheritdoc />
        public override bool Equals(object obj) => this == (obj as GetOrAddCachedItemOp<TOperation, TReturn>);

        /// <inheritdoc />
        public override int GetHashCode() => HashCodeHelper.Initialize()
            .Hash(this.Operation)
            .Value;

        /// <inheritdoc />
        public new GetOrAddCachedItemOp<TOperation, TReturn> DeepClone() => (GetOrAddCachedItemOp<TOperation, TReturn>)this.DeepCloneInternal();

        /// <summary>
        /// Deep clones this object with a new <see cref="Operation" />.
        /// </summary>
        /// <param name="operation">The new <see cref="Operation" />.  This object will NOT be deep cloned; it is used as-is.</param>
        /// <returns>New <see cref="GetOrAddCachedItemOp{TOperation, TReturn}" /> using the specified <paramref name="operation" /> for <see cref="Operation" /> and a deep clone of every other property.</returns>
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
        public GetOrAddCachedItemOp<TOperation, TReturn> DeepCloneWithOperation(TOperation operation)
        {
            var result = new GetOrAddCachedItemOp<TOperation, TReturn>(
                                 operation);

            return result;
        }

        /// <inheritdoc />
        protected override OperationBase DeepCloneInternal()
        {
            var result = new GetOrAddCachedItemOp<TOperation, TReturn>(
                                 DeepCloneGeneric(this.Operation));

            return result;
        }

        private static TOperation DeepCloneGeneric(TOperation value)
        {
            TOperation result;

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
                    result = (TOperation)(object)valueAsString.Clone().ToString();
                }
                else if (value is global::System.Version valueAsVersion)
                {
                    result = (TOperation)valueAsVersion.Clone();
                }
                else
                {
                    throw new NotSupportedException(Invariant($"I do not know how to deep clone an object of type '{type.ToStringReadable()}'"));
                }
            }

            return result;
        }

        private static TReturn DeepCloneGeneric(TReturn value)
        {
            TReturn result;

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
                    result = (TReturn)(object)valueAsString.Clone().ToString();
                }
                else if (value is global::System.Version valueAsVersion)
                {
                    result = (TReturn)valueAsVersion.Clone();
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
            var result = Invariant($"Naos.Protocol.Domain.{this.GetType().ToStringReadable()}: Operation = {this.Operation?.ToString() ?? "<null>"}.");

            return result;
        }
    }
}