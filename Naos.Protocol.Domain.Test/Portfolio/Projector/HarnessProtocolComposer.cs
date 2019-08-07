// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HarnessProtocolComposer.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Protocol.Domain.Test
{
    using System;
    using System.Collections.Generic;

    public class HarnessProtocolComposer : ProtocolComposerBase,
                                           IGetVoidProtocol<HandleEventOp<EntityMembershipViewModelUpdated>>,
                                           IGetReturningProtocol<GetLatestOp<PortfolioStreamLocator>, PortfolioStreamLocator>
    {
        /// <inheritdoc />
        public override IReadOnlyCollection<Type> DependentComposerTypes => new[]
                                                                            {
                                                                                typeof(PortfolioProtocolComposer),
                                                                                // typeof(EntitySqlProtocolComposer),
                                                                                typeof(SqlProtocolComposer<string, PortfolioViewModel, PortfolioStreamLocator>),
                                                                                typeof(SqlProtocolComposer<string, PortfolioDescriptionViewModel, PortfolioStreamLocator>),
                                                                            };

        /// <inheritdoc />
        public IVoidProtocol<HandleEventOp<EntityMembershipViewModelUpdated>> Get()
        {
            return new HandleEventEntityMembershipViewModelUpdated(this.GetDependentComposer<PortfolioProtocolComposer>());
        }

        /// <inheritdoc />
        IReturningProtocol<GetLatestOp<PortfolioStreamLocator>, PortfolioStreamLocator>
            IGetReturningProtocol<GetLatestOp<PortfolioStreamLocator>, PortfolioStreamLocator>.Get()
        {
            return new ConfigGet<PortfolioStreamLocator>();
        }
    }
}
