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
        public static string BuildCreationScriptForTag(
            string streamName)
        {
            var result = FormattableString.Invariant($@"
			SET ANSI_NULLS ON
			GO

			SET QUOTED_IDENTIFIER ON
			GO

			CREATE TABLE [{streamName}].[Tag](
				[Id] [uniqueidentifier] NOT NULL,
				[ObjectId] [uniqueidentifier] NOT NULL,
				[Name] [nvarchar](1000) NOT NULL,
				[Value] [nvarchar](4000) NULL,
				[CreatedDateTimeUtc] [datetime] NOT NULL,
			 CONSTRAINT [PK_Tag] PRIMARY KEY CLUSTERED 
			(
				[Id] ASC
			)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
			) ON [PRIMARY]
			GO

			ALTER TABLE [{streamName}].[Tag]  WITH CHECK ADD  CONSTRAINT [FK_Tags_Object] FOREIGN KEY([ObjectId])
			REFERENCES [{streamName}].[Object] ([Id])
			GO

			ALTER TABLE [{streamName}].[Tag] CHECK CONSTRAINT [FK_Tags_Object]
			GO
			");

            return result;
        }
    }
}
