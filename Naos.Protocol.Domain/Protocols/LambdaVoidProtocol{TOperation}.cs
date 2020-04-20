// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LambdaVoidProtocol{TOperation}.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Protocol.Domain
{
    using System;
    using OBeautifulCode.Assertion.Recipes;

    /// <summary>
    /// Event container.
    /// </summary>
    /// <typeparam name="TOperation">Type of operation.</typeparam>
    public class LambdaVoidProtocol<TOperation> : IVoidProtocol<TOperation>
    where TOperation : VoidOperationBase
    {
        private readonly Action<TOperation> lambda;

        /// <summary>
        /// Initializes a new instance of the <see cref="LambdaVoidProtocol{TOperation}"/> class.
        /// </summary>
        /// <param name="lambda">The lambda to extract tags.</param>
        public LambdaVoidProtocol(
            Action<TOperation> lambda)
        {
            lambda.MustForArg(nameof(lambda)).NotBeNull();
            this.lambda = lambda;
        }

        /// <inheritdoc />
        public void Execute(
            TOperation operation)
        {
            this.lambda(operation);
        }
    }
}
