// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PutOp.cs" company="Naos Project">
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
    public class PutOp<TObject> : VoidOperationBase
        where TObject : class
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PutOp{TObject}"/> class.
        /// </summary>
        /// <param name="payload">The payload to operate on.</param>
        public PutOp(TObject payload)
        {
            this.Payload = payload ?? throw new ArgumentNullException(nameof(payload));
        }

        /// <summary>
        /// Gets the payload.
        /// </summary>
        /// <value>The payload.</value>
        public TObject Payload { get; private set; }
    }
}