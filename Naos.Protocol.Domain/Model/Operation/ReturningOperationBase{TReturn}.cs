// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ReturningOperationBase{TReturn}.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Protocol.Domain
{
    using System;
    using System.Collections.Generic;
    using OBeautifulCode.Type;

    /// <summary>
    /// Represents an operation that returns some object.
    /// </summary>
    /// <typeparam name="TReturn">The type of the object that the operation returns.</typeparam>
    public abstract partial class ReturningOperationBase<TReturn> : OperationBase, IReturningOperation<TReturn>, IModelViaCodeGen
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ReturningOperationBase{TReturn}"/> class.
        /// </summary>
        protected ReturningOperationBase()
        {
        }
    }
}