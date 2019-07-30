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
                                          IComposeProtocol<GetLatest<PortfolioStreamLocator>>,
                                          IComposeProtocol<DetermineStreamLocatorByKey<string, PortfolioStreamLocator>>,
                                          IComposeProtocol<GetLatest<PortfolioViewModel>>,
                                          IComposeProtocol<GetLatest<PortfolioDescriptionViewModel>>,
                                          IComposeProtocol<GetLatest<EntityMembershipViewModel>>,
                                          IComposeProtocol<Discover<EntityMembershipViewModel, PortfolioStrategiesImpacted>>
    {
        private readonly SqlProtocol<string, PortfolioViewModel, PortfolioStreamLocator> viewModelDataProtocol;
        private readonly SqlProtocol<string, PortfolioDescriptionViewModel, PortfolioStreamLocator> viewModelDescriptionDataProtocol;

        public PortfolioProtocolComposer()
        {
            this.viewModelDataProtocol = new SqlProtocol<string, PortfolioViewModel, PortfolioStreamLocator>(
                this.ReCompose<DetermineStreamLocatorByKey<string, PortfolioStreamLocator>, PortfolioStreamLocator>());
            this.viewModelDescriptionDataProtocol = new SqlProtocol<string, PortfolioDescriptionViewModel, PortfolioStreamLocator>(
                this.ReCompose<DetermineStreamLocatorByKey<string, PortfolioStreamLocator>, PortfolioStreamLocator>());
        }

        /// <inheritdoc />
        IProtocol<GetLatest<PortfolioStreamLocator>> IComposeProtocol<GetLatest<PortfolioStreamLocator>>.Compose()
        {
            return new ConfigGet<PortfolioStreamLocator>();
        }

        /// <inheritdoc />
        IProtocol<GetLatest<PortfolioViewModel>> IComposeProtocol<GetLatest<PortfolioViewModel>>.Compose()
        {
            return this.viewModelDataProtocol;
        }

        /// <inheritdoc />
        IProtocol<GetLatest<PortfolioDescriptionViewModel>> IComposeProtocol<GetLatest<PortfolioDescriptionViewModel>>.Compose()
        {
            return this.viewModelDescriptionDataProtocol;
        }

        /// <inheritdoc />
        public IProtocol<DetermineStreamLocatorByKey<string, PortfolioStreamLocator>> Compose()
        {
            var returnProtocol = this.ReCompose<GetLatest<PortfolioStreamLocator>, PortfolioStreamLocator>();
            var seededDetermination = new SeededDetermineStreamLocator<string, PortfolioStreamLocator>(
                returnProtocol);
            return seededDetermination;
        }

        IProtocol<Discover<EntityMembershipViewModel, PortfolioStrategiesImpacted>> IComposeProtocol<Discover<EntityMembershipViewModel, PortfolioStrategiesImpacted>>.Compose()
        {
            var entityProtocolComposer = this.GetDependentComposer<EntityProtocolComposer>();
            return new EntityImpactDiscover(
                this.viewModelDescriptionDataProtocol,
                entityProtocolComposer.GetDependentComposer<EntityProtocolComposer>().GetProtocol<GetLatest<EntityMembershipViewModel>>());
        }

        /// <inheritdoc />
        IProtocol<GetLatest<EntityMembershipViewModel>> IComposeProtocol<GetLatest<EntityMembershipViewModel>>.Compose()
        {
            return this.ReCompose<GetLatest<EntityMembershipViewModel>, EntityMembershipViewModel>();
        }
    }
}
