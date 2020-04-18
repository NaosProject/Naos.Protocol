// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GetStreamLocatorByTypeOp.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Protocol.Domain
{
    using System;
    using Naos.Protocol.Domain.Internal;

    /// <summary>
    /// Operation to a <see cref="StreamLocatorBase"/> by a <see cref="Type"/>.
    /// </summary>
    public class GetStreamLocatorByTypeOp : ReturningOperationBase<StreamLocatorBase>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetStreamLocatorByTypeOp"/> class.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <exception cref="ArgumentNullException">type.</exception>
        public GetStreamLocatorByTypeOp(
            Type type)
        {
            this.Type = type ?? throw new ArgumentNullException(nameof(type));
        }

        /// <summary>
        /// Gets the type.
        /// </summary>
        /// <value>The type.</value>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1721:PropertyNamesShouldNotMatchGetMethods", Justification = "Spelling/name is correct.")]
        public Type Type { get; private set; }
    }
}
