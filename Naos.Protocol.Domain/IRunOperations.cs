// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IRunOperations.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Protocol.Domain
{
    using System.Threading.Tasks;

    /// <summary>
    /// Abstract base of an operation.
    /// </summary>
    /// <typeparam name="TOperation">Type of the operation it runs.</typeparam>
    /// <typeparam name="TReturn">Type of return.</typeparam>
    public interface IRunOperations<in TOperation, TReturn>
        where TOperation : OperationBase
    {
        /// <summary>
        /// Run the operation and returns as appropriate.
        /// </summary>
        /// <param name="operation">Operation to run.</param>
        /// <returns>Appropriate return of operation.</returns>
        Task<TReturn> RunAsync(TOperation operation);
    }
}
