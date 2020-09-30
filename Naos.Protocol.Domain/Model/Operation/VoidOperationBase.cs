// --------------------------------------------------------------------------------------------------------------------
// <copyright file="VoidOperationBase.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Protocol.Domain
{
    using OBeautifulCode.Type;

    /// <summary>
    /// Represents an operation that does not return (void) any object.
    /// </summary>
    public abstract partial class VoidOperationBase : OperationBase, IVoidOperation, IModelViaCodeGen
    {
    }
}