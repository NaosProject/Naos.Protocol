// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PortfolioProtocolComposer.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Protocol.Domain.Test
{
    using System.Collections;
    using System.Collections.Generic;

    public class PortfolioProtocolComposer : ProtocolComposerBase
    {
        /// <inheritdoc />
        public override IReadOnlyCollection<ProtocolPrototype> ProtocolRegistrations { get; }
    }
}
