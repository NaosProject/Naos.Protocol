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

    public class EntityProtocolComposer : ProtocolComposerBase,
                                          IComposeProtocol<GetLatest<EntityStreamLocator>>,
                                          IComposeProtocol<DetermineStreamLocatorByKey<string, EntityStreamLocator>>,
                                          IComposeProtocol<GetLatest<EntityViewModel>>,
                                          IComposeProtocol<GetLatest<EntityDescriptionViewModel>>,
                                          IComposeProtocol<GetLatest<EntityMembershipViewModel>>
    {
        private readonly SqlProtocol<string, EntityViewModel, EntityStreamLocator> viewModelDataProtocol;
        private readonly SqlProtocol<string, EntityMembershipViewModel, EntityStreamLocator> viewModelMembershipDataProtocol;
        private readonly SqlProtocol<string, EntityDescriptionViewModel, EntityStreamLocator> viewModelDescriptionDataProtocol;

        public EntityProtocolComposer()
        {
            this.viewModelDataProtocol = new SqlProtocol<string, EntityViewModel, EntityStreamLocator>(
                this.ReCompose<DetermineStreamLocatorByKey<string, EntityStreamLocator>, EntityStreamLocator>());
            this.viewModelMembershipDataProtocol = new SqlProtocol<string, EntityMembershipViewModel, EntityStreamLocator>(
                this.ReCompose<DetermineStreamLocatorByKey<string, EntityStreamLocator>, EntityStreamLocator>());
            this.viewModelDescriptionDataProtocol = new SqlProtocol<string, EntityDescriptionViewModel, EntityStreamLocator>(
                this.ReCompose<DetermineStreamLocatorByKey<string, EntityStreamLocator>, EntityStreamLocator>());
        }

        /// <inheritdoc />
        IProtocol<GetLatest<EntityStreamLocator>> IComposeProtocol<GetLatest<EntityStreamLocator>>.Compose()
        {
            return new ConfigGet<EntityStreamLocator>();
        }

        /// <inheritdoc />
        IProtocol<GetLatest<EntityViewModel>> IComposeProtocol<GetLatest<EntityViewModel>>.Compose()
        {
            return this.viewModelDataProtocol;
        }

        /// <inheritdoc />
        IProtocol<GetLatest<EntityDescriptionViewModel>> IComposeProtocol<GetLatest<EntityDescriptionViewModel>>.Compose()
        {
            return this.viewModelDescriptionDataProtocol;
        }

        /// <inheritdoc />
        IProtocol<GetLatest<EntityMembershipViewModel>> IComposeProtocol<GetLatest<EntityMembershipViewModel>>.Compose()
        {
            return this.viewModelMembershipDataProtocol;
        }

        /// <inheritdoc />
        public IProtocol<DetermineStreamLocatorByKey<string, EntityStreamLocator>> Compose()
        {
            var returnProtocol = this.ReCompose<GetLatest<EntityStreamLocator>, EntityStreamLocator>();
            var seededDetermination = new SeededDetermineStreamLocator<string, EntityStreamLocator>(
                returnProtocol);
            return seededDetermination;
        }
    }
}
