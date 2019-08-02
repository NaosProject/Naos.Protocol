namespace Naos.Protocol.Domain.Test {
    using System;
    using System.Collections.Generic;

    public class PortfolioStrategiesImpacted
    {
        public IReadOnlyCollection<PortfolioDescriptionViewModel> Descriptions { get; set; }
    }

    public class EntityImpactDiscover : IProtocolWithReturn<Discover<EntityMembershipViewModel, PortfolioStrategiesImpacted>, PortfolioStrategiesImpacted>
    {
        public EntityImpactDiscover(
            IProtocol<GetLatest<PortfolioDescriptionViewModel>> portfolioProtocol,
            IProtocol<GetLatest<EntityMembershipViewModel>> entityProtocol)
        {
            this.PortfolioProtocol = portfolioProtocol ?? throw new ArgumentNullException(nameof(portfolioProtocol));
            this.EntityProtocol = entityProtocol ?? throw new ArgumentNullException(nameof(entityProtocol));
        }

        public IProtocol<GetLatest<PortfolioDescriptionViewModel>> PortfolioProtocol { get; }

        public IProtocol<GetLatest<EntityMembershipViewModel>> EntityProtocol { get; }

        /// <inheritdoc />
        public TReturn ExecuteScalar<TReturn>(
            Discover<EntityMembershipViewModel, PortfolioStrategiesImpacted> operation)
        {
            // basically do some kind of query across these two data sets and conclude the impact space and return it.
            // presently these are small enough that this can just be done in memory, you can imagine this breaking out into a 
            // stream and chunking it out if it became too big to do this way...
            throw new NotImplementedException();
        }
    }
}