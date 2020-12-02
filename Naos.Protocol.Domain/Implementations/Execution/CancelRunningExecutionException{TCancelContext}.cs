// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CancelRunningExecutionException{TCancelContext}.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Protocol.Domain
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// Exception to be thrown during execution that allows the protocol to communicate the difference between a failure and a self cancellation.
    /// </summary>
    /// <typeparam name="TCancelContext">The type of the cancel context.</typeparam>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1032:ImplementStandardExceptionConstructors", Justification = "Exception is for a specific usage, not serializing or marshaling.")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2240:ImplementISerializableCorrectly", Justification = "Exception is for a specific usage, not serializing or marshaling.")]
    [Serializable]
    public partial class SelfCancelRunningExecutionException<TCancelContext> : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SelfCancelRunningExecutionException{TCancelContext}"/> class.
        /// </summary>
        /// <param name="cancelContext">The cancel context.</param>
        public SelfCancelRunningExecutionException(
            TCancelContext cancelContext)
        {
            this.CancelContext = cancelContext;
        }

        /// <summary>
        /// Gets the cancel context.
        /// </summary>
        /// <value>The cancel context.</value>
        public TCancelContext CancelContext { get; private set; }
    }
}
