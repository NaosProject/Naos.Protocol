// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LambdaReturningProtocol{TOperation,TReturn}.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Protocol.Domain
{
    using System;
    using System.Threading.Tasks;
    using Naos.Recipes.RunWithRetry;
    using OBeautifulCode.Assertion.Recipes;

    /// <summary>
    /// Event container.
    /// </summary>
    /// <typeparam name="TOperation">Type of operation.</typeparam>
    /// <typeparam name="TReturn">Type of the return.</typeparam>
    public class LambdaReturningProtocol<TOperation, TReturn> : ISyncAndAsyncReturningProtocol<TOperation, TReturn>
    where TOperation : IReturningOperation<TReturn>
    {
        private readonly Func<TOperation, TReturn> synchronousLambda;

        private readonly Func<TOperation, Task<TReturn>> asyncAsynchronousLambda;

        /// <summary>
        /// Initializes a new instance of the <see cref="LambdaReturningProtocol{TOperation,TReturn}"/> class.
        /// </summary>
        /// <param name="asynchronousLambda">The lambda to protocol the operation.</param>
        public LambdaReturningProtocol(
            Func<TOperation, Task<TReturn>> asynchronousLambda)
        {
            asynchronousLambda.MustForArg(nameof(asynchronousLambda)).NotBeNull();
            this.asyncAsynchronousLambda = asynchronousLambda;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LambdaReturningProtocol{TOperation,TReturn}"/> class.
        /// </summary>
        /// <param name="synchronousLambda">The lambda to protocol the operation.</param>
        public LambdaReturningProtocol(
            Func<TOperation, TReturn> synchronousLambda)
        {
            synchronousLambda.MustForArg(nameof(synchronousLambda)).NotBeNull();
            this.synchronousLambda = synchronousLambda;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LambdaReturningProtocol{TOperation,TReturn}"/> class.
        /// </summary>
        /// <param name="synchronousLambda">The synchronous lambda to protocol the operation.</param>
        /// <param name="asynchronousLambda">The asynchronous lambda to protocol the operation.</param>
        public LambdaReturningProtocol(
            Func<TOperation, TReturn> synchronousLambda,
            Func<TOperation, Task<TReturn>> asynchronousLambda)
        {
            synchronousLambda.MustForArg(nameof(synchronousLambda)).NotBeNull();
            asynchronousLambda.MustForArg(nameof(asynchronousLambda)).NotBeNull();

            this.synchronousLambda = synchronousLambda;
            this.asyncAsynchronousLambda = asynchronousLambda;
        }

        /// <inheritdoc />
        public TReturn Execute(
            TOperation operation)
        {
            return this.synchronousLambda != null
                ? this.synchronousLambda(operation)
                : Run.TaskUntilCompletion(this.asyncAsynchronousLambda(operation));
        }

        /// <inheritdoc />
        public async Task<TReturn> ExecuteAsync(
            TOperation operation)
        {
            return await (this.asyncAsynchronousLambda != null
                ? this.asyncAsynchronousLambda(operation)
                : Task.FromResult(this.synchronousLambda(operation)));
        }
    }
}
