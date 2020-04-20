// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StreamSchema.Sprocs.SerializerDescription.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Protocol.SqlServer
{
    using System;
    using Naos.Protocol.SqlServer.Internal;

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
        public static string BuildSerializerDescriptionSprocName(
            string streamName)
        {
            return FormattableString.Invariant($"[{streamName}].GetIdAddIfNecessarySerializerDescription");
        }

        /// <summary>
        /// Builds the name of the put stored procedure.
        /// </summary>
        /// <param name="streamName">Name of the stream.</param>
        /// <returns>Name of the put stored procedure.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Sproc", Justification = NaosSuppressBecause.CA1704_IdentifiersShouldBeSpelledCorrectly_SpellingIsCorrectInContextOfTheDomain)]
        public static string BuildCreationScriptForSerializerDescriptionSproc(
            string streamName)
        {
            return FormattableString.Invariant($@"
CREATE PROCEDURE [{streamName}].GetIdAddIfNecessarySerializerDescription(
  @AssemblyQualitifiedNameWithoutVersion AS nvarchar(2000)
, @AssemblyQualitifiedNameWithVersion AS nvarchar(2000)
, @SerializationKind AS varchar(50)
, @SerializationFormat AS varchar(50)
, @CompressionKind AS varchar(50)
, @UnregisteredTypeEncounteredStrategy AS varchar(50)
, @Result int OUTPUT
)
AS
BEGIN

BEGIN TRANSACTION [GetIdAddSerializerDescription]
  BEGIN TRY
      DECLARE @TypeWithoutVersionId int
      EXEC [{streamName}].[GetIdAddIfNecessaryTypeWithoutVersion] @AssemblyQualitifiedNameWithoutVersion, @TypeWithoutVersionId OUTPUT
      DECLARE @TypeWithVersionId int
      EXEC [{streamName}].[GetIdAddIfNecessaryTypeWithVersion] @AssemblyQualitifiedNameWithVersion, @TypeWithVersionId OUTPUT
      
      SELECT @Result = [Id] FROM [{streamName}].[SerializerDescription]
        WHERE [SerializationConfigurationTypeWithVersionId] = @TypeWithVersionId
        AND [SerializationConfigurationTypeWithoutVersionId] = @TypeWithoutVersionId
        AND [SerializationKind] = @SerializationKind
        AND [SerializationFormat] = @SerializationFormat
        AND [CompressionKind] = @CompressionKind
        AND [UnregisteredTypeEncounteredStrategy] = @UnregisteredTypeEncounteredStrategy

	  IF (@Result IS NULL)
	  BEGIN
	      INSERT INTO [{streamName}].[SerializerDescription] (
		    [SerializationKind]
		  , [SerializationFormat]
		  , [SerializationConfigurationTypeWithoutVersionId]
		  , [SerializationConfigurationTypeWithVersionId]
		  , [CompressionKind]
		  , [UnregisteredTypeEncounteredStrategy]
		  , [RecordCreatedUtc]
		  ) VALUES (
	        @SerializationKind
		  , @SerializationFormat
		  , @TypeWithoutVersionId
		  , @TypeWithVersionId
		  , @CompressionKind
		  , @UnregisteredTypeEncounteredStrategy
		  , GETUTCDATE()
		  )

	      SET @Result = SCOPE_IDENTITY()
	  END

      COMMIT TRANSACTION [GetIdAddSerializerDescription]

  END TRY

  BEGIN CATCH

      SET @Result = NULL
      ROLLBACK TRANSACTION [GetIdAddSerializerDescription]

  END CATCH  
END");
        }
    }
}
