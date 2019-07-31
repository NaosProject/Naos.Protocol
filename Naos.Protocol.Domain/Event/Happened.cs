﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Happened.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Protocol.Domain
{
    using System;

    /// <summary>
    /// Event container.
    /// </summary>
    public class Happened<TOperation> : EventBase
        where TOperation : class
    {
        public Happened(
            TOperation executedOperation)
        {
            this.ExecutedOperation = executedOperation ?? throw new ArgumentNullException(nameof(executedOperation));
        }

        public TOperation ExecutedOperation { get; private set; }
    }
}