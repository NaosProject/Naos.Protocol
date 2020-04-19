// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LambdaGetTagsFromObjectProtocol{TObject}.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Protocol.Domain
{
    using System;
    using System.Collections.Generic;
    using OBeautifulCode.Assertion.Recipes;

    /// <summary>
    /// Event container.
    /// </summary>
    /// <typeparam name="TObject">Type of object to inspect.</typeparam>
    public class LambdaGetTagsFromObjectProtocol<TObject> : IReturningProtocol<GetTagsFromObjectOp<TObject>, IReadOnlyDictionary<string, string>>
    {
        private readonly Func<TObject, IReadOnlyDictionary<string, string>> lambda;

        /// <summary>
        /// Initializes a new instance of the <see cref="LambdaGetTagsFromObjectProtocol{TObject}"/> class.
        /// </summary>
        /// <param name="lambda">The lambda to extract tags.</param>
        public LambdaGetTagsFromObjectProtocol(
            Func<TObject, IReadOnlyDictionary<string, string>> lambda)
        {
            lambda.MustForArg(nameof(lambda)).NotBeNull();
            this.lambda = lambda;
        }

        /// <inheritdoc />
        public IReadOnlyDictionary<string, string> Execute(
            GetTagsFromObjectOp<TObject> operation)
        {
            var result = this.lambda(operation.ObjectToDetermineKeyFrom);
            return result;
        }
    }
}
