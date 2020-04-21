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
, @SerializerDescriptionId AS int
, @SerializedObjectId AS nvarchar(450)
, @SerializedObjectString AS nvarchar(max)
, @SerializedObjectBinary AS varbinary(max)
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
      
	  DECLARE @Id int

	  DECLARE @RecordCreatedUtc datetime
	  SET @RecordCreatedUtc = GETUTCDATE()
	  INSERT INTO [{streamName}].[Object] (
		  [ObjectTypeWithoutVersionId]
		, [ObjectTypeWithVersionId]
		, [SerializerDescriptionId]
		, [SerializedObjectId]
		, [SerializedObjectString]
		, [SerializedObjectBinary]
		, [RecordCreatedUtc]
		) VALUES (
		  @TypeWithoutVersionId
		, @TypeWithVersionId
		, @SerializerDescriptionId
		, @SerializedObjectId
		, @SerializedObjectString
		, @SerializedObjectBinary
		, @RecordCreatedUtc
		)
      SET @Id = SCOPE_IDENTITY()
	  
	  IF (@Tags IS NOT NULL)
	  BEGIN
	      INSERT INTO [{streamName}].Tag
		  SELECT
  		    @Id
          , @TypeWithoutVersionId
		  , C.value('(Tag/@Key)[1]', 'nvarchar(450)') as [TagKey]
		  , C.value('(Tag/@Value)[1]', 'nvarchar(4000)') as [TagValue]
		  , @RecordCreatedUtc as RecordCreatedUtc
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
