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
                                          IRequireProtocolWithReturn<GetLatestOp<PortfolioStreamLocator>, PortfolioStreamLocator>,
                                          IAggregateReadProtocol<string, PortfolioViewModel> aggregateReadProtocol,
                                          IRequireProtocolWithReturn<GetLatestOp<PortfolioDescriptionViewModel>, PortfolioDescriptionViewModel>,
                                          IRequireProtocolWithReturn<GetLatestOp<EntityMembershipViewModel>, EntityMembershipViewModel>,
                                          IGetReturningProtocol<DetermineLocatorByKeyOp<string, PortfolioStreamLocator>, PortfolioStreamLocator>,
                                          IGetReturningProtocol<DiscoverPortfolioStrategiesImpactedByEntityMembershipViewModelChangesOp, PortfolioStrategiesImpacted>
    {
        // concept 1
        public override IReadOnlyCollection<Type> DependentComposerTypes => base.DependentComposerTypes;

        // concept 2
        // IRequireProtocolWithReturn

        // concept 3
        // IGetReturningProtocol

        public IReturningProtocol<DiscoverPortfolioStrategiesImpactedByEntityMembershipViewModelChangesOp, PortfolioStrategiesImpacted> Get()
        {
            var entityProtocolComposer = this.GetDependentComposer<EntityProtocolComposer>(); // is this better to connect the ideas?
            var entityProtocol = this.GetProtocol<GetLatestOp<EntityMembershipViewModel>, EntityMembershipViewModel>();

            var portfolioProtocol = this.GetProtocol<GetLatestOp<PortfolioDescriptionViewModel>, PortfolioDescriptionViewModel>();
            return new DiscoverPortfolioStrategiesImpactedByEntityMembershipViewModelChangesProtocol(portfolioProtocol, entityProtocol);
        }

        /// <inheritdoc />
        IReturningProtocol<GetLatestOp<PortfolioStreamLocator>, PortfolioStreamLocator>
            IGetReturningProtocol<GetLatestOp<PortfolioStreamLocator>, PortfolioStreamLocator>.Get()
        {
            var result = this.DelegatedGet<GetLatestOp<PortfolioStreamLocator>, PortfolioStreamLocator>();

            return result;
        }

        /// <inheritdoc />
        IReturningProtocol<DetermineLocatorByKeyOp<string, PortfolioStreamLocator>, PortfolioStreamLocator>
            IGetReturningProtocol<DetermineLocatorByKeyOp<string, PortfolioStreamLocator>, PortfolioStreamLocator>.Get()
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        IReturningProtocol<GetLatestOp<PortfolioViewModel>, PortfolioViewModel>
            IGetReturningProtocol<GetLatestOp<PortfolioViewModel>, PortfolioViewModel>.Get()
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        IReturningProtocol<GetLatestOp<PortfolioDescriptionViewModel>, PortfolioDescriptionViewModel>
            IGetReturningProtocol<GetLatestOp<PortfolioDescriptionViewModel>, PortfolioDescriptionViewModel>.Get()
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        IReturningProtocol<GetLatestOp<EntityMembershipViewModel>, EntityMembershipViewModel>
            IGetReturningProtocol<GetLatestOp<EntityMembershipViewModel>, EntityMembershipViewModel>.Get()
        {
            throw new NotImplementedException();
        }
    }
}
