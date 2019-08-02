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
                                           IComposeProtocolNoReturn<Handle<EntityMembershipViewModelUpdated>>,
                                           IComposeProtocolWithReturn<GetLatest<PortfolioStreamLocator>, PortfolioStreamLocator>
    {
        /// <inheritdoc />
        public override IReadOnlyCollection<Type> DependentComposerTypes => new[]
                                                                            {
                                                                                typeof(PortfolioProtocolComposer),
                                                                                // typeof(EntitySqlProtocolComposer),
                                                                                typeof(SqlProtocolComposer<string, PortfolioViewModel, PortfolioStreamLocator>),
                                                                                typeof(SqlProtocolComposer<string, PortfolioDescriptionViewModel, PortfolioStreamLocator>),
                                                                            };

        /// <inheritdoc />
        public IProtocolNoReturn<Handle<EntityMembershipViewModelUpdated>> Compose()
        {
            return new PortfolioViewModelProjector(this.GetDependentComposer<PortfolioProtocolComposer>());
        }

        /// <inheritdoc />
        IProtocolWithReturn<GetLatest<PortfolioStreamLocator>, PortfolioStreamLocator>
            IComposeProtocolWithReturn<GetLatest<PortfolioStreamLocator>, PortfolioStreamLocator>.Compose()
        {
            return new ConfigGet<PortfolioStreamLocator>();
        }
    }
}
