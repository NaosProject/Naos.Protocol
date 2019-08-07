using FakeItEasy;
using FluentAssertions;

namespace Naos.Protocol.Domain.Test.Portfolio.Domain.Test.Protocols
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Xunit;

    public static class DiscoverPortfolioStrategiesImpactedByEntityMembershipViewModelChangesProtocolTest
    {
        [Fact]
        public static void Test()
        {
            // Arrange
            var getLatestPortfolioDescriptionViewModelOp = new GetLatestOp<PortfolioDescriptionViewModel>
            {
            };

            var portfolioDescriptionViewModel = new PortfolioDescriptionViewModel
            {
            };

            var portfolioProtocol = A.Fake<IReturningProtocol<GetLatestOp<PortfolioDescriptionViewModel>, PortfolioDescriptionViewModel>>();
            A.CallTo(() => portfolioProtocol.Execute(getLatestPortfolioDescriptionViewModelOp)).Returns(portfolioDescriptionViewModel);

            var getLatestEntityMembershipViewModelOp = new GetLatestOp<EntityMembershipViewModel>
            {
            };

            var entityMembershipViewModel = new EntityMembershipViewModel
            {
            };

            var entityProtocol = A.Fake<IReturningProtocol<GetLatestOp<EntityMembershipViewModel>, EntityMembershipViewModel>>();
            A.CallTo(() => entityProtocol.Execute(getLatestEntityMembershipViewModelOp)).Returns(entityMembershipViewModel);

            var systemUnderTest = new DiscoverPortfolioStrategiesImpactedByEntityMembershipViewModelChangesProtocol(portfolioProtocol, entityProtocol);

            var operation = new DiscoverPortfolioStrategiesImpactedByEntityMembershipViewModelChangesOp
            {
            };

            var expected = new PortfolioStrategiesImpacted()
            {
            };

            // Act
            var actual = systemUnderTest.Execute(operation);

            // Assert
            actual.Should().Be(expected);
        }

        [Fact]
        public static void Test2()
        {
            // Arrange
            var porfolioStreamComposer = new MemoryComposer<PortfolioDescriptionViewModel>(new PortfolioDescriptionViewModel("badName"));
            var entityStreamComposer = new MemoryComposer<EntityMembershipViewModel>(new EntityMembershipViewModel());
            var composer = new PortfolioProtocolComposer().LoadRequiredProtocols(porfolioStreamComposer, entityStreamComposer);
            var systemUnderTest = new DiscoverPortfolioStrategiesImpactedByEntityMembershipViewModelChangesProtocol(composer, composer);

            var operation = new DiscoverPortfolioStrategiesImpactedByEntityMembershipViewModelChangesOp
            {
            };

            var expected = new PortfolioStrategiesImpacted()
            {
            };

            // Act
            var actual = systemUnderTest.Execute(operation);

            // Assert
            actual.Should().Be(expected);
        }
    }

    public class MemoryComposer<T> : ProtocolComposerBase,
        IGetReturningProtocol<GetLatestOp<T>, T>
    {
        private T[] models;

        public MemoryComposer(params T[] models)
        {
            this.models = models;
        }

        public IReturningProtocol<GetLatestOp<T>, T> Get()
        {
            return new MemoryProtocol<T>(this.models);
        }
    }

    public class MemoryProtocol<T> : IReturningProtocol<GetLatestOp<T>, T>
    {
        private T[] models;

        public MemoryProtocol(T[] models)
        {
            this.models = models;
        }

        public T Execute(GetLatestOp<T> operation)
        {
            return this.models.Last();
        }
    }
}
