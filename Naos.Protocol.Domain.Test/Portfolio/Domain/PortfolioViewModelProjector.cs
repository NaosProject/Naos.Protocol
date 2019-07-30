namespace Naos.Protocol.Domain.Test {
    using System;

    public class PortfolioViewModelProjector : IProtocol<Handle<EntityMembershipViewModelUpdated>>
    {
        private readonly PortfolioProtocolComposer composer;

        public PortfolioViewModelProjector(PortfolioProtocolComposer composer)
        {
            this.composer = composer ?? throw new ArgumentNullException(nameof(composer));
        }

        /// <inheritdoc />
        public void Execute(Handle<EntityMembershipViewModelUpdated> operation)
        {

            var entityMembershipViewModel = this.composer.Execute<GetLatest<EntityMembershipViewModel>, EntityMembershipViewModel>(new GetLatest<EntityMembershipViewModel>());
            var portfolioStrategiesImpacted = this.composer.Execute<Discover<EntityMembershipViewModel, PortfolioStrategiesImpacted>, PortfolioStrategiesImpacted>(
                    new Discover<EntityMembershipViewModel, PortfolioStrategiesImpacted>(entityMembershipViewModel));

            foreach (var impacted in portfolioStrategiesImpacted.Descriptions)
            {
                var portfolioViewModel = new PortfolioViewModel(impacted, entityMembershipViewModel.Entities);
                this.composer.Execute(new Put<PortfolioViewModel>(portfolioViewModel));
            }
        }
    }
}