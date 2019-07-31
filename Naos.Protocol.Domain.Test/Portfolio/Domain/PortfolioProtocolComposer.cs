// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PortfolioProtocolComposer.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Protocol.Domain.Test
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class PortfolioProtocolComposer : ProtocolComposerBase,
                                          IRequireProtocol<GetLatest<PortfolioStreamLocator>>, //-- where do we share things
                                          IRequireProtocol<DetermineLocatorByKey<string, PortfolioStreamLocator>>, //-- where do we share these, another interface ILocatePortfolioStreams or just ILocateStreams<tkey, tstream>
                                          IComposeProtocol<GetLatest<PortfolioViewModel>>,
                                          IComposeProtocol<GetLatest<PortfolioDescriptionViewModel>>,
                                          IComposeProtocol<GetLatest<EntityMembershipViewModel>>,
                                          IComposeProtocol<Discover<EntityMembershipViewModel, PortfolioStrategiesImpacted>>
    {

        /// <inheritdoc />
        IProtocol<GetLatest<PortfolioViewModel>> IComposeProtocol<GetLatest<PortfolioViewModel>>.Compose()
        {
            return this.ReCompose<GetLatest<PortfolioViewModel>>();
        }

        /// <inheritdoc />
        IProtocol<GetLatest<PortfolioDescriptionViewModel>> IComposeProtocol<GetLatest<PortfolioDescriptionViewModel>>.Compose()
        {
            return this.ReCompose<GetLatest<PortfolioDescriptionViewModel>>();
        }

        /// <inheritdoc />
        public IProtocol<DetermineLocatorByKey<string, PortfolioStreamLocator>> Compose()
        {
            var returnProtocol = this.ReCompose<GetLatest<PortfolioStreamLocator>>();
            var seededDetermination = new SeededDetermineLocator<string, PortfolioStreamLocator>(returnProtocol);
            return seededDetermination;
        }

        IProtocol<Discover<EntityMembershipViewModel, PortfolioStrategiesImpacted>> IComposeProtocol<Discover<EntityMembershipViewModel, PortfolioStrategiesImpacted>>.Compose()
        {
            var entityProtocolComposer = this.GetDependentComposer<EntityProtocolComposer>(); // is this better to connect the ideas?
            var portfolioProtocol = this.GetProtocol<GetLatest<PortfolioDescriptionViewModel>>();
            var entityProtocol = this.GetProtocol<GetLatest<EntityMembershipViewModel>>();
            return new EntityImpactDiscover(
                portfolioProtocol,
                entityProtocol);
        }

        /// <inheritdoc />
        IProtocol<GetLatest<EntityMembershipViewModel>> IComposeProtocol<GetLatest<EntityMembershipViewModel>>.Compose()
        {
            return this.ReCompose<GetLatest<EntityMembershipViewModel>>();
        }

        /// <inheritdoc />
        IProtocol<GetLatest<PortfolioStreamLocator>> IComposeProtocol<GetLatest<PortfolioStreamLocator>>.Compose()
        {
            return this.ReCompose<GetLatest<PortfolioStreamLocator>>();
        }
    }
}
