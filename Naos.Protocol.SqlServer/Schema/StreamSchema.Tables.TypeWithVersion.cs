// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StreamSchema.Tables.TypeWithVersion.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Protocol.SqlServer
{
    using System;

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
	[AssemblyQualifiedName] [nvarchar](2000) UNIQUE NOT NULL,
	[RecordCreatedUtc] [datetime2] NULL,
 CONSTRAINT [PK_TypeWithVersion] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

SET ANSI_PADDING ON

CREATE NONCLUSTERED INDEX [IX_TypeWithoutVersion_Id_Asc] ON [{streamName}].[TypeWithVersion]
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
			");

            return result;
        }
    }
}