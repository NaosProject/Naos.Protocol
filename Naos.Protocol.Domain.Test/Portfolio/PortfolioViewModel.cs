// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Example.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Protocol.Domain.Test
{
    using System.Collections.Generic;

    public class PortfolioViewModel
    {
        public PortfolioViewModel(
            PortfolioDescriptionViewModel description,
            IReadOnlyCollection<EntityViewModel> entities)
        {
            throw new System.NotImplementedException();
        }
    }

    public class PortfolioDescriptionViewModel{}


    public class PortfolioStreamLocator { }

    public class EntityViewModel {}

    public class EntityMembershipViewModel
    {
        public IReadOnlyCollection<EntityViewModel> Entities { get; private set; }
    }

    public class EntityStreamLocator : StreamLocator { }

}
