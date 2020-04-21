// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StreamSchema.Tables.Object.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Protocol.SqlServer
{
    using System;

    /// <summary>
    /// Object table schema.
    /// </summary>
    public static partial class StreamSchema
    {
        /// <summary>
        /// Builds the creation script for object table.
        /// </summary>
        /// <param name="streamName">Name of the stream.</param>
        /// <returns>Creation script for object table.</returns>
        public static string BuildCreationScriptForObject(
            string streamName)
        {
            var result = FormattableString.Invariant($@"
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

CREATE TABLE [{streamName}].[Object](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ObjectTypeWithoutVersionId] [int] NOT NULL,
	[ObjectTypeWithVersionId] [int] NOT NULL,
	[SerializerDescriptionId] [int] NOT NULL,
	[SerializedObjectId] [nvarchar](450) NOT NULL,
	[SerializedObjectString] [nvarchar](max) NULL,
	[SerializedObjectBinary] [varbinary](max) NULL,
	[RecordCreatedUtc] [datetime2] NOT NULL,
 CONSTRAINT [PK_Object] PRIMARY KEY CLUSTERED 
(
	[Id] DESC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

ALTER TABLE [{streamName}].[Object]  WITH CHECK ADD  CONSTRAINT [FK_Object_TypeWithoutVersion] FOREIGN KEY([ObjectTypeWithoutVersionId])
REFERENCES [{streamName}].[TypeWithoutVersion] ([Id])

ALTER TABLE [{streamName}].[Object] CHECK CONSTRAINT [FK_Object_TypeWithoutVersion]

ALTER TABLE [{streamName}].[Object]  WITH CHECK ADD  CONSTRAINT [FK_Object_TypeWithVersion] FOREIGN KEY([ObjectTypeWithVersionId])
REFERENCES [{streamName}].[TypeWithVersion] ([Id])

ALTER TABLE [{streamName}].[Object] CHECK CONSTRAINT [FK_Object_TypeWithVersion]

SET ANSI_PADDING ON

CREATE NONCLUSTERED INDEX [IX_Object_Id_Asc] ON [{streamName}].[Object]
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

CREATE NONCLUSTERED INDEX [IX_Object_ObjectTypeWithoutVersionId_Asc] ON [{streamName}].[Object]
(
	[ObjectTypeWithoutVersionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

CREATE NONCLUSTERED INDEX [IX_Object_SerializedObjectId_Asc] ON [{streamName}].[Object]
(
	[SerializedObjectId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
			");

            return result;
        }
    }
}
