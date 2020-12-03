// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IHaveDetails.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Protocol.Domain
{
    using System.Collections.Generic;
    using OBeautifulCode.Type;

    /// <summary>
    /// Interface to declare having details or context on some action or event.
    /// </summary>
    public interface IHaveDetails
    {
        /// <summary>
        /// Gets the details.
        /// </summary>
        /// <value>The details.</value>
        string Details { get; }
    }
}