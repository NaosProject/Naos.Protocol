// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CrudOperationHandlers.Get.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Protocol.SqlServer
{
    using System.Threading.Tasks;
    using Naos.Protocol.Domain;

#pragma warning disable CS1710 // XML comment has a duplicate typeparam tag
    /// <summary>
    /// TODO: Starting point for new project.
    /// </summary>
    /// <typeparam name="TObject">The type of payload.</typeparam>
    public partial class CrudOperationHandlers<TObject> : IProtocol<GetLatestOp<TObject>>
#pragma warning restore CS1710 // XML comment has a duplicate typeparam tag
        where TObject : class
    {
        /// <inheritdoc />
        public void Execute(
            GetLatestOp<TObject> operation)
        {
            throw new System.NotImplementedException();
        }

        /// <inheritdoc />
        public void Execute(
            PutOp<TObject> operation)
        {
            throw new System.NotImplementedException();
        }
    }
}
