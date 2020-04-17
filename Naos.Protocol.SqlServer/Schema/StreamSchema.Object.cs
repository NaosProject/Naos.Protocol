// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StreamSchema.cs" company="Naos Project">
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
        public static string BuildCreationScriptForObject(
            string streamName)
        {
            var result = FormattableString.Invariant($@"
			SET ANSI_NULLS ON
			GO

			SET QUOTED_IDENTIFIER ON
			GO

			CREATE TABLE [{streamName}].[Object](
				[Id] [uniqueidentifier] NOT NULL,
				[ObjectTypeWithoutVersionId] [int] NOT NULL,
				[ObjectTypeWithVersionId] [int] NOT NULL,
				[SerializerDescriptionId] [int] NOT NULL,
				[SerializedPayload] [varchar](max) NOT NULL,
				[CreatedDateTimeUtc] [datetime] NOT NULL,
			 CONSTRAINT [PK_Object] PRIMARY KEY CLUSTERED 
			(
				[Id] ASC
			)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
			) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
			GO

			ALTER TABLE [{streamName}].[Object]  WITH CHECK ADD  CONSTRAINT [FK_Object_TypeWithoutVersion] FOREIGN KEY([ObjectTypeWithoutVersionId])
			REFERENCES [{streamName}].[TypeWithoutVersion] ([Id])
			GO

			ALTER TABLE [{streamName}].[Object] CHECK CONSTRAINT [FK_Object_TypeWithoutVersion]
			GO

			ALTER TABLE [{streamName}].[Object]  WITH CHECK ADD  CONSTRAINT [FK_Object_TypeWithVersion] FOREIGN KEY([ObjectTypeWithVersionId])
			REFERENCES [{streamName}].[TypeWithVersion] ([Id])
			GO

			ALTER TABLE [{streamName}].[Object] CHECK CONSTRAINT [FK_Object_TypeWithVersion]
			GO
			");

            return result;
        }
    }
}
