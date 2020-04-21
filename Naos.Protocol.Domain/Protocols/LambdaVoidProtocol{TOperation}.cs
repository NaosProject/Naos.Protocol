// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LambdaVoidProtocol{TOperation}.cs" company="Naos Project">
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
    public class LambdaVoidProtocol<TOperation> : ISyncAndAsyncVoidProtocol<TOperation>
    where TOperation : VoidOperationBase
    {
        private readonly Action<TOperation> synchronousLambda;

        private readonly Func<TOperation, Task> asyncAsynchronousLambda;

        /// <summary>
        /// Initializes a new instance of the <see cref="LambdaVoidProtocol{TOperation}"/> class.
        /// </summary>
        /// <param name="asynchronousLambda">The lambda to protocol the operation.</param>
        public LambdaVoidProtocol(
            Func<TOperation, Task> asynchronousLambda)
        {
            asynchronousLambda.MustForArg(nameof(asynchronousLambda)).NotBeNull();
            this.asyncAsynchronousLambda = asynchronousLambda;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LambdaVoidProtocol{TOperation}"/> class.
        /// </summary>
        /// <param name="synchronousLambda">The lambda to protocol the operation.</param>
        public LambdaVoidProtocol(
            Action<TOperation> synchronousLambda)
        {
            synchronousLambda.MustForArg(nameof(synchronousLambda)).NotBeNull();
            this.synchronousLambda = synchronousLambda;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LambdaVoidProtocol{TOperation}"/> class.
        /// </summary>
        /// <param name="synchronousLambda">The synchronous lambda to protocol the operation.</param>
        /// <param name="asynchronousLambda">The asynchronous lambda to protocol the operation.</param>
        public LambdaVoidProtocol(
            Action<TOperation> synchronousLambda,
            Func<TOperation, Task> asynchronousLambda)
        {
            synchronousLambda.MustForArg(nameof(synchronousLambda)).NotBeNull();
            asynchronousLambda.MustForArg(nameof(asynchronousLambda)).NotBeNull();

            this.synchronousLambda = synchronousLambda;
            this.asyncAsynchronousLambda = asynchronousLambda;
        }

        /// <inheritdoc />
        public void Execute(
            TOperation operation)
        {
            if (this.synchronousLambda != null)
            {
                this.synchronousLambda(operation);
            }
            else
            {
                Run.TaskUntilCompletion(this.asyncAsynchronousLambda(operation));
            }
        }

        /// <inheritdoc />
        public async Task ExecuteAsync(
            TOperation operation)
        {
            if (this.asyncAsynchronousLambda != null)
            {
                await this.asyncAsynchronousLambda(operation);
            }
            else
            {
                await Task.Run(() => this.synchronousLambda(operation));
            }
        }
    }
}
