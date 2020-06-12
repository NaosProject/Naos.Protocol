// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IOperation.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Protocol.Domain
{
    using OBeautifulCode.Type;

    /// <summary>
    /// An operation is, conceptually, a method on an interface.
    /// </summary>
    /// <remarks>
    /// Some operations return an object and some don't.  The operation needs to be named and should be suffixed with
    /// Op' (e.g. DoSomeWorkOp).  Operations have 0 or more parameters.  These define the data with which the operation
    /// is being performed and any behavioral variables.
    ///
    /// Only abstract classes should implement this interface.  Other interfaces should not implement this interface.
    /// By defining operations in classes it forces the consumer to specify one operation per class (i.e. you can't
    /// implement the same interface multiple times for different conceptual operations).  This simplifies the
    /// definition, consumption, and serialization of operations.
    ///
    /// The two implementors below differentiate operations that return versus those that don't.  When you inherit
    /// from one of these base classes, the nature of the operation from a return perspective should be evident and
    /// declarative because it prevents the consumer from defining a void operation where a returning operation is
    /// actually required.  The TReturn in <see cref="ReturningOperationBase{TReturn}"/> is not used by the class itself
    /// and is thus not enforced when the operation is being authored.  However, the
    /// <see cref="IProtocol{TOperation}"/> associated with the operation requires this generic parameter to define
    /// the signature of the method that can execute the operation.
    /// </remarks>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1040:AvoidEmptyInterfaces", Justification = "Prefer interface over attribute here.")]
    public interface IOperation
    {
    }

    /// <summary>
    /// Represents an operation that does not return (void) any object.
    /// </summary>
    public abstract class VoidOperationBase : IOperation
    {
    }

    /// <summary>
    /// Represents an operation that returns some object.
    /// </summary>
    /// <typeparam name="TReturn">The type of the object that the operation returns.</typeparam>
    public abstract class ReturningOperationBase<TReturn> : IOperation
    {
    }
}
