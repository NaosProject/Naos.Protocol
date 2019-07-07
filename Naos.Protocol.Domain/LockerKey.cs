// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LockerKey.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Protocol.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Naos.Compression.Domain;
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
            if (string.IsNullOrWhiteSpace(keyId))
            {
                throw new ArgumentException("Cannot be null or whitespace.", nameof(keyId));
            }

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
            this.Contents = contents ?? throw new ArgumentNullException(nameof(contents));
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
        private readonly ISerializerFactory serializerFactory;
        private readonly ICompressorFactory compressorFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="Locker"/> class.
        /// </summary>
        /// <param name="keyToContentsMap">Key to contents map.</param>
        /// <param name="serializerFactory">Serializer factory for opening <see cref="DescribedSerialization" />.</param>
        /// <param name="compressorFactory">Compressor factory for opening <see cref="DescribedSerialization" />.</param>
        public Locker(IReadOnlyDictionary<LockerKey, DescribedSerialization> keyToContentsMap, ISerializerFactory serializerFactory, ICompressorFactory compressorFactory = null)
        {
            this.keyToContentsMap = keyToContentsMap ?? throw new ArgumentNullException(nameof(keyToContentsMap));
            this.serializerFactory = serializerFactory ?? throw new ArgumentNullException(nameof(serializerFactory));
            this.compressorFactory = compressorFactory ?? CompressorFactory.Instance;
        }

        /// <inheritdoc />
        public async Task<DescribedSerialization> HandleAsync(LockerKey operation)
        {
            return await Task.FromResult(this.keyToContentsMap[operation]);
        }

        /// <inheritdoc />
        public DescribedSerialization Handle(LockerKey operation)
        {
            throw new System.NotImplementedException();
        }

        /// <inheritdoc />
        public TSpecificReturn HandleWithSpecificReturn<TSpecificReturn>(LockerKey operation)
        {
            var entry = this.keyToContentsMap[operation];
            var result = entry.DeserializePayloadUsingSpecificFactory<TSpecificReturn>(this.serializerFactory, this.compressorFactory);
            return result;
        }
    }
}