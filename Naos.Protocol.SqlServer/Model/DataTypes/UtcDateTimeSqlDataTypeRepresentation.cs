﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UtcDateTimeSqlDataTypeRepresentation.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Protocol.SqlServer
{
    using System;
    using OBeautifulCode.Assertion.Recipes;

    /// <summary>
    /// Top level .
    /// </summary>
    public class UtcDateTimeSqlDataTypeRepresentation : SqlDataTypeRepresentationBase
    {
        /// <inheritdoc />
        public override string DeclarationInSqlSyntax => "[DATETIME2]";

        /// <inheritdoc />
        public override void ValidateObjectTypeIsCompatible(
            Type objectType)
        {
            objectType.MustForArg(nameof(objectType)).NotBeNull().And().BeEqualTo(typeof(DateTime));
        }
    }
}
