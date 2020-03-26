// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GetLatestOp.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Protocol.Domain
{
    /// <summary>
    /// Operation to get the most recent object of a certain type.
    /// </summary>
    /// <typeparam name="TObject">The type of the t object.</typeparam>
    /// <seealso cref="Naos.Protocol.Domain.ReturningOperationBase{TObject}" />
    public class GetLatestOp<TObject> : ReturningOperationBase<TObject>
    {
    }
}
