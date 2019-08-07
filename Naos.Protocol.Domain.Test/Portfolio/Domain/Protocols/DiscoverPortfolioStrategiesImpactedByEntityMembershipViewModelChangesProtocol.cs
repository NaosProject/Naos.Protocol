namespace Naos.Protocol.Domain.Test {

    using System;

    public class DiscoverPortfolioStrategiesImpactedByEntityMembershipViewModelChangesProtocol : 
        IReturningProtocol<DiscoverPortfolioStrategiesImpactedByEntityMembershipViewModelChangesOp, PortfolioStrategiesImpacted>
    {
        public DiscoverPortfolioStrategiesImpactedByEntityMembershipViewModelChangesProtocol(
            IGetReturningProtocol<GetLatestOp<PortfolioDescriptionViewModel>, PortfolioDescriptionViewModel> portfolioProtocol,
            IGetReturningProtocol<GetLatestOp<EntityMembershipViewModel>, EntityMembershipViewModel> entityProtocol)
        {
            this.PortfolioProtocol = portfolioProtocol?.Get() ?? throw new ArgumentNullException(nameof(portfolioProtocol));
            this.EntityProtocol = entityProtocol?.Get() ?? throw new ArgumentNullException(nameof(entityProtocol));
        }

        public IProtocol<GetLatestOp<PortfolioDescriptionViewModel>> PortfolioProtocol { get; }

        public IProtocol<GetLatestOp<EntityMembershipViewModel>> EntityProtocol { get; }

        /// <inheritdoc />
        public PortfolioStrategiesImpacted Execute(DiscoverPortfolioStrategiesImpactedByEntityMembershipViewModelChangesOp operation)
        {
            // basically do some kind of query across these two data sets and conclude the impact space and return it.
            // presently these are small enough that this can just be done in memory, you can imagine this breaking out into a 
            // stream and chunking it out if it became too big to do this way...
            throw new NotImplementedException();
        }
    }
}