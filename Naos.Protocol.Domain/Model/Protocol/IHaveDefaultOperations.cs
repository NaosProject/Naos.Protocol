// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IHaveDefaultOperations.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Protocol.Domain
{
    using System.Collections.Generic;

    /// <summary>
    /// Interface for a <see cref="IProtocol{TOperation}"/> (a class that supports executing one or more <see cref="IOperation"/>)
    /// to declare an ordered set of <see cref="IOperation"/>'s that can be executed on the protocol externally.
    /// </summary>
    public interface IHaveDefaultOperations : ISyncAndAsyncVoidProtocol<ExecuteDefaultOperationsOnProtocolOp>
    {
        /// <summary>
        /// Gets the default and ordinal operations.
        /// </summary>
        /// <value>The default and ordinal operations.</value>
        IReadOnlyList<IOperation> DefaultOperations { get; }
    }
}
