// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ProxyBuilderTest.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Protocol.Domain.Test
{
    using Naos.Serialization.Json;
    using Newtonsoft.Json;
    using Xunit;
    using Xunit.Abstractions;

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

        [Fact(Skip = "Need to figure out how to deal with this.")]
        public void TestSerialization()
        {
            // var serializer = new NaosJsonSerializer();
            var gateIdKey = "gateId";
            var input = new OperationPrototype(
                "Hello",
                _ => new CloseGate(_.Get<string>(new LockerKey(gateIdKey))));

            var json = JsonConvert.SerializeObject(input);
            this.testOutputHelper.WriteLine(json);
        }
    }
}
