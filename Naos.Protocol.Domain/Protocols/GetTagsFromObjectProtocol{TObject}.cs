// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GetTagsFromObjectProtocol{TObject}.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Protocol.Domain
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

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
            if (operation.ObjectToDetermineTagsFrom is IHaveId<TObject> hasTagsObject)
            {
                return new Dictionary<string, string>
                       {
                           { hasTagsObject.Id.ToString(), "Value" },
                       };
            }

            return new Dictionary<string, string>();
        }

        /// <inheritdoc />
        public async Task<IReadOnlyDictionary<string, string>> ExecuteAsync(
            GetTagsFromObjectOp<TObject> operation)
        {
            return await Task.FromResult(this.Execute(operation));
        }
    }
}
