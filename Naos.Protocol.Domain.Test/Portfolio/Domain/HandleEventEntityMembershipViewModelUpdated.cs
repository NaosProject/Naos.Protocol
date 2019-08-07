namespace Naos.Protocol.Domain.Test {
    using System;

    public class HandleEventEntityMembershipViewModelUpdated : IVoidProtocol<HandleEventOp<EntityMembershipViewModelUpdated>>
    {
        private readonly PortfolioProtocolComposer composer;

        private readonly IGetReturningProtocol<DiscoverOp<EntityMembershipViewModel, PortfolioStrategiesImpacted>, PortfolioStrategiesImpacted> composerSpecific;

        private readonly IReturningProtocol<GetLatestOp<EntityMembershipViewModel>, EntityMembershipViewModel> composedEntity;
        private readonly IReturningProtocol<DiscoverOp<EntityMembershipViewModel, PortfolioStrategiesImpacted>, PortfolioStrategiesImpacted> composedDiscovery;

        public HandleEventEntityMembershipViewModelUpdated(PortfolioProtocolComposer composer)
            : this(composer, composer)
        {
            this.composer = composer ?? throw new ArgumentNullException(nameof(composer));
        }

        public HandleEventEntityMembershipViewModelUpdated(
            IGetReturningProtocol<GetLatestOp<EntityMembershipViewModel>, EntityMembershipViewModel> composerEntity,
            IGetReturningProtocol<DiscoverOp<EntityMembershipViewModel, PortfolioStrategiesImpacted>, PortfolioStrategiesImpacted> composerDiscovery)
            : this(composerEntity.Get(), composerDiscovery.Get())
        {
        }

        public HandleEventEntityMembershipViewModelUpdated(
            IReturningProtocol<GetLatestOp<EntityMembershipViewModel>, EntityMembershipViewModel> composedEntity,
            IReturningProtocol<DiscoverOp<EntityMembershipViewModel, PortfolioStrategiesImpacted>, PortfolioStrategiesImpacted> composedDiscovery)
        {
            this.composedEntity = composedEntity;
            this.composedDiscovery = composedDiscovery;
        }

        /// <inheritdoc />
        public void Execute(HandleEventOp<EntityMembershipViewModelUpdated> operation)
        {
            var entityMembershipViewModel = this.composedEntity.Execute(new GetLatestOp<EntityMembershipViewModel>());
            var discoverImpactOperation = new DiscoverOp<EntityMembershipViewModel, PortfolioStrategiesImpacted>(entityMembershipViewModel);

            var portfolioStrategiesImpacted = this.composedDiscovery.Execute(discoverImpactOperation);

            foreach (var impacted in portfolioStrategiesImpacted.Descriptions)
            {
                var portfolioViewModel = new PortfolioViewModel(impacted, entityMembershipViewModel.Entities);
                this.composer.ExecuteNoReturn(new PutOp<PortfolioViewModel>(portfolioViewModel));
            }
        }
    }

    public static class Extensions
    {
        public static TReturn ExecuteScalarExtension<TOperation, TReturn>(this TOperation operation, IReturningProtocol<TOperation, TReturn> protocol)
            where TOperation : ReturningOperationBase<TReturn>
        {
            var result = protocol.Execute(operation);
            return result;
        }

        public static TReturn ExecuteScalarExtension<TOperation, TReturn>(this IReturningProtocol<TOperation, TReturn> protocol, TOperation operation)
            where TOperation : ReturningOperationBase<TReturn>
        {
            var result = protocol.Execute(operation);
            return result;
        }
    }
}