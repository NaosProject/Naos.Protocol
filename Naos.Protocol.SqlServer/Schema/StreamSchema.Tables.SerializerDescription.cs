// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StreamSchema.Tables.SerializerDescription.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Protocol.SqlServer
{
    using System;

    /// <summary>
    /// SerializerDescription table schema.
    /// </summary>
    public partial class StreamSchema
    {
        /// <summary>
        /// Builds the creation script for SerializerDescription table.
        /// </summary>
        /// <param name="streamName">Name of the stream.</param>
        /// <returns>Creation script for SerializerDescription table.</returns>
        public static string BuildCreationScriptForSerializerDescription(
            string streamName)
        {
            var result = FormattableString.Invariant($@"
SET ANSI_NULLS ON


SET QUOTED_IDENTIFIER ON


CREATE TABLE [{streamName}].[SerializerDescription](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[SerializationKind] [varchar](50) NOT NULL,
	[SerializationFormat] [varchar](50) NOT NULL,
	[SerializationConfigurationTypeWithoutVersionId] [int] NOT NULL,
	[SerializationConfigurationTypeWithVersionId] [int] NOT NULL,
	[CompressionKind] [varchar](50) NOT NULL,
	[UnregisteredTypeEncounteredStrategy] [nvarchar](50) NULL,
	[RecordCreatedUtc] [datetime2] NOT NULL,
 CONSTRAINT [PK_SerializerDescription] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]


ALTER TABLE [{streamName}].[SerializerDescription]  WITH CHECK ADD  CONSTRAINT [FK_SerializerDescription_TypeWithoutVersion] FOREIGN KEY([SerializationConfigurationTypeWithoutVersionId])
REFERENCES [{streamName}].[TypeWithoutVersion] ([Id])


ALTER TABLE [{streamName}].[SerializerDescription] CHECK CONSTRAINT [FK_SerializerDescription_TypeWithoutVersion]


ALTER TABLE [{streamName}].[SerializerDescription]  WITH CHECK ADD  CONSTRAINT [FK_SerializerDescription_TypeWithVersion] FOREIGN KEY([SerializationConfigurationTypeWithVersionId])
REFERENCES [{streamName}].[TypeWithVersion] ([Id])


ALTER TABLE [{streamName}].[SerializerDescription] CHECK CONSTRAINT [FK_SerializerDescription_TypeWithVersion]

ALTER TABLE [{streamName}].[SerializerDescription] ADD CONSTRAINT [UQ_SerializerDescription_All] UNIQUE([SerializationKind], [SerializationFormat], [SerializationConfigurationTypeWithoutVersionId], [SerializationConfigurationTypeWithVersionId], [CompressionKind], [UnregisteredTypeEncounteredStrategy]);

SET ANSI_PADDING ON

CREATE NONCLUSTERED INDEX [IX_SerializerDescription_Id_Asc] ON [{streamName}].[SerializerDescription]
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

CREATE NONCLUSTERED INDEX [IX_SerializerDescription__SerializationKind_Asc] ON [{streamName}].[SerializerDescription]
(
	[SerializationKind] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

CREATE NONCLUSTERED INDEX [IX_SerializerDescription_SerializationFormat_Asc] ON [{streamName}].[SerializerDescription]
(
	[SerializationFormat] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

CREATE NONCLUSTERED INDEX [IX_SerializerDescription_SerializationConfigurationTypeWithoutVersionId_Asc] ON [{streamName}].[SerializerDescription]
(
	[SerializationConfigurationTypeWithoutVersionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

CREATE NONCLUSTERED INDEX [IX_SerializerDescription_SerializationConfigurationTypeWithVersionId_Asc] ON [{streamName}].[SerializerDescription]
(
	[SerializationConfigurationTypeWithVersionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

CREATE NONCLUSTERED INDEX [IX_SerializerDescription_CompressionKind_Asc] ON [{streamName}].[SerializerDescription]
(
	[CompressionKind] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

CREATE NONCLUSTERED INDEX [IX_SerializerDescription_UnregisteredTypeEncounteredStrategy_Asc] ON [{streamName}].[SerializerDescription]
(
	[UnregisteredTypeEncounteredStrategy] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
");

            return result;
        }
    }
}
