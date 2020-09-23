// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GetVoidProtocolOp{TOperation}.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Protocol.Domain
{
    /// <summary>
    /// Operation to get a void protocol by type of operation.
    /// </summary>
    /// <typeparam name="TOperation">The type of the operation.</typeparam>
    public partial class GetVoidProtocolOp<TOperation> : ReturningOperationBase<IVoidProtocol<TOperation>>
        where TOperation : IVoidOperation
    {
    }
}
