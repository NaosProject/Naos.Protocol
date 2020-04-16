// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SqlStream.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Protocol.SqlServer
{
    using System;
    using System.Collections.Generic;
    using Naos.Protocol.Domain;
    using Naos.Protocol.SqlServer.Internal;
    using OBeautifulCode.Assertion.Recipes;
    using OBeautifulCode.Type;

    /// <summary>
    /// SQL implementation of an <see cref="IStream" />.
    /// </summary>
    /// <typeparam name="TKey">Type of the key.</typeparam>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1711:IdentifiersShouldNotHaveIncorrectSuffix", Justification = NaosSuppressBecause.CA1711_IdentifiersShouldNotHaveIncorrectSuffix_TypeNameAddedAsSuffixForTestsWhereTypeIsPrimaryConcern)]
    public partial class SqlStream<TKey> : IStream, IModelViaCodeGen
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SqlStream{TKey}"/> class.
        /// </summary>
        /// <param name="name">The name of the stream.</param>
        /// <param name="servers">The servers in the fleet.</param>
        public SqlStream(
            string name,
            IReadOnlyCollection<SqlServerInstance> servers)
        {
            name.MustForArg(nameof(name)).NotBeNullNorWhiteSpace();
            servers.MustForArg(nameof(servers)).NotBeNullNorEmptyEnumerableNorContainAnyNulls();

            this.Name = name;
            this.Servers = servers;
        }

        /// <inheritdoc />
        public string Name { get; private set; }

        /// <summary>
        /// Gets the servers in the fleet.
        /// </summary>
        /// <value>The servers in the fleet.</value>
        public IReadOnlyCollection<SqlServerInstance> Servers { get; private set; }

        /// <summary>
        /// Gets the type of the key.
        /// </summary>
        /// <value>The type of the key.</value>
        public Type KeyType => typeof(TKey);
    }
}
