// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IHandleOperations.cs" company="Naos Project">
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
    public interface IHandleOperations<in TOperation, TReturn>
        where TOperation : OperationBase
    {
        /// <summary>
        /// Run the operation and returns as appropriate.
        /// </summary>
        /// <param name="operation">Operation to run.</param>
        /// <returns>Appropriate return of operation.</returns>
        Task<TReturn> HandleAsync(TOperation operation);

        /// <summary>
        /// Run the operation and returns as appropriate.
        /// </summary>
        /// <param name="operation">Operation to run.</param>
        /// <returns>Appropriate return of operation.</returns>
        TReturn Handle(TOperation operation);

        /// <summary>
        /// Run the operation and returns as appropriate in the specific type.
        /// </summary>
        /// <typeparam name="TSpecificReturn">Type of return (overriding TReturn).</typeparam>
        /// <param name="operation">Operation to run.</param>
        /// <returns>Appropriate return of operation.</returns>
        TSpecificReturn HandleWithSpecificReturn<TSpecificReturn>(TOperation operation);
    }

    /*
    public class ConsolidateOperationHandler<T1Operation, T1Return> : IHandleOperations<T1Operation, T1Return>
    where T1Operation : OperationBase
    {
        private IHandleOperations<T1Operation, T1Return> handler1;

        public ConsolidateOperationHandler(IHandleOperations<T1Operation, T1Return> handler1)
        {
            this.handler1 = handler1;
        }

        public async Task<T1Return> HandleAsync(T1Operation operation)
        {
            return await this.handler1.HandleAsync(operation);
        }
    }

    public class ConsolidateOperationHandler<T1Operation, T1Return, T2Operation, T2Return>
        : IHandleOperations<T1Operation, T1Return>, IHandleOperations<T2Operation, T2Return>
        where T1Operation : OperationBase
        where T2Operation : OperationBase
    {
        private IHandleOperations<T1Operation, T1Return> handler1;
        private IHandleOperations<T2Operation, T2Return> handler2;

        public ConsolidateOperationHandler(IHandleOperations<T1Operation, T1Return> handler1, IHandleOperations<T2Operation, T2Return> handler2)
        {
            this.handler1 = handler1;
            this.handler2 = handler2;
        }

        public async Task<T1Return> HandleAsync(T1Operation operation)
        {
            return await this.handler1.HandleAsync(operation);
        }

        public async Task<T2Return> HandleAsync(T2Operation operation)
        {
            return await this.handler2.HandleAsync(operation);
        }
    }
    */
}
