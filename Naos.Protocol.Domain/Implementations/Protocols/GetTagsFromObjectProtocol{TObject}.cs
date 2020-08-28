// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GetTagsFromObjectProtocol{TObject}.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Protocol.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using OBeautifulCode.Type;
    using OBeautifulCode.Type.Recipes;

    /// <summary>
    /// Event container.
    /// </summary>
    /// <typeparam name="TObject">Type of object to inspect.</typeparam>
    public class GetTagsFromObjectProtocol<TObject> : ISyncAndAsyncReturningProtocol<GetTagsFromObjectOp<TObject>, IReadOnlyDictionary<string, string>>
    {
        /// <inheritdoc />
        public IReadOnlyDictionary<string, string> Execute(
            GetTagsFromObjectOp<TObject> operation)
        {
            if (operation.ObjectToDetermineTagsFrom is IHaveTags hasTagsObject)
            {
                return hasTagsObject.Tags;
            }

            throw new ArgumentException(FormattableString.Invariant($"Cannot extract an Tags from type '{typeof(TObject).ToStringReadable()}' because it does not implement '{typeof(IHaveTags).ToStringReadable()}'."));
        }

        /// <inheritdoc />
        public async Task<IReadOnlyDictionary<string, string>> ExecuteAsync(
            GetTagsFromObjectOp<TObject> operation)
        {
            return await Task.FromResult(this.Execute(operation));
        }
    }
}
