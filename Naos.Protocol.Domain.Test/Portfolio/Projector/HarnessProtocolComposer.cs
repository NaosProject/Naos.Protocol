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
                                           IComposeProtocol<Handle<EntityMembershipViewModelUpdated>>,
                                           IComposeProtocol<GetLatest<PortfolioStreamLocator>>
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
        public IProtocol<Handle<EntityMembershipViewModelUpdated>> Compose()
        {
            this.Compose<GetLatest<PortfolioViewModel>>().Execute<PortfolioViewModel>();
            return new PortfolioViewModelProjector(this.GetDependentComposer<PortfolioProtocolComposer>());
        }

        /// <inheritdoc />
        IProtocol<GetLatest<PortfolioStreamLocator>> IComposeProtocol<GetLatest<PortfolioStreamLocator>>.Compose()
        {
            return new ConfigGet<PortfolioStreamLocator>();
        }
    }
}
