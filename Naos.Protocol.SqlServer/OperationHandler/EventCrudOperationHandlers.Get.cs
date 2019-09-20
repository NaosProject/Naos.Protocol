// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EventCrudOperationHandlers.Get.cs" company="Naos Project">
//     Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Protocol.SqlServer
{
    using System.Threading.Tasks;
    using Naos.Protocol.Domain;

#pragma warning disable CS1710 // XML comment has a duplicate typeparam tag
    /// <summary>
    /// SQL protocol for <see cref="GetLatestOp{TObject}" />.
    /// </summary>
    /// <typeparam name="TObject">The type of payload.</typeparam>
    public partial class CrudOperationHandlers<TObject> : IReturningProtocol<GetLatestOp<TObject>, TObject>
        where TObject : class
    {
        /// <inheritdoc />
        TObject IReturningProtocol<GetLatestOp<TObject>, TObject>.Execute(GetLatestOp<TObject> operation)
        {
            throw new System.NotImplementedException();
        }
    }
}
