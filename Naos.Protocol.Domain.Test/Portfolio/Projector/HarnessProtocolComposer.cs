// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HarnessProtocolComposer.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Protocol.Domain.Test
{
    using System;
    using System.Collections.Generic;

    public class HarnessProtocolComposer : ProtocolComposerBase,
                                           IComposeProtocol<Handle<EntityMembershipViewModelUpdated>>
    {
        /// <inheritdoc />
        public override IReadOnlyCollection<Type> DependentComposerTypes => new[]
                                                                           {
                                                                               typeof(PortfolioProtocolComposer),
                                                                           };

        /// <inheritdoc />
        public IProtocol<Handle<EntityMembershipViewModelUpdated>> Compose()
        {
            return new PortfolioViewModelProjector(this.GetDependentComposer<PortfolioProtocolComposer>());
        }
    }
}
