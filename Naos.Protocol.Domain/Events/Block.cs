// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Block.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Protocol.Domain
{
    using System;

    /// <summary>
    /// Event container.
    /// </summary>
    public class Block : EventBase
    {
        public Block(
            string details)
        {
            this.Details = details ?? throw new ArgumentNullException(nameof(details));
        }

        public string Details { get; private set; }
    }
}