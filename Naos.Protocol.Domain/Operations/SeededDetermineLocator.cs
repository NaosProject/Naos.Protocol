// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SeededDetermineLocator.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Protocol.Domain
{
    using System;

    /// <summary>
    /// Single locator protocol that will just return a seeded value regardless of input.
    /// </summary>
    /// <typeparam name="TInput">The type of the t input.</typeparam>
    /// <typeparam name="TLocator">The type of the t locator.</typeparam>
    public class SeededDetermineLocator<TInput, TLocator> : IReturningProtocol<DetermineLocatorByKeyOp<TInput, TLocator>, TLocator>
        where TInput : class
        where TLocator : LocatorBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SeededDetermineLocator{TInput, TLocator}" /> class.
        /// </summary>
        /// <param name="returnProtocol">The return protocol.</param>
        public SeededDetermineLocator(
            IReturningProtocol<GetLatestOp<TLocator>, TLocator> returnProtocol)
        {
            this.ReturnProtocol = returnProtocol ?? throw new ArgumentNullException(nameof(returnProtocol));
        }

        /// <summary>
        /// Gets or sets the return protocol.
        /// </summary>
        /// <value>The return protocol.</value>
        public IReturningProtocol<GetLatestOp<TLocator>, TLocator> ReturnProtocol { get; set; }

        /// <summary>
        /// Executes the specified operation.
        /// </summary>
        /// <param name="operation">The operation.</param>
        /// <returns>TLocator.</returns>
        /// <inheritdoc />
        public TLocator Execute(
            DetermineLocatorByKeyOp<TInput, TLocator> operation)
        {
            var actualOperation = new GetLatestOp<TLocator>();
            var result          = this.ReturnProtocol.Execute(actualOperation);
            return result;
        }
    }
}