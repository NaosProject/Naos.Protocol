// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CachingTests.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Protocol.Domain.Test
{
    using System;
    using System.Threading.Tasks;
    using OBeautifulCode.Assertion.Recipes;

    using Xunit;

    /// <summary>
    /// TODO: Starting point for new project.
    /// </summary>
    public static partial class CachingTests
    {
        [Fact]
        public static async Task MemoryCachingReturningProtocol_works()
        {
            // Arrange
            var backingCalled = 0L;
            var backingProtocolResult = new NullResourceLocator();
            var backingProtocol = new LambdaReturningProtocol<GetResourceLocatorByIdOp<string>, IResourceLocator>(
                op =>
                {
                    backingCalled = backingCalled + 1L;
                    return backingProtocolResult;
                });

            var cachingProtocol = new MemoryCachingReturningProtocol<GetResourceLocatorByIdOp<string>, IResourceLocator>(backingProtocol);

            // First Act/Assert
            var initialStatus = await cachingProtocol.ExecuteAsync(new GetCacheStatusOp());
            var firstStop = DateTime.UtcNow;

            initialStatus.Size.MustForTest().BeEqualTo(0L);

            // Second Act/Assert
            var secondResult = await cachingProtocol.ExecuteAsync(new GetResourceLocatorByIdOp<string>("1"));
            var secondStatus = await cachingProtocol.ExecuteAsync(new GetCacheStatusOp());
            var secondStop = DateTime.UtcNow;

            secondResult.MustForTest().NotBeNull().And().BeOfType<NullResourceLocator>();
            secondStatus.Size.MustForTest().BeEqualTo(1L);
            secondStatus.DateRangeOfCachedObjectsUtc.StartDateTimeInUtc.MustForTest().BeGreaterThanOrEqualTo(firstStop);
            secondStatus.DateRangeOfCachedObjectsUtc.EndDateTimeInUtc.MustForTest().BeLessThanOrEqualTo(secondStop);
            backingCalled.MustForTest().BeEqualTo(1L);

            // Third Act/Assert
            await cachingProtocol.ExecuteAsync(new ClearCacheOp());
            var thirdStatus = await cachingProtocol.ExecuteAsync(new GetCacheStatusOp());
            var thirdStop = DateTime.UtcNow;

            thirdStatus.Size.MustForTest().BeEqualTo(0L);
            thirdStatus.DateRangeOfCachedObjectsUtc.MustForTest().BeNull();
            backingCalled.MustForTest().BeEqualTo(1L);

            // Fourth Act/Assert
            var fourthResult = await cachingProtocol.ExecuteAsync(new GetResourceLocatorByIdOp<string>("2"));
            var fourthStatus = await cachingProtocol.ExecuteAsync(new GetCacheStatusOp());
            var fourthStop = DateTime.UtcNow;

            fourthResult.MustForTest().NotBeNull().And().BeOfType<NullResourceLocator>();
            fourthStatus.Size.MustForTest().BeEqualTo(1L);
            fourthStatus.DateRangeOfCachedObjectsUtc.StartDateTimeInUtc.MustForTest().BeGreaterThanOrEqualTo(thirdStop);
            secondStatus.DateRangeOfCachedObjectsUtc.EndDateTimeInUtc.MustForTest().BeLessThanOrEqualTo(fourthStop);
        }
    }
}
