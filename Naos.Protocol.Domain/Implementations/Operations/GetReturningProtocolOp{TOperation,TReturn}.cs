// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GetReturningProtocolOp{TOperation,TReturn}.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Protocol.Domain
{
    /// <summary>
    /// Operation to get a returning protocol by type of operation.
    /// </summary>
    /// <typeparam name="TOperation">Type of operation.</typeparam>
    /// <typeparam name="TReturn">The type of the return.</typeparam>
    public partial class GetReturningProtocolOp<TOperation, TReturn> : ReturningOperationBase<IReturningProtocol<TOperation, TReturn>>
        where TOperation : ReturningOperationBase<TReturn>
    {
    }
}
