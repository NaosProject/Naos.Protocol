// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GetIdFromObjectProtocol{TId,TObject}.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Protocol.Domain
{
    using System;
    using OBeautifulCode.Type.Recipes;

    /// <summary>
    /// Protocol for <see cref="GetIdFromObjectOp{TId,TObject}"/> that checks common interfaces to use.
    /// </summary>
    /// <typeparam name="TId">The type of the ID.</typeparam>
    /// <typeparam name="TObject">The type of the object.</typeparam>
    public class GetIdFromObjectProtocol<TId, TObject> : IReturningProtocol<GetIdFromObjectOp<TId, TObject>, TId>
    {
        /// <inheritdoc />
        public TId Execute(
            GetIdFromObjectOp<TId, TObject> operation)
        {
            if (operation.ObjectToDetermineIdFrom is IHaveKey<TId> haveKeyObject)
            {
                return haveKeyObject.Id;
            }

            throw new ArgumentException(FormattableString.Invariant($"Cannot extract an Id from type {typeof(TObject).ToStringReadable()}"));
        }
    }
}
