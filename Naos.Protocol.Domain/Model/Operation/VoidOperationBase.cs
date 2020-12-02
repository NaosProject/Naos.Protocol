// --------------------------------------------------------------------------------------------------------------------
// <copyright file="VoidOperationBase.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Protocol.Domain
{
    using System.Collections.Generic;
    using OBeautifulCode.Type;

    /// <summary>
    /// Represents an operation that does not return (void) any object.
    /// </summary>
    public abstract partial class VoidOperationBase : OperationBase, IVoidOperation, IModelViaCodeGen
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VoidOperationBase"/> class.
        /// </summary>
        protected VoidOperationBase()
        {
        }
    }
}