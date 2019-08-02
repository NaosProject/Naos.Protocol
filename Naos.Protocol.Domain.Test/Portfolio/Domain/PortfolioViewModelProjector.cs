namespace Naos.Protocol.Domain.Test {
    using System;

    public class PortfolioViewModelProjector : IProtocolNoReturn<Handle<EntityMembershipViewModelUpdated>>
    {
        private readonly IProtocolWithReturn<Discover<EntityMembershipViewModel, PortfolioStrategiesImpacted>, PortfolioStrategiesImpacted> composed;

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
            this.composed.ExecuteScalar(discoverImpactOperation); // can we put TOperation on OperationWithReturnBase?
            discoverImpactOperation.ExecuteScalarExtension(this.composer);
            this.composer.ExecuteScalarExtension(discoverImpactOperation);
            var portfolioStrategiesImpacted = discoverImpactOperation.Execute(this.composer);
            this.composer.ExecuteScalar(discoverImpactOperation);

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
            var result = protocol.ExecuteScalar<TReturn>(operation);
            return result;
        }

        public static TReturn ExecuteScalarExtension<TOperation, TReturn>(this IProtocolWithReturn<TOperation, TReturn> protocol, TOperation operation)
            where TOperation : OperationWithReturnBase<TReturn>
        {
            var result = protocol.ExecuteScalar<TReturn>(operation);
            return result;
        }
    }
}