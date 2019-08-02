namespace Naos.Protocol.Domain.Test {
    using System;

    public class PortfolioViewModelProjector : IProtocolNoReturn<Handle<EntityMembershipViewModelUpdated>>
    {
        private readonly PortfolioProtocolComposer composer;

        public PortfolioViewModelProjector(PortfolioProtocolComposer composer)
        {
            this.composer = composer ?? throw new ArgumentNullException(nameof(composer));
        }

        /// <inheritdoc />
        public void ExecuteNoReturn(Handle<EntityMembershipViewModelUpdated> operation)
        {
            var entityMembershipViewModel = this.composer.ExecuteScalar<GetLatest<EntityMembershipViewModel>, EntityMembershipViewModel>(new GetLatest<EntityMembershipViewModel>());
            var discoverImpactOperation = new Discover<EntityMembershipViewModel, PortfolioStrategiesImpacted>(entityMembershipViewModel);
            var portfolioStrategiesImpacted = this.composer.ExecuteScalar<Discover<EntityMembershipViewModel, PortfolioStrategiesImpacted>, PortfolioStrategiesImpacted>(
                    discoverImpactOperation);

            foreach (var impacted in portfolioStrategiesImpacted.Descriptions)
            {
                var portfolioViewModel = new PortfolioViewModel(impacted, entityMembershipViewModel.Entities);
                this.composer.ExecuteNoReturn(new Put<PortfolioViewModel>(portfolioViewModel));
            }
        }
    }
}