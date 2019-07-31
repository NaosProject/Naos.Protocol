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

    public interface INeedSpecificEntityProtocolCompoosition :
        IComposeProtocol<GetLatest<EntityStreamLocator>>,
        IComposeProtocol<DetermineLocatorByKey<string, EntityStreamLocator>>,
        IComposeProtocol<GetLatest<EntityViewModel>>
    { }

    public class EntityProtocolComposer : ProtocolComposerBase,
                                          IComposeProtocol<GetLatest<EntityStreamLocator>>,
                                          IComposeProtocol<DetermineLocatorByKey<string, EntityStreamLocator>>,
                                          IComposeProtocol<GetLatest<EntityViewModel>>,
                                          IComposeProtocol<GetLatest<EntityDescriptionViewModel>>,
                                          IComposeProtocol<GetLatest<EntityMembershipViewModel>>
    {
        /// <inheritdoc />
        IProtocol<GetLatest<EntityStreamLocator>> IComposeProtocol<GetLatest<EntityStreamLocator>>.Compose()
        {
            return this.ReCompose<GetLatest<EntityStreamLocator>>();
        }

        /// <inheritdoc />
        IProtocol<GetLatest<EntityViewModel>> IComposeProtocol<GetLatest<EntityViewModel>>.Compose()
        {
            return this.ReCompose<GetLatest<EntityViewModel>>();
        }

        /// <inheritdoc />
        IProtocol<GetLatest<EntityDescriptionViewModel>> IComposeProtocol<GetLatest<EntityDescriptionViewModel>>.Compose()
        {
            return this.ReCompose<GetLatest<EntityDescriptionViewModel>>();
        }

        /// <inheritdoc />
        IProtocol<GetLatest<EntityMembershipViewModel>> IComposeProtocol<GetLatest<EntityMembershipViewModel>>.Compose()
        {
            return this.ReCompose<GetLatest<EntityMembershipViewModel>>();
        }

        /// <inheritdoc />
        public IProtocol<DetermineLocatorByKey<string, EntityStreamLocator>> Compose()
        {
            var returnProtocol = this.ReCompose<GetLatest<EntityStreamLocator>>();
            var seededDetermination = new SeededDetermineLocator<string, EntityStreamLocator>(returnProtocol);
            return seededDetermination;
        }
    }
}
