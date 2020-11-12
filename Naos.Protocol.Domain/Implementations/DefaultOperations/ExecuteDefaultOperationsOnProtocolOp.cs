// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ExecuteDefaultOperationsOnProtocolOp.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Protocol.Domain
{
    using static System.FormattableString;

    /// <summary>
    /// Executes the pre-configured default operations on a <see cref="IProtocol"/> that is also <see cref="IHaveDefaultOperations"/>.
    /// </summary>
    public partial class ExecuteDefaultOperationsOnProtocolOp : VoidOperationBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ExecuteDefaultOperationsOnProtocolOp"/> class.
        /// </summary>
        /// <param name="context"> The optional context around the default execution; this is useful if the operation is being persisted for auditing; DEFAULT is null.</param>
        public ExecuteDefaultOperationsOnProtocolOp(
            string context = null)
        {
            this.Context = context;
        }

        /// <summary>
        /// Gets the context.
        /// </summary>
        /// <value>The context.</value>
        public string Context { get; private set; }
    }
}
