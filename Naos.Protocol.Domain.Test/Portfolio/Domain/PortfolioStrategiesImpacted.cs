namespace Naos.Protocol.Domain.Test {
    using System;
    using System.Collections.Generic;

    public class PortfolioStrategiesImpacted
    {
        public IReadOnlyCollection<PortfolioDescriptionViewModel> Descriptions { get; set; }
    }

    public class EntityImpactDiscover : IProtocol<Discover<EntityMembershipViewModel, PortfolioStrategiesImpacted>>
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
        public void Execute(Discover<EntityMembershipViewModel, PortfolioStrategiesImpacted> operation)
        {
            throw new System.NotImplementedException();
        }
    }
}