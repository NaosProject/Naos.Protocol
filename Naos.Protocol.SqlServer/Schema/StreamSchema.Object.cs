// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StreamSchema.Object.cs" company="Naos Project">
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
			

			ALTER TABLE [{streamName}].[Object]  WITH CHECK ADD  CONSTRAINT [FK_Object_TypeWithoutVersion] FOREIGN KEY([ObjectTypeWithoutVersionId])
			REFERENCES [{streamName}].[TypeWithoutVersion] ([Id])
			

			ALTER TABLE [{streamName}].[Object] CHECK CONSTRAINT [FK_Object_TypeWithoutVersion]
			

			ALTER TABLE [{streamName}].[Object]  WITH CHECK ADD  CONSTRAINT [FK_Object_TypeWithVersion] FOREIGN KEY([ObjectTypeWithVersionId])
			REFERENCES [{streamName}].[TypeWithVersion] ([Id])
			

			ALTER TABLE [{streamName}].[Object] CHECK CONSTRAINT [FK_Object_TypeWithVersion]
			
			");

            return result;
        }
    }
}
