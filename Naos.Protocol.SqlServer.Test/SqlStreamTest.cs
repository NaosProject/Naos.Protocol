// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SqlStreamTest.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Protocol.SqlServer.Test
{
    using System;
    using MongoDB.Bson.Serialization;
    using Naos.Protocol.Domain;
    using Naos.Protocol.Serialization.Bson;
    using OBeautifulCode.Assertion.Recipes;
    using OBeautifulCode.Representation.System;
    using OBeautifulCode.Serialization;
    using OBeautifulCode.Serialization.Bson;
    using Xunit;

    /// <summary>
    /// Tests for <see cref="SqlStream{TKey}"/>.
    /// </summary>
    public static partial class SqlStreamTest
    {
        [Fact(Skip = "Just testing.")]
        public static void Method___Should_do_something___When_called()
        {
            var sqlStreamLocator = new SqlStreamLocator("localhost", "Streams", "sa", "J28k#aWOFW#MUdRn", "SQLDEV2017");
            var sqlDriver = new SqlSingleDriver(sqlStreamLocator);

            var configurationTypeRepresentation = typeof(ProtocolBsonSerializationConfiguration).ToRepresentation();
            SerializationDescription defaultSerializerDescription = new SerializationDescription(
                SerializationKind.Bson,
                SerializationFormat.String,
                configurationTypeRepresentation);

            var tagExtractor = new LambdaGetTagsFromObjectProtocol<MyObject>(
                _ => new Dictionary<string, string>
                     {
                         { nameof(MyObject.Field), _.Field },
                     });

            var stream = new SqlStream<string>(
                "Third",
                TimeSpan.FromMinutes(20),
                defaultSerializerDescription,
                BsonSerializerFactory.Instance,
                sqlDriver,
                sqlDriver,
                sqlDriver,
                new Dictionary<Type, IProtocol>
                {
                    { typeof(MyObject), tagExtractor },
                });

            stream.Execute(new CreateStreamOp<string>(stream));

            var payload = new Block("Testing.");
            stream.BuildPutProtocol<Block>().Execute(new PutOp<Block>(payload));
        }
    }

    public class MyObject : IHaveKey<string>
    {
        public MyObject(
            string key,
            string field)
        {
            this.Key = key;
            this.Field = field;
        }

        /// <inheritdoc />
        public string Key { get; private set; }

        public string Field { get; private set; }
    }
}