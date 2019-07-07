// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ProxyBuilderTest.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Protocol.Domain.Test
{
    using System;
    using System.Linq.Expressions;
    using FluentAssertions;
    using Naos.Protocol.Serialization.Json;
    using Naos.Serialization.Domain;
    using Naos.Serialization.Json;
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

        [Fact ]
        //[Fact(Skip = "Need to figure out how to deal with this.")]
        public void TestSerialization()
        {
            var serializer = new NaosJsonSerializer(typeof(ProtocolJsonConfiguration), UnregisteredTypeEncounteredStrategy.Attempt);
            var gateIdKey = "gateId";

            Expression<Func<ILocker, OperationBase>> operationBuilder = _ => new CloseGate(_.HandleWithSpecificReturn<string>(new LockerKey(gateIdKey)));
            var sequence = new DispatchedOperationSequence(
                new[]
                {
                    OperationPrototype.Build(
                        "Hello",
                        operationBuilder),
                },
                new Channel("channel"));

            var json = serializer.SerializeToString(sequence);
            this.testOutputHelper.WriteLine(json);
            var actual = serializer.Deserialize<DispatchedOperationSequence>(json);
            actual.Should().NotBeNull();
        }
    }
}
