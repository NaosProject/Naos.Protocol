// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StreamSchema.cs" company="Naos Project">
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
        public static string BuildCreationScriptForSerializerDescription(
            string streamName)
        {
            var result = FormattableString.Invariant($@"
			SET ANSI_NULLS ON
			GO

			SET QUOTED_IDENTIFIER ON
			GO

			CREATE TABLE [{streamName}].[SerializerDescription](
				[Id] [uniqueidentifier] NOT NULL,
				[SerializationKind] [varchar](50) NOT NULL,
				[SerializationFormat] [varchar](50) NOT NULL,
				[SerializationConfigurationTypeWithoutVersionId] [int] NOT NULL,
				[SerializationConfigurationTypeWithVersionId] [int] NOT NULL,
				[CompressionKind] [varchar](50) NOT NULL,
				[CreatedDateTimeUtc] [datetime] NOT NULL,
			 CONSTRAINT [PK_SerializerDescription] PRIMARY KEY CLUSTERED 
			(
				[Id] ASC
			)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
			) ON [PRIMARY]
			GO

			ALTER TABLE [{streamName}].[SerializerDescription]  WITH CHECK ADD  CONSTRAINT [FK_SerializerDescription_TypeWithoutVersion] FOREIGN KEY([SerializationConfigurationTypeWithoutVersionId])
			REFERENCES [{streamName}].[TypeWithoutVersion] ([Id])
			GO

			ALTER TABLE [{streamName}].[SerializerDescription] CHECK CONSTRAINT [FK_SerializerDescription_TypeWithoutVersion]
			GO

			ALTER TABLE [{streamName}].[SerializerDescription]  WITH CHECK ADD  CONSTRAINT [FK_SerializerDescription_TypeWithVersion] FOREIGN KEY([SerializationConfigurationTypeWithVersionId])
			REFERENCES [{streamName}].[TypeWithVersion] ([Id])
			GO

			ALTER TABLE [{streamName}].[SerializerDescription] CHECK CONSTRAINT [FK_SerializerDescription_TypeWithVersion]
			GO
			");

            return result;
        }
    }
}
