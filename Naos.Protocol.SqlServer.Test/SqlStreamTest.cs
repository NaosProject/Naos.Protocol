// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SqlStreamTest.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Protocol.SqlServer.Test
{
    using System;
    using Naos.Protocol.Domain;
    using OBeautifulCode.Assertion.Recipes;

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
            var stream = new SqlStream<string>("Second", TimeSpan.FromMinutes(20), sqlDriver, sqlDriver, sqlDriver);

            stream.Execute(new CreateStreamOp<string>(stream));
        }
    }
}
