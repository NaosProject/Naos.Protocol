// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IProtocol.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Protocol.Domain
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <summary>
    /// Interface for protocol execution.
    /// </summary>
    /// <typeparam name="TOperation">Type of the operation it runs.</typeparam>
    public interface IProtocol<in TOperation>
        where TOperation : OperationBase
    {
        void Execute(
            TOperation operation);
    }

    /// <summary>
    /// Interface for protocol execution.
    /// </summary>
    /// <typeparam name="TOperation">Type of the operation it runs.</typeparam>
    /// <typeparam name="TReturn">Type of return.</typeparam>
    public interface IProtocol<in TOperation, TReturn> : IProtocol<TOperation>
        where TOperation : OperationBase<TReturn>
    {
        TReturn Execute<TReturn>(TOperation operation);
    }
}
