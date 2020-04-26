// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SqlInputParameterRepresentation{TValue}.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Protocol.SqlServer
{
    using System;
    using OBeautifulCode.Assertion.Recipes;
    using OBeautifulCode.Type.Recipes;

    /// <summary>
    /// Top level .
    /// </summary>
    /// <typeparam name="TValue">Type of the input value.</typeparam>
    public class SqlInputParameterRepresentation<TValue> : SqlParameterRepresentationBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SqlInputParameterRepresentation{TValue}"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="type">The type.</param>
        /// <param name="value">The value.</param>
        public SqlInputParameterRepresentation(
            string name,
            SqlDataTypeRepresentationBase type,
            TValue value)
            : base(name, type)
        {
            type.MustForTest(nameof(type)).NotBeNull();

            var valueType = typeof(TValue);
            type.ValidateObjectTypeIsCompatible(valueType);

            this.Value = value;
        }

        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <value>The value.</value>
        public TValue Value { get; private set; }
    }
}
