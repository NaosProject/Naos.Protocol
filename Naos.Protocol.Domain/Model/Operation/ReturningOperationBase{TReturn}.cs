// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ReturningOperationBase{TReturn}.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Protocol.Domain
{
    using OBeautifulCode.Type;

    /// <summary>
    /// Represents an operation that returns some object.
    /// </summary>
    /// <typeparam name="TReturn">The type of the object that the operation returns.</typeparam>
    public abstract partial class ReturningOperationBase<TReturn> : IOperation, IModelViaCodeGen
    {
    }
}