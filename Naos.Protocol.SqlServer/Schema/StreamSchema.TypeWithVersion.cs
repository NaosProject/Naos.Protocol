// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StreamSchema.TypeWithVersion.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Protocol.SqlServer
{
    using System;
    using Naos.Protocol.Domain;

    /// <summary>
    /// TODO: Starting point for new project.
    /// </summary>
    public partial class StreamSchema
    {
        /// <summary>
        /// Builds the creation script for type with version table.
        /// </summary>
        /// <param name="streamName">Name of the stream.</param>
        /// <returns>Creation script for the type with version table.</returns>
        public static string BuildCreationScriptForTypeWithVersion(
            string streamName)
        {
            var result = FormattableString.Invariant($@"
            SET ANSI_NULLS ON
            

            SET QUOTED_IDENTIFIER ON
            

            CREATE TABLE [{streamName}].[TypeWithVersion](
	            [Id] [int] IDENTITY(1,1) NOT NULL,
	            [AssemblyQualifiedName] [nvarchar](2000) NOT NULL,
	            [CreateDateTimeUtc] [datetime] NULL,
             CONSTRAINT [PK_TypeWithVersion] PRIMARY KEY CLUSTERED 
            (
	            [Id] ASC
            )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
            ) ON [PRIMARY]
            
			");

            return result;
        }
    }
}