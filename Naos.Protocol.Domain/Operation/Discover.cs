// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Discover.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Protocol.Domain
{
    using System;
    using System.Collections.Generic;
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
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Discover", Justification = "Name/Spelling is correct.")]
    public class Discover<TObject, TReturn> : OperationWithReturnBase<TReturn>
        where TObject : class
    {
        public Discover(
            TObject operationKey)
        {
            throw new NotImplementedException();
        }
    }

    public class SeededDetermineLocator<TInput, TLocator> : IProtocolWithReturn<DetermineLocatorByKey<TInput, TLocator>, TLocator>
        where TInput : class
        where TLocator : LocatorBase
    {
        public SeededDetermineLocator(
            IProtocolWithReturn<GetLatest<TLocator>, TLocator> returnProtocol)
        {
            this.ReturnProtocol = returnProtocol ?? throw new ArgumentNullException(nameof(returnProtocol));
        }

        public IProtocolWithReturn<GetLatest<TLocator>, TLocator> ReturnProtocol { get; set; }

        /// <inheritdoc />
        public TReturn ExecuteScalar<TReturn>(
            DetermineLocatorByKey<TInput, TLocator> operation)
        {
            var actualOperation = new GetLatest<TLocator>();
            var result = this.ReturnProtocol.ExecuteScalar<TReturn>(actualOperation);
            return result;
        }
    }
}