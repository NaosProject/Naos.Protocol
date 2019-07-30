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

    public class PortfolioStreamLocator:StreamLocatorBase{ }

}
