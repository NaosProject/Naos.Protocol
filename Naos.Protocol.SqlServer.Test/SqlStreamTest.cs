// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SqlStreamTest.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Protocol.SqlServer.Test
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using MongoDB.Bson.Serialization;
    using Naos.Protocol.Domain;
    using Naos.Protocol.Serialization.Bson;
    using OBeautifulCode.Assertion.Recipes;
    using OBeautifulCode.Compression.Recipes;
    using OBeautifulCode.Representation.System;
    using OBeautifulCode.Serialization;
    using OBeautifulCode.Serialization.Bson;
    using Xunit;
    using Xunit.Abstractions;

    /// <summary>
    /// Tests for <see cref="SqlStream{TKey}"/>.
    /// </summary>
    public partial class SqlStreamTest
    {
        private readonly ITestOutputHelper testOutputHelper;

        public SqlStreamTest(
            ITestOutputHelper testOutputHelper)
        {
            this.testOutputHelper = testOutputHelper;
        }

        [Fact(Skip = "Testing only.")]
        public void Method___Should_do_something___When_called()
        {
            var sqlStreamLocator = new SqlStreamLocator("localhost", "Streams", "sa", "password", "SQLDEV2017");
            var streamLocatorProtocol = new SingleStreamLocatorProtocol<string>(sqlStreamLocator);

            var configurationTypeRepresentation = typeof(ProtocolBsonSerializationConfiguration)
                // typeof(GenericDependencyConfiguration<GenericDiscoveryBsonConfiguration<MyObject>, ProtocolBsonSerializationConfiguration>)
                   .ToRepresentation();
            SerializationDescription defaultSerializerDescription = new SerializationDescription(
                SerializationKind.Bson,
                SerializationFormat.String,
                configurationTypeRepresentation);

            var tagExtractor = new LambdaReturningProtocol<GetTagsFromObjectOp<MyObject>, IReadOnlyDictionary<string, string>>(
                _ => new Dictionary<string, string>
                     {
                         { nameof(MyObject.Field), _.ObjectToDetermineKeyFrom.Field },
                     });

            var stream = new SqlStream<string>(
                "HMyStream",
                TimeSpan.FromMinutes(20),
                TimeSpan.FromMinutes(20),
                defaultSerializerDescription,
                BsonSerializerFactory.Instance,
                CompressorFactory.Instance,
                streamLocatorProtocol,
                new Dictionary<Type, IProtocol>(), 
                new Dictionary<Type, IProtocol>
                {
                    { typeof(MyObject), tagExtractor },
                });

            stream.Execute(new CreateStreamOp<string>(stream));
            var key = stream.Name;
            var firstValue = "Testing again.";
            var secondValue = "Testing again latest.";
            stream.BuildPutProtocol<MyObject>().Execute(new PutOp<MyObject>(new MyObject(key, firstValue)));
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            stream.BuildPutProtocol<MyObject>().Execute(new PutOp<MyObject>(new MyObject(key, secondValue)));
            stopwatch.Stop();
            this.testOutputHelper.WriteLine(FormattableString.Invariant($"Put: {stopwatch.Elapsed.TotalMilliseconds} ms"));
            stopwatch.Reset();
            stopwatch.Start();
            var my = stream.BuildGetLatestByKeyProtocol<MyObject>().Execute(new GetLatestByIdOp<string, MyObject>(key));
            this.testOutputHelper.WriteLine(FormattableString.Invariant($"Get: {stopwatch.Elapsed.TotalMilliseconds} ms"));
           // this.testOutputHelper.WriteLine(FormattableString.Invariant($"Get: {SqlStream<string>.Stopwatch.Elapsed.TotalMilliseconds} ms"));
            this.testOutputHelper.WriteLine(FormattableString.Invariant($"Key={my.Id}, Field={my.Field}"));
            my.Id.MustForTest().BeEqualTo(key);
        }
    }

    public class MyObject : IHaveKey<string>
    {
        public MyObject(
            string key,
            string field)
        {
            this.Id = key;
            this.Field = field;
        }

        public string Id { get; private set; }

        public string Field { get; private set; }
    }
}