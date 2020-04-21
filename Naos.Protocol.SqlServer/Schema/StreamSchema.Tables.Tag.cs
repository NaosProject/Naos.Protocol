// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StreamSchema.Tables.Tag.cs" company="Naos Project">
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
        /// Builds the creation script for tag table.
        /// </summary>
        /// <param name="streamName">Name of the stream.</param>
        /// <returns>Creation script for tag.</returns>
        public static string BuildCreationScriptForTag(
            string streamName)
        {
            var result = FormattableString.Invariant($@"
SET ANSI_NULLS ON


SET QUOTED_IDENTIFIER ON


CREATE TABLE [{streamName}].[Tag](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ObjectId] [int] NOT NULL,
	[ObjectTypeWithoutVersionId] [int] NOT NULL,
	[TagKey] [nvarchar](450) NOT NULL,
	[TagValue] [nvarchar](4000) NULL,
	[RecordCreatedUtc] [datetime2] NOT NULL,
 CONSTRAINT [PK_Tag] PRIMARY KEY CLUSTERED 
(
	[Id] DESC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]


ALTER TABLE [{streamName}].[Tag]  WITH CHECK ADD  CONSTRAINT [FK_Tags_Object] FOREIGN KEY([ObjectId])
REFERENCES [{streamName}].[Object] ([Id])


ALTER TABLE [{streamName}].[Tag] CHECK CONSTRAINT [FK_Tags_Object]

SET ANSI_PADDING ON

CREATE NONCLUSTERED INDEX [IX_Tag_Id_Asc] ON [{streamName}].[Tag]
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

CREATE NONCLUSTERED INDEX [IX_Tag_ObjectId_Desc] ON [{streamName}].[Tag]
(
	[ObjectId] DESC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

CREATE NONCLUSTERED INDEX [IX_Tag_ObjectTypeWithoutVersionId_Asc] ON [{streamName}].[Tag]
(
	[ObjectTypeWithoutVersionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

CREATE NONCLUSTERED INDEX [IX_Tag_TagKey_Asc] ON [{streamName}].[Tag]
(
	[TagKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
			");

            return result;
        }
    }
}
