// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Handling.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Protocol.Domain
{
    using System;

    /// <summary>
    /// Event container.
    /// </summary>
    public class Handling<TOperation> : EventBase
        where TOperation : class
    {
        public Handling(
            TOperation executedOperation)
        {
            this.ExecutedOperation = executedOperation ?? throw new ArgumentNullException(nameof(executedOperation));
        }

        public TOperation ExecutedOperation { get; private set; }
    }
}