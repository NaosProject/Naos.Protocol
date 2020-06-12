﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SqlDataTypeRepresentationBase.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Protocol.SqlServer
{
    using System;

    /// <summary>
    /// Top level .
    /// </summary>
    public abstract class SqlDataTypeRepresentationBase
    {
        /// <summary>
        /// Gets the declaration in SQL syntax.
        /// </summary>
        /// <returns>String of SQL that declares the type correctly, for use in Stored Procedures and Table declarations.</returns>
        public abstract string DeclarationInSqlSyntax { get; }

        /// <summary>
        /// Validates the object type is compatible, throws if not.
        /// </summary>
        /// <param name="objectType">Type of the .NET object.</param>
        public abstract void ValidateObjectTypeIsCompatible(Type objectType);
    }
}