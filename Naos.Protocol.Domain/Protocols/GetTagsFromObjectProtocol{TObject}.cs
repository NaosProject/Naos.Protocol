// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GetTagsFromObjectProtocol{TObject}.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Protocol.Domain
{
    using System.Collections.Generic;

    /// <summary>
    /// Event container.
    /// </summary>
    /// <typeparam name="TObject">Type of object to inspect.</typeparam>
    public class GetTagsFromObjectProtocol<TObject> : IReturningProtocol<GetTagsFromObjectOp<TObject>, IReadOnlyDictionary<string, string>>
    {
        /// <inheritdoc />
        public IReadOnlyDictionary<string, string> Execute(
            GetTagsFromObjectOp<TObject> operation)
        {
            if (operation.ObjectToDetermineKeyFrom is IHaveKey<TObject> haveKeyObject)
            {
                return new Dictionary<string, string>
                       {
                           { haveKeyObject.Id.ToString(), "Value" },
                       };
            }

            return new Dictionary<string, string>();
        }
    }
}
