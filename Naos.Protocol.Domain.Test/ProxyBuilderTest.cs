// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ProxyBuilderTest.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Protocol.Domain.Test
{
    using Xunit;
    using Xunit.Abstractions;
    using static System.FormattableString;

    /// <summary>
    /// Proxy builder tests.
    /// </summary>
    public class ProxyBuilderTest
    {
        private readonly ITestOutputHelper testOutputHelper;

        public ProxyBuilderTest(ITestOutputHelper testOutputHelper)
        {
            this.testOutputHelper = testOutputHelper;
        }

        [Fact(Skip = "Need to port to proper syntax generation.")]
        public void BuildInteractionPatternFulfillmentToEntity()
        {
            var text = ProxyBuilder.BuildAllProxies(
                "GateManager",
                new[] { typeof(IOpenGate), typeof(ICloseGate) });

            this.testOutputHelper.WriteLine(text);
        }
    }
}
