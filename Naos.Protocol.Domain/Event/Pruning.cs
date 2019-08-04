// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Pruning.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Protocol.Domain
{
    using System;

    /// <summary>
    /// Event container.
    /// </summary>
    public class Pruning : EventBase
    {
        public Pruning(
            string pruner)
        {
            this.Pruner = pruner ?? throw new ArgumentNullException(nameof(pruner));
        }

        public string Pruner { get; private set; }
    }
}