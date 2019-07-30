// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HarnessProtocolComposer.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Protocol.Domain.Test
{
    using System;
    using System.Collections.Generic;
    using Naos.Configuration.Domain;
    using OBeautifulCode.Representation;

    public class HarnessProtocolComposer : ProtocolComposerBase
    {
        /// <inheritdoc />
        public override IReadOnlyCollection<ProtocolPrototype> ProtocolRegistrations
        {
            get
            {
                var entityStreamLocatorProtocol = new ProtocolPrototype<GetLatest<EntityStreamLocator>>(_ => new ConfigGet<EntityStreamLocator>());
                var discoverEntityStreamLocatorProtocol = new ProtocolPrototype<Discover<string, EntityStreamLocator>>(
                    _ => new SeededDiscovery<string, EntityStreamLocator>(_.GetProtocol<GetLatest<EntityStreamLocator>, EntityStreamLocator>()));
                var portfolioStreamLocatorProtocol = new ProtocolPrototype<GetLatest<PortfolioStreamLocator>>(_ => new ConfigGet<PortfolioStreamLocator>());
                var discoverPortfolioStreamLocatorProtocol = new ProtocolPrototype<Discover<string, PortfolioStreamLocator>>(
                    _ => new SeededDiscovery<string, PortfolioStreamLocator>(_.GetProtocol<GetLatest<PortfolioStreamLocator>, PortfolioStreamLocator>()));
                var getEntityMemberShipViewModelByKeyProtocol = new ProtocolPrototype<GetByKey<string, EntityMembershipViewModel>>(
                    _ => new SqlProtocol<string, EntityMembershipViewModel>(_.GetProtocol<Discover<string, PortfolioStreamLocator>, PortfolioStreamLocator>()));
                var portfolioViewModelProjectorProtocol = new ProtocolPrototype<
                    Handle<Created<
                        EntityMembershipViewModel>>>(_ => new PortfolioViewModelProjector((PortfolioProtocolComposer)_));

                var result = new List<ProtocolPrototype>
                             {
                                 entityStreamLocatorProtocol,
                                 discoverEntityStreamLocatorProtocol,
                                 portfolioStreamLocatorProtocol,
                                 discoverPortfolioStreamLocatorProtocol,
                                 getEntityMemberShipViewModelByKeyProtocol,
                                 portfolioViewModelProjectorProtocol,
                             };

                return result;
            }
        }
    }

    public class ConfigGet<T> : IProtocol<GetLatest<T>, T>
        where T : class
    {
        /// <inheritdoc />
        public void Execute(
            GetLatest<T> operation)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public TReturn Execute<TReturn>(
            GetLatest<T> operation)
        {
            if (typeof(T) != typeof(TReturn))
            {
                throw new ArgumentException("Type mismatch T and TReturn");
            }

            return Config.Get<TReturn>();
        }
    }

    public class SqlProtocol<TKey, T> : IProtocol<GetByKey<TKey, T>>
        where TKey : class
        where T : class
    {
        private readonly IProtocol<Discover<TKey, PortfolioStreamLocator>, PortfolioStreamLocator> streamLocatorProtocol;

        public SqlProtocol(
             IProtocol<Discover<TKey, PortfolioStreamLocator>, PortfolioStreamLocator> streamLocatorProtocol)
        {
            this.streamLocatorProtocol = streamLocatorProtocol;
        }

        /// <inheritdoc />
        public void Execute(
            GetByKey<TKey, T> operation)
        {
            var streamLocator = this.streamLocatorProtocol.Execute<PortfolioStreamLocator>(new Discover<TKey, PortfolioStreamLocator>(operation.Key));
            throw new NotImplementedException();
        }
    }

    public class PortfolioViewModelProjector : IProtocol<Handle<Created<EntityMembershipViewModel>>>
    {
        private readonly PortfolioProtocolComposer composer;

        public PortfolioViewModelProjector(
            PortfolioProtocolComposer composer)
        {
            this.composer = composer ?? throw new ArgumentNullException(nameof(composer));
        }

        /// <inheritdoc />
        public void Execute(
            Handle<Created<EntityMembershipViewModel>> operation)
        {
            var entityMembershipViewModel = this.composer.Execute<GetLatest<EntityMembershipViewModel>, EntityMembershipViewModel>(new GetLatest<EntityMembershipViewModel>());
            var portfolioStrategiesImpacted =
                this.composer.Execute<Discover<EntityMembershipViewModel, PortfolioStrategiesImpacted>, PortfolioStrategiesImpacted>(
                    new Discover<EntityMembershipViewModel, PortfolioStrategiesImpacted>(entityMembershipViewModel));

            foreach (var impacted in portfolioStrategiesImpacted.Descriptions)
            {
                var portfolioViewModel = new PortfolioViewModel(impacted, entityMembershipViewModel.Entities);
                this.composer.Execute(new Put<PortfolioViewModel>(portfolioViewModel));
            }
        }
    }

    public class PortfolioStrategiesImpacted {
        public IReadOnlyCollection<PortfolioDescriptionViewModel> Descriptions { get; set; }
    }
}
