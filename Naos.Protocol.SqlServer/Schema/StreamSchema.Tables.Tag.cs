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
	[Id] [uniqueidentifier] NOT NULL,
	[ObjectId] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](450) NOT NULL,
	[Value] [nvarchar](4000) NULL,
	[CreateDateTimeUtc] [datetime] NOT NULL,
 CONSTRAINT [PK_Tag] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]


ALTER TABLE [{streamName}].[Tag]  WITH CHECK ADD  CONSTRAINT [FK_Tags_Object] FOREIGN KEY([ObjectId])
REFERENCES [{streamName}].[Object] ([Id])


ALTER TABLE [{streamName}].[Tag] CHECK CONSTRAINT [FK_Tags_Object]

			
			");

            return result;
        }
    }
}
