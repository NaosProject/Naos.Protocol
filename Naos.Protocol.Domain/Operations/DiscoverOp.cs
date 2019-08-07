// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DiscoverOp.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Protocol.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using OBeautifulCode.Representation;
    using OBeautifulCode.Type;
    using static System.FormattableString;

    /// <summary>
    /// Abstract base of an operation.
    /// </summary>
    /// <typeparam name="TObject">Type of data being written.</typeparam>
    [SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Discover", Justification = "Name/Spelling is correct.")]
    public class DiscoverOp<TObject, TReturn> : ReturningOperationBase<TReturn>
        where TObject : class
    {
        public DiscoverOp(
            TObject operationKey)
        {
            throw new NotImplementedException();
        }
    }

    public class SeededDetermineLocator<TInput, TLocator> : IReturningProtocol<DetermineLocatorByKeyOp<TInput, TLocator>, TLocator>
        where TInput : class
        where TLocator : LocatorBase
    {
        public SeededDetermineLocator(
            IReturningProtocol<GetLatestOp<TLocator>, TLocator> returnProtocol)
        {
            this.ReturnProtocol = returnProtocol ?? throw new ArgumentNullException(nameof(returnProtocol));
        }

        public IReturningProtocol<GetLatestOp<TLocator>, TLocator> ReturnProtocol { get; set; }

        /// <inheritdoc />
        public TLocator Execute(
            DetermineLocatorByKeyOp<TInput, TLocator> operation)
        {
            var actualOperation = new GetLatestOp<TLocator>();
            var result = this.ReturnProtocol.Execute(actualOperation);
            return result;
        }
    }
}