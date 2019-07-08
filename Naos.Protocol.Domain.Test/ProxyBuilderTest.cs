// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ProxyBuilderTest.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Protocol.Domain.Test
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
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

        [Fact]
        public void RoundTripMemberInfoDescription()
        {
            var serializer = new NaosJsonSerializer(typeof(ProtocolJsonConfiguration), UnregisteredTypeEncounteredStrategy.Attempt);
            var allMethodsInfos = typeof(ILocker).GetAllMethodInfos();
            var memberInfo = allMethodsInfos.First(_ =>
            {
                return _.ToString()
                        .Contains(
                            "TSpecificReturn HandleWithSpecificReturn[TSpecificReturn](Naos.Protocol.Domain.LockerKey)");
            });
            memberInfo.Should().NotBeNull();

            var description = memberInfo.ToDescription();
            var serialized = serializer.SerializeToString(description);
            this.testOutputHelper.WriteLine(serialized);
            var deserialized = serializer.Deserialize<MethodInfoDescription>(serialized);
            var actual = deserialized.FromDescription();
        }

        [Fact ]
        //[Fact(Skip = "Need to figure out how to deal with this.")]
        public void TestSerialization()
        {
            var serializer = new NaosJsonSerializer(typeof(ProtocolJsonConfiguration), UnregisteredTypeEncounteredStrategy.Attempt);

            Expression<Func<ILocker, OperationBase>> operationBuilder = _ => new CloseGate(_.HandleWithSpecificReturn<string>(new LockerKey("gateId")));
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

            actual.OperationPrototypes.Single().OperationBuilder.FromDescription().Compile().DynamicInvoke(new Locker(
                new Dictionary<LockerKey, DescribedSerialization>
                {
                    {
                        new LockerKey("gateId"),
                        "monkey".ToDescribedSerializationUsingSpecificSerializer(new SerializationDescription(SerializationKind.Json, SerializationFormat.String), serializer)
                    },
                },
                JsonSerializerFactory.Instance));
        }
    }
}
