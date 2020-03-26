// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EventCrudOperationHandlers.Get.cs" company="Naos Project">
//     Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Protocol.SqlServer
{
    using System;
    using System.Threading.Tasks;
    using Naos.Protocol.Domain;

#pragma warning disable CS1710 // XML comment has a duplicate typeparam tag
    /// <summary>
    /// SQL protocol for <see cref="GetLatestOp{TObject}" />.
    /// </summary>
    /// <typeparam name="TKey">The type of the key.</typeparam>
    /// <typeparam name="TObject">The type of payload.</typeparam>
    public partial class CrudOperationHandlers<TKey, TObject> : IReturningProtocol<GetLatestOp<TObject>, TObject>
        where TObject : class
    {
        /// <inheritdoc />
        TObject IReturningProtocol<GetLatestOp<TObject>, TObject>.Execute(GetLatestOp<TObject> operation)
        {
            if (operation == null)
            {
                throw new ArgumentNullException(nameof(operation));
            }

            /*
            var locatorOp = new DetermineLocatorByKeyOp<TKey, LocatorBase>(operation.Key);
            var locator = this.keyLocatorProtocol.Execute();
            */

            throw new System.NotImplementedException();
        }
    }
}
