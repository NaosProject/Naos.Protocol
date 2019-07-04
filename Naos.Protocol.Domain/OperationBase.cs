// --------------------------------------------------------------------------------------------------------------------
// <copyright file="OperationBase.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Protocol.Domain
{
    /// <summary>
    /// Abstract base of an operation.
    /// </summary>
    public abstract class OperationBase
    {
    }

    /// <summary>
    /// An <see cref="OperationBase" /> that does NOT mutate state.
    /// </summary>
    /// <typeparam name="TReturn">Return type of the operation.</typeparam>
    public abstract class ReadOperationBase<TReturn> : OperationBase
    {
    }

    /// <summary>
    /// An <see cref="OperationBase" /> that DOES mutate state.
    /// </summary>
    /// <typeparam name="TReturn">Return type of the operation.</typeparam>
    public abstract class WriteOperationBase<TReturn> : OperationBase
    {
    }

    /// <summary>
    /// Fake class to be the necessary return type for an operation.
    /// </summary>
    public class NoReturnType
    {
    }
}
