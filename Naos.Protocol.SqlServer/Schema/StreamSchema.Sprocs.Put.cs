// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StreamSchema.Sprocs.Put.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Protocol.SqlServer
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using Naos.Protocol.SqlServer.Internal;
    using OBeautifulCode.Serialization;

    /// <summary>
    /// Object table schema.
    /// </summary>
    public static partial class StreamSchema
    {
        /// <summary>
        /// Builds the name of the put stored procedure.
        /// </summary>
        /// <param name="streamName">Name of the stream.</param>
        /// <returns>Name of the put stored procedure.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Sproc", Justification = NaosSuppressBecause.CA1704_IdentifiersShouldBeSpelledCorrectly_SpellingIsCorrectInContextOfTheDomain)]
        public static string BuildPutSprocName(
            string streamName)
        {
            return FormattableString.Invariant($"[{streamName}].PutObject");
        }

        /// <summary>
        /// Builds the creation script for put sproc.
        /// </summary>
        /// <param name="streamName">Name of the stream.</param>
        /// <returns>System.String.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Sproc", Justification = NaosSuppressBecause.CA1704_IdentifiersShouldBeSpelledCorrectly_SpellingIsCorrectInContextOfTheDomain)]
        public static string BuildCreationScriptForPutSproc(
            string streamName)
        {
            var result = FormattableString.Invariant($@"
CREATE PROCEDURE [{streamName}].PutObject(
  @AssemblyQualitifiedNameWithoutVersion AS nvarchar(2000)
, @AssemblyQualitifiedNameWithVersion AS nvarchar(2000)
, @SerializerDescriptionId AS uniqueidentifier
, @SerializedKey AS nvarchar(450)
, @SerializedPayload AS varchar(max)
, @Tags AS xml
)
AS
BEGIN

BEGIN TRANSACTION [PutObject]
  BEGIN TRY
      DECLARE @TypeWithoutVersionId int
      EXEC [{streamName}].[GetIdAddIfNecessaryTypeWithoutVersion] @AssemblyQualitifiedNameWithoutVersion, @TypeWithoutVersionId OUTPUT
      DECLARE @TypeWithVersionId int
      EXEC [{streamName}].[GetIdAddIfNecessaryTypeWithVersion] @AssemblyQualitifiedNameWithVersion, @TypeWithVersionId OUTPUT
      
	  DECLARE @Id uniqueidentifier
	  SET @Id = NEWID()
	  DECLARE @CreateDateTimeUtc datetime
	  SET @CreateDateTimeUtc = GETUTCDATE()
	  INSERT INTO [{streamName}].[Object] (
          [Id]
		, [ObjectTypeWithoutVersionId]
		, [ObjectTypeWithVersionId]
		, [SerializerDescriptionId]
		, [SerializedKey]
		, [SerializedPayload]
		, [CreateDateTimeUtc]
		) VALUES (
		  @Id
		, @TypeWithoutVersionId
		, @TypeWithVersionId
		, @SerializerDescriptionId
		, @SerializedKey
		, @SerializedPayload
		, @CreateDateTimeUtc
		)
	  
	  IF (@Tags IS NOT NULL)
	  BEGIN
	      INSERT INTO [{streamName}].Tag
		  SELECT
			NEWID()
  		  , @Id
		  , C.value('(Tag/@Name)[1]', 'nvarchar(450)') as [Name]
		  , C.value('(Tag/@Value)[1]', 'nvarchar(4000)') as [Value]
		  , @CreateDateTimeUtc as CreateDateTimeUtc
		  FROM
			@Tags.nodes('/Tags') AS T(C)
	  END

      COMMIT TRANSACTION [PutObject]

  END TRY

  BEGIN CATCH
      ROLLBACK TRANSACTION [PutObject]

  END CATCH  
END
			");

            return result;
        }
    }
}
