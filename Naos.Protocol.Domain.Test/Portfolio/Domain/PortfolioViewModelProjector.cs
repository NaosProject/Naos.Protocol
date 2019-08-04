namespace Naos.Protocol.Domain.Test {
    using System;

    public class PortfolioViewModelProjector : IProtocolNoReturn<Handle<EntityMembershipViewModelUpdated>>
    {
        private readonly PortfolioProtocolComposer composer;

        private readonly IComposeProtocolWithReturn<Discover<EntityMembershipViewModel, PortfolioStrategiesImpacted>, PortfolioStrategiesImpacted> composerSpecific;

        private readonly IProtocolWithReturn<GetLatest<EntityMembershipViewModel>, EntityMembershipViewModel> composedEntity;
        private readonly IProtocolWithReturn<Discover<EntityMembershipViewModel, PortfolioStrategiesImpacted>, PortfolioStrategiesImpacted> composedDiscovery;

        public PortfolioViewModelProjector(PortfolioProtocolComposer composer)
            : this(composer, composer)
        {
            this.composer = composer ?? throw new ArgumentNullException(nameof(composer));
        }

        public PortfolioViewModelProjector(
            IComposeProtocolWithReturn<GetLatest<EntityMembershipViewModel>, EntityMembershipViewModel> composerEntity,
            IComposeProtocolWithReturn<Discover<EntityMembershipViewModel, PortfolioStrategiesImpacted>, PortfolioStrategiesImpacted> composerDiscovery)
            : this(composerEntity.Compose(), composerDiscovery.Compose())
        {
        }

        public PortfolioViewModelProjector(
            IProtocolWithReturn<GetLatest<EntityMembershipViewModel>, EntityMembershipViewModel> composedEntity,
            IProtocolWithReturn<Discover<EntityMembershipViewModel, PortfolioStrategiesImpacted>, PortfolioStrategiesImpacted> composedDiscovery)
        {
            this.composedEntity = composedEntity;
            this.composedDiscovery = composedDiscovery;
        }

        /// <inheritdoc />
        public void ExecuteNoReturn(Handle<EntityMembershipViewModelUpdated> operation)
        {
            PortfolioStrategiesImpacted portfolioStrategiesImpacted = null;

            var entityMembershipViewModel = this.composedEntity.ExecuteScalar(new GetLatest<EntityMembershipViewModel>());
            var discoverImpactOperation = new Discover<EntityMembershipViewModel, PortfolioStrategiesImpacted>(entityMembershipViewModel);

            // EITHER OR...
            portfolioStrategiesImpacted = this.composedDiscovery.ExecuteScalar(discoverImpactOperation);
            portfolioStrategiesImpacted = discoverImpactOperation.ExecuteScalar(this.composedDiscovery);

            foreach (var impacted in portfolioStrategiesImpacted.Descriptions)
            {
                var portfolioViewModel = new PortfolioViewModel(impacted, entityMembershipViewModel.Entities);
                this.composer.ExecuteNoReturn(new Put<PortfolioViewModel>(portfolioViewModel));
            }
        }
    }

    public static class Extensions
    {
        public static TReturn ExecuteScalarExtension<TOperation, TReturn>(this TOperation operation, IProtocolWithReturn<TOperation, TReturn> protocol)
            where TOperation : OperationWithReturnBase<TReturn>
        {
            var result = protocol.ExecuteScalar(operation);
            return result;
        }

        public static TReturn ExecuteScalarExtension<TOperation, TReturn>(this IProtocolWithReturn<TOperation, TReturn> protocol, TOperation operation)
            where TOperation : OperationWithReturnBase<TReturn>
        {
            var result = protocol.ExecuteScalar(operation);
            return result;
        }
    }
}