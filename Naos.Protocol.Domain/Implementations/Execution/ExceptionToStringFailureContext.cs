// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ExceptionToStringFailureContext.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Protocol.Domain
{
    using System;
    using System.Collections.Generic;
    using Naos.CodeAnalysis.Recipes;
    using OBeautifulCode.Type;

    /// <summary>
    /// Basic failure context with the <see cref="Exception.ToString"/> of the failure.
    /// </summary>
    public partial class ExceptionToStringFailureContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ExceptionToStringFailureContext"/> class.
        /// </summary>
        /// <param name="exceptionToString">The string value of the exception.</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1720:IdentifiersShouldNotContainTypeNames", MessageId = "string", Justification = NaosSuppressBecause.CA1720_IdentifiersShouldNotContainTypeNames_TypeNameAddsClarityToIdentifierAndAlternativesDegradeClarity)]
        public ExceptionToStringFailureContext(
            string exceptionToString)
        {
            this.ExceptionToString = exceptionToString;
        }

        /// <summary>
        /// Gets the string value of the exception.
        /// </summary>
        /// <value>The string value of the exception.</value>
        public string ExceptionToString { get; private set; }
    }
}
