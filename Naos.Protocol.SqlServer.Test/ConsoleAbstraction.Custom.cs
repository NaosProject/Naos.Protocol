// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ConsoleAbstraction.Custom.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Protocol.SqlServer.Test
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Text;
    using System.Threading.Tasks;
    using CLAP;
    using FakeItEasy;
    using Naos.Logging.Domain;
    using Naos.Protocol.Domain;
    using Naos.Protocol.Serialization.Bson;
    using OBeautifulCode.AutoFakeItEasy;
    using OBeautifulCode.Compression.Recipes;
    using OBeautifulCode.Representation.System;
    using OBeautifulCode.Serialization;
    using OBeautifulCode.Serialization.Bson;
    using OBeautifulCode.Serialization.Recipes;

    /// <summary>
    /// Default implementation of the ConsoleAbstraction layer.
    /// </summary>
    public partial class ConsoleAbstraction
    {
        [Verb(Aliases = "Create", IsDefault = false, Description = "Creates a stream")]
        public static void CreateStream(
            [Aliases("name")] string streamName,
            [Aliases("kind")] SerializationKind serializationKind,
            [Aliases("format")] SerializationFormat serializationFormat,
            [Aliases("server")] string serverName,
            [Aliases("db")] string databaseName,
            [Aliases("instance")] string instanceName,
            [Aliases("user")] string userName,
            [Aliases("pass")] string password,
            [Aliases("")][DefaultValue(false)] bool debug)
        {
            CommonSetup(debug);
            var stream = GetSqlStream(streamName, serializationKind, serializationFormat, serverName, databaseName, instanceName, userName, password);

            var stopwatch = new Stopwatch();
            stopwatch.Reset();
            stopwatch.Start();
            stream.Execute(new CreateStreamOp<string>(stream, ExistingStreamEncounteredStrategy.Skip));
            stopwatch.Stop();
            Console.WriteLine(FormattableString.Invariant($"Create - {streamName} took {stopwatch.Elapsed.TotalMilliseconds}ms."));
        }

        [Verb(Aliases = "Put", IsDefault = false, Description = "Puts a dummy in the stream")]
        public static void PutDummy(
            [Aliases("name")] string streamName,
            [Aliases("kind")] SerializationKind serializationKind,
            [Aliases("format")] SerializationFormat serializationFormat,
            [Aliases("server")] string serverName,
            [Aliases("db")] string databaseName,
            [Aliases("instance")] string instanceName,
            [Aliases("user")] string userName,
            [Aliases("pass")] string password,
            [Aliases("count")] int dummyCount,
            [Aliases("")][DefaultValue(false)] bool debug)
        {
            CommonSetup(debug, logWritingSettings: new LogWritingSettings());

            var stream = GetSqlStream(streamName, serializationKind, serializationFormat, serverName, databaseName, instanceName, userName, password);
            var stopwatch = new Stopwatch();
            for (var idx = 0;
                idx < dummyCount;
                idx++)
            {
                var id = Guid.NewGuid().ToString().ToUpperInvariant();
                var field = BuildGarbageStringOfLength(10);
                var filler = BuildGarbageStringOfLength(A.Dummy<int>().ThatIsInRange(1000, 18000));
                stopwatch.Reset();
                stopwatch.Start();
                stream.BuildPutProtocol<TestObject>()
                      .Execute(
                           new PutOp<TestObject>(
                               new TestObject(
                                   id,
                                   field,
                                   filler)));
                stopwatch.Stop();
                Console.WriteLine(FormattableString.Invariant($"Put - {id} took {stopwatch.Elapsed.TotalMilliseconds}ms - size field: {field.Length}, size filler: {filler.Length}."));
            }
        }

        [Verb(Aliases = "Get", IsDefault = false, Description = "Gets a dummy from the stream")]
        public static void GetDummy(
            [Aliases("name")] string streamName,
            [Aliases("kind")] SerializationKind serializationKind,
            [Aliases("format")] SerializationFormat serializationFormat,
            [Aliases("server")] string serverName,
            [Aliases("db")] string databaseName,
            [Aliases("instance")] string instanceName,
            [Aliases("user")] string userName,
            [Aliases("pass")] string password,
            [Aliases("")] string id,
            [Aliases("")] [DefaultValue(false)] bool debug)
        {
            CommonSetup(debug, logWritingSettings: new LogWritingSettings());

            var stream = GetSqlStream(streamName, serializationKind, serializationFormat, serverName, databaseName, instanceName, userName, password);

            for (var idx = 0;
                idx < 5;
                idx++)
            {
                var stopwatch = new Stopwatch();
                stopwatch.Reset();
                stopwatch.Start();
                var result = stream.BuildGetLatestByIdAndTypeProtocol<TestObject>().Execute(new GetLatestByIdAndTypeOp<string, TestObject>(id));
                stopwatch.Stop();
                Console.WriteLine(
                    FormattableString.Invariant(
                        $"Get - {id} took {stopwatch.Elapsed.TotalMilliseconds}ms - size field: {result.Field.Length}, size filler: {result.Filler.Length}."));
            }
        }

        private static string BuildGarbageStringOfLength(
            int length)
        {
            // creating a StringBuilder object()
            StringBuilder result = new StringBuilder();
            Random random = new Random();

            for (var idx = 0;
                idx < length;
                idx++)
            {
                var flt = random.NextDouble();
                var shift = Convert.ToInt32(Math.Floor(25 * flt));
                var letter = Convert.ToChar(shift + 65);
                result.Append(letter);
            }

            return result.ToString();
        }

        private static SqlStream<string> GetSqlStream(
            string streamName,
            SerializationKind serializationKind,
            SerializationFormat serializationFormat,
            string serverName,
            string databaseName,
            string instanceName,
            string userName,
            string password)
        {
            var sqlStreamLocator = new SqlStreamLocator(serverName, databaseName, userName, password, instanceName);
            var streamLocatorProtocol = new SingleStreamLocatorProtocol<string>(sqlStreamLocator);

            var configurationTypeRepresentation = typeof(ProtocolBsonSerializationConfiguration)
                // typeof(GenericDependencyConfiguration<GenericDiscoveryBsonConfiguration<MyObject>, ProtocolBsonSerializationConfiguration>)
               .ToRepresentation();
            var defaultSerializerDescription = new SerializationDescription(
                serializationKind,
                serializationFormat,
                configurationTypeRepresentation);

            var tagExtractor = new LambdaReturningProtocol<GetTagsFromObjectOp<TestObject>, IReadOnlyDictionary<string, string>>(
                _ => new Dictionary<string, string>
                     {
                         { nameof(TestObject.Field), _.ObjectToDetermineTagsFrom.Field },
                     });

            var stream = new SqlStream<string>(
                streamName,
                TimeSpan.FromMinutes(20),
                TimeSpan.FromMinutes(20),
                defaultSerializerDescription,
                SerializerFactory.Instance,
                CompressorFactory.Instance,
                streamLocatorProtocol,
                new Dictionary<Type, IProtocol>(),
                new Dictionary<Type, IProtocol>
                {
                    { typeof(TestObject), tagExtractor },
                });
            return stream;
        }
    }

    public class TestObject : IHaveId<string>
    {
        public string Id { get; private set; }

        public string Field { get; private set; }

        public string Filler { get; private set; }

        public TestObject(
            string id,
            string field,
            string filler)
        {
            this.Id = id;
            this.Field = field;
            this.Filler = filler;
        }
    }
}
