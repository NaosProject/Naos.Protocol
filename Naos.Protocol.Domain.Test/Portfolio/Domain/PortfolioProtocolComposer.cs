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
                                          IRequireProtocolWithReturn<GetLatest<PortfolioStreamLocator>, PortfolioStreamLocator>, //-- where do we share things
                                          IRequireProtocolWithReturn<DetermineLocatorByKey<string, PortfolioStreamLocator>, PortfolioStreamLocator>, //-- where do we share these, another interface ILocatePortfolioStreams or just ILocateStreams<tkey, tstream>
                                          IComposeProtocolWithReturn<GetLatest<PortfolioViewModel>, PortfolioViewModel>,
                                          IComposeProtocolWithReturn<GetLatest<PortfolioDescriptionViewModel>, PortfolioDescriptionViewModel>,
                                          IComposeProtocolWithReturn<GetLatest<EntityMembershipViewModel>, EntityMembershipViewModel>,
                                          IComposeProtocolWithReturn<Discover<EntityMembershipViewModel, PortfolioStrategiesImpacted>, PortfolioStrategiesImpacted>
    {
        IProtocolWithReturn<Discover<EntityMembershipViewModel, PortfolioStrategiesImpacted>, PortfolioStrategiesImpacted> IComposeProtocolWithReturn<Discover<EntityMembershipViewModel, PortfolioStrategiesImpacted>, PortfolioStrategiesImpacted>.Compose()
        {
            var entityProtocolComposer = this.GetDependentComposer<EntityProtocolComposer>(); // is this better to connect the ideas?
            var portfolioProtocol = this.GetProtocol<GetLatest<PortfolioDescriptionViewModel>>();
            var entityProtocol = this.GetProtocol<GetLatest<EntityMembershipViewModel>>();
            return new EntityImpactDiscover(
                portfolioProtocol,
                entityProtocol);
        }

        /// <inheritdoc />
        IProtocolWithReturn<GetLatest<PortfolioStreamLocator>, PortfolioStreamLocator>
            IComposeProtocolWithReturn<GetLatest<PortfolioStreamLocator>, PortfolioStreamLocator>.Compose()
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        IProtocolWithReturn<DetermineLocatorByKey<string, PortfolioStreamLocator>, PortfolioStreamLocator>
            IComposeProtocolWithReturn<DetermineLocatorByKey<string, PortfolioStreamLocator>, PortfolioStreamLocator>.Compose()
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        IProtocolWithReturn<GetLatest<PortfolioViewModel>, PortfolioViewModel>
            IComposeProtocolWithReturn<GetLatest<PortfolioViewModel>, PortfolioViewModel>.Compose()
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        IProtocolWithReturn<GetLatest<PortfolioDescriptionViewModel>, PortfolioDescriptionViewModel>
            IComposeProtocolWithReturn<GetLatest<PortfolioDescriptionViewModel>, PortfolioDescriptionViewModel>.Compose()
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        IProtocolWithReturn<GetLatest<EntityMembershipViewModel>, EntityMembershipViewModel>
            IComposeProtocolWithReturn<GetLatest<EntityMembershipViewModel>, EntityMembershipViewModel>.Compose()
        {
            throw new NotImplementedException();
        }
    }
}
