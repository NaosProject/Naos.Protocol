// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EntityProtocolComposer.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Protocol.Domain.Test
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;

    public interface INeedSpecificEntityProtocolCompoosition :
        IGetProtocol<GetLatestOp<EntityStreamLocator>>,
        IGetProtocol<DetermineLocatorByKeyOp<string, EntityStreamLocator>>,
        IGetProtocol<GetLatestOp<EntityViewModel>>
    { }

    public class EntityProtocolComposer : ProtocolComposerBase,
                                          IRequireProtocolWithReturn<GetLatestOp<EntityStreamLocator>, EntityStreamLocator>,
                                          IGetReturningProtocol<DetermineLocatorByKeyOp<string, EntityStreamLocator>, EntityStreamLocator>,
                                          IGetReturningProtocol<GetLatestOp<EntityViewModel>, EntityViewModel>,
                                          IGetReturningProtocol<GetLatestOp<EntityDescriptionViewModel>, EntityDescriptionViewModel>,
                                          IGetReturningProtocol<GetLatestOp<EntityMembershipViewModel>, EntityMembershipViewModel>
    {
        /// <inheritdoc />
        IReturningProtocol<GetLatestOp<EntityStreamLocator>, EntityStreamLocator>
            IGetReturningProtocol<GetLatestOp<EntityStreamLocator>, EntityStreamLocator>.Get()
        {
            var result = this.DelegatedGet<GetLatestOp<EntityStreamLocator>, EntityStreamLocator>();
            return result;
        }

        /// <inheritdoc />
        IReturningProtocol<DetermineLocatorByKeyOp<string, EntityStreamLocator>, EntityStreamLocator>
            IGetReturningProtocol<DetermineLocatorByKeyOp<string, EntityStreamLocator>, EntityStreamLocator>.Get()
        {
            var returnProtocol      = this.DelegatedGet<GetLatestOp<EntityStreamLocator>, EntityStreamLocator>();
            var seededDetermination = new SeededDetermineLocator<string, EntityStreamLocator>(returnProtocol);
            return seededDetermination;
        }

        /// <inheritdoc />
        IReturningProtocol<GetLatestOp<EntityViewModel>, EntityViewModel> IGetReturningProtocol<GetLatestOp<EntityViewModel>, EntityViewModel>.
            Get()
        {
            var result = this.DelegatedGet<GetLatestOp<EntityViewModel>, EntityViewModel>();
            return result;
        }

        /// <inheritdoc />
        IReturningProtocol<GetLatestOp<EntityDescriptionViewModel>, EntityDescriptionViewModel>
            IGetReturningProtocol<GetLatestOp<EntityDescriptionViewModel>, EntityDescriptionViewModel>.Get()
        {
            var result = this.DelegatedGet<GetLatestOp<EntityDescriptionViewModel>, EntityDescriptionViewModel>();
            return result;
        }

        /// <inheritdoc />
        IReturningProtocol<GetLatestOp<EntityMembershipViewModel>, EntityMembershipViewModel>
            IGetReturningProtocol<GetLatestOp<EntityMembershipViewModel>, EntityMembershipViewModel>.Get()
        {
            throw new NotImplementedException();
        }
    }
}
