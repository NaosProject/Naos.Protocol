// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LambdaReturningProtocol{TOperation,TReturn}.cs" company="Naos Project">
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
    /// <typeparam name="TReturn">Type of the return.</typeparam>
    public class LambdaReturningProtocol<TOperation, TReturn> : IReturningProtocol<TOperation, TReturn>
    where TOperation : ReturningOperationBase<TReturn>
    {
        private readonly Func<TOperation, TReturn> lambda;

        /// <summary>
        /// Initializes a new instance of the <see cref="LambdaReturningProtocol{TOperation,TReturn}"/> class.
        /// </summary>
        /// <param name="lambda">The lambda to extract tags.</param>
        public LambdaReturningProtocol(
            Func<TOperation, TReturn> lambda)
        {
            lambda.MustForArg(nameof(lambda)).NotBeNull();
            this.lambda = lambda;
        }

        /// <inheritdoc />
        public TReturn Execute(
            TOperation operation)
        {
            var result = this.lambda(operation);
            return result;
        }
    }
}
