// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LockerKey.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Protocol.Domain
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Naos.Serialization.Domain;
    using OBeautifulCode.Validation.Recipes;

    /// <summary>
    /// Key to get saved output.
    /// </summary>
    public class LockerKey : ReadOperationBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LockerKey"/> class.
        /// </summary>
        /// <param name="keyId">Key id.</param>
        public LockerKey(string keyId)
        {
            new { keyId }.Must().NotBeNullNorWhiteSpace();

            this.KeyId = keyId;
        }

        /// <summary>
        /// Gets the key id.
        /// </summary>
        public string KeyId { get; private set; }
    }

    /// <summary>
    /// Response object from the locker.
    /// </summary>
    public class LockerContents
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LockerContents"/> class.
        /// </summary>
        /// <param name="contents">Contents of the locker.</param>
        public LockerContents(DescribedSerialization contents)
        {
            new { contents }.Must().NotBeNull();

            this.Contents = contents;
        }

        /// <summary>
        /// Gets the contents.
        /// </summary>
        public DescribedSerialization Contents { get; private set; }
    }

    /// <summary>
    /// Friendly interface for the handler.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1040:AvoidEmptyInterfaces", Justification = "Prefer an interface.")]
    public interface ILocker : IHandleOperations<LockerKey, DescribedSerialization>
    {
    }

    /// <summary>
    /// Container of <see cref="DescribedSerialization" /> keyed by a <see cref="LockerKey" />.
    /// </summary>
    public class Locker : ILocker
    {
        private readonly IReadOnlyDictionary<LockerKey, DescribedSerialization> keyToContentsMap;

        /// <summary>
        /// Initializes a new instance of the <see cref="Locker"/> class.
        /// </summary>
        /// <param name="keyToContentsMap">Key to contents map.</param>
        public Locker(IReadOnlyDictionary<LockerKey, DescribedSerialization> keyToContentsMap)
        {
            new { keyToContentsMap }.Must().NotBeNull();

            this.keyToContentsMap = keyToContentsMap;
        }

        /// <inheritdoc />
        public async Task<DescribedSerialization> HandleAsync(LockerKey operation)
        {
            return await Task.FromResult(this.keyToContentsMap[operation]);
        }
    }
}