// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IRequireProtocol.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Protocol.Domain
{
    public interface IRequireProtocolNoReturn<TOperation> : IGetVoidProtocol<TOperation>
        where TOperation : VoidOperationBase
    {
    }

    public interface IRequireProtocolWithReturn<TOperation, TReturn> : IGetReturningProtocol<TOperation, TReturn>
        where TOperation : ReturningOperationBase<TReturn>
    {
    }
}