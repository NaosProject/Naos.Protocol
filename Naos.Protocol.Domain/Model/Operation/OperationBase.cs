// --------------------------------------------------------------------------------------------------------------------
// <copyright file="OperationBase.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Protocol.Domain
{
    using OBeautifulCode.Type;

    /// <summary>
    /// Represents an operation that does not return (void) any object.
    /// </summary>
    public abstract partial class OperationBase : IOperation, IModelViaCodeGen
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OperationBase"/> class.
        /// </summary>
        protected OperationBase()
        {
        }
    }
}