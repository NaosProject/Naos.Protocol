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
        IComposeProtocol<GetLatest<EntityStreamLocator>>,
        IComposeProtocol<DetermineLocatorByKey<string, EntityStreamLocator>>,
        IComposeProtocol<GetLatest<EntityViewModel>>
    { }

    public class EntityProtocolComposer : ProtocolComposerBase,
                                          IRequireProtocolWithReturn<GetLatest<EntityStreamLocator>, EntityStreamLocator>,
                                          IComposeProtocolWithReturn<DetermineLocatorByKey<string, EntityStreamLocator>, EntityStreamLocator>,
                                          IComposeProtocolWithReturn<GetLatest<EntityViewModel>, EntityViewModel>,
                                          IComposeProtocolWithReturn<GetLatest<EntityDescriptionViewModel>, EntityDescriptionViewModel>,
                                          IComposeProtocolWithReturn<GetLatest<EntityMembershipViewModel>, EntityMembershipViewModel>
    {
        /// <inheritdoc />
        IProtocolWithReturn<GetLatest<EntityStreamLocator>, EntityStreamLocator>
            IComposeProtocolWithReturn<GetLatest<EntityStreamLocator>, EntityStreamLocator>.Compose()
        {
            var result = this.ReComposeWithReturn<GetLatest<EntityStreamLocator>, EntityStreamLocator>();
            return result;
        }

        /// <inheritdoc />
        IProtocolWithReturn<DetermineLocatorByKey<string, EntityStreamLocator>, EntityStreamLocator>
            IComposeProtocolWithReturn<DetermineLocatorByKey<string, EntityStreamLocator>, EntityStreamLocator>.Compose()
        {
            var returnProtocol      = this.ReComposeWithReturn<GetLatest<EntityStreamLocator>, EntityStreamLocator>();
            var seededDetermination = new SeededDetermineLocator<string, EntityStreamLocator>(returnProtocol);
            return seededDetermination;
        }

        /// <inheritdoc />
        IProtocolWithReturn<GetLatest<EntityViewModel>, EntityViewModel> IComposeProtocolWithReturn<GetLatest<EntityViewModel>, EntityViewModel>.
            Compose()
        {
            var result = this.ReComposeWithReturn<GetLatest<EntityViewModel>, EntityViewModel>();
            return result;
        }

        /// <inheritdoc />
        IProtocolWithReturn<GetLatest<EntityDescriptionViewModel>, EntityDescriptionViewModel>
            IComposeProtocolWithReturn<GetLatest<EntityDescriptionViewModel>, EntityDescriptionViewModel>.Compose()
        {
            var result = this.ReComposeWithReturn<GetLatest<EntityDescriptionViewModel>, EntityDescriptionViewModel>();
            return result;
        }

        /// <inheritdoc />
        IProtocolWithReturn<GetLatest<EntityMembershipViewModel>, EntityMembershipViewModel>
            IComposeProtocolWithReturn<GetLatest<EntityMembershipViewModel>, EntityMembershipViewModel>.Compose()
        {
            throw new NotImplementedException();
        }
    }
}
