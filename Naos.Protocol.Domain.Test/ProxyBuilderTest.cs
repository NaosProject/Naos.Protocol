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
    using OBeautifulCode.Type;
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
            var allMethodsInfos = typeof(ILockerOpener).GetAllMethodInfos();
            var memberInfo = allMethodsInfos.First(_ =>
            {
                return _.ToString()
                        .Contains(
                            "TSpecificReturn Handle[TSpecificReturn](Naos.Protocol.Domain.LockerKey)");
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

            var sequence = new DispatchedOperationSequence(
                new[]
                {
                    OperationPrototype.Build(
                        "Create",
                        _ => new CreateGate(_.Handle<string>(new LockerKey("gateCreationOutput"))),
                        new LockerKey("gateCreationOutput")),
                    OperationPrototype.Build(
                        "Open",
                        _ => new OpenGate(_.Handle<string>(new LockerKey("gateCreationOutput")))),
                    OperationPrototype.Build(
                        "Close",
                        _ => new CloseGate(_.Handle<string>(new LockerKey("gateCreationOutput")))),
                },
                new Channel("channel"));

            var json = serializer.SerializeToString(sequence);
            this.testOutputHelper.WriteLine(json);
            var actual = serializer.Deserialize<DispatchedOperationSequence>(json);
            actual.Should().NotBeNull();

            var serializationDescription = new SerializationDescription(
                SerializationKind.Json,
                SerializationFormat.String,
                typeof(ProtocolJsonConfiguration).ToTypeDescription());

            var keyToContentsMap = new Dictionary<LockerKey, DescribedSerialization>
            {
                {
                    new LockerKey("gateCreationOutput"),
                    "monkey".ToDescribedSerializationUsingSpecificSerializer(
                        serializationDescription,
                        serializer)
                },
            };

            var lockerOpener = new LockerOpener(
                keyToContentsMap,
                JsonSerializerFactory.Instance);

            var realLambdas = actual.OperationPrototypes.Select(_ => _.Builder.FromDescription()).ToList();
            var realObjects = realLambdas.Select(expression =>
            {
                var compiled = expression.Compile();
                return compiled.DynamicInvoke(lockerOpener);
            }).ToList();

            var operations = realObjects.Cast<OperationBase>().ToList();
            operations[0].Should().BeOfType<CreateGate>();
            ((CreateGate)operations[0]).GateId.Should().Be("monkey");
            operations[1].Should().BeOfType<OpenGate>();
            ((OpenGate)operations[1]).GateId.Should().Be("monkey");
            operations[2].Should().BeOfType<CloseGate>();
            ((CloseGate)operations[2]).GateId.Should().Be("monkey");
        }
    }
}
