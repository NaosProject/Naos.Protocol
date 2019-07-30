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
    public class Discover<TObject, TReturn> : OperationBase<TReturn>
        where TObject : class
    {
        public Discover(
            TObject operationKey)
        {
            throw new NotImplementedException();
        }
    }

    public class SeededDiscovery<TInput, TOutput> : IProtocol<Discover<TInput, TOutput>, TOutput>
        where TInput : class
        where TOutput : class
    {
        public SeededDiscovery(
            IProtocol<GetLatest<TOutput>, TOutput> returnProtocol)
        {
            this.ReturnProtocol = returnProtocol ?? throw new ArgumentNullException(nameof(returnProtocol));
        }

        public IProtocol<GetLatest<TOutput>, TOutput> ReturnProtocol { get; set; }

        public void Execute(
            Discover<TInput, TOutput> operation)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public TReturn Execute<TReturn>(
            Discover<TInput, TOutput> operation)
        {
            return this.ReturnProtocol.Execute<TReturn>(new GetLatest<TOutput>());
        }
    }
}