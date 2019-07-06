// --------------------------------------------------------------------------------------------------------------------
// <copyright file="OperationBase.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Protocol.Domain
{
    using System;
    using System.Linq.Expressions;
    using OBeautifulCode.Validation.Recipes;

    /// <summary>
    /// Abstract base of an operation.
    /// </summary>
    public abstract class OperationBase
    {
    }

    /// <summary>
    /// An <see cref="OperationBase" /> that does NOT mutate state.
    /// </summary>
    public abstract class ReadOperationBase : OperationBase
    {
    }

    /// <summary>
    /// An <see cref="OperationBase" /> that DOES mutate state.
    /// </summary>
    public abstract class WriteOperationBase : OperationBase
    {
    }

    /// <summary>
    /// Fake class to be the necessary return type for an operation.
    /// </summary>
    public class NoReturnType
    {
    }

    /// <summary>
    /// Prototype of an operation that can inflated into an operation with a <see cref="Locker" /> of necessary inputs.
    /// </summary>
    public class OperationPrototype
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OperationPrototype"/> class.
        /// </summary>
        /// <param name="description">Description of the operation.</param>
        /// <param name="operationBuilder">Builder taking in the locker of output from previous runs.</param>
        public OperationPrototype(
            string description,
            Expression<Func<Locker, OperationBase>> operationBuilder)
        {
            new { description }.Must().NotBeNullNorWhiteSpace();
            new { operationBuilder }.Must().NotBeNull();

            this.Description = description;
            this.OperationBuilder = operationBuilder;
        }

        /// <summary>
        /// Gets the description of the operation.
        /// </summary>
        public string Description { get; private set; }

        /// <summary>
        /// Gets the builder for the operation.
        /// </summary>
        public Expression<Func<Locker, OperationBase>> OperationBuilder { get; private set; }
    }
}
