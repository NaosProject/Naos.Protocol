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
			");

            return result;
        }
    }
}
