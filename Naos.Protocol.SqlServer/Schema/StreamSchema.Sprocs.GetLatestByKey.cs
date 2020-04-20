// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StreamSchema.Sprocs.GetLatestByKey.cs" company="Naos Project">
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
        public static string BuildGetLatestByKeySprocName(
            string streamName)
        {
            return FormattableString.Invariant($"[{streamName}].GetLatestByKey");
        }

        /// <summary>
        /// Builds the creation script for put sproc.
        /// </summary>
        /// <param name="streamName">Name of the stream.</param>
        /// <returns>System.String.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "ForGet", Justification = NaosSuppressBecause.CA1704_IdentifiersShouldBeSpelledCorrectly_SpellingIsCorrectInContextOfTheDomain)]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Sproc", Justification = NaosSuppressBecause.CA1704_IdentifiersShouldBeSpelledCorrectly_SpellingIsCorrectInContextOfTheDomain)]
        public static string BuildCreationScriptForGetLatestByKeySproc(
            string streamName)
        {
            var result = FormattableString.Invariant($@"
CREATE PROCEDURE [{streamName}].GetLatestByKey(
  @SerializedObjectId AS nvarchar(450)
, @SerializationConfigAssemblyQualifiedNameWithoutVersion AS nvarchar(2000) OUTPUT
, @SerializationKind AS varchar(50) OUTPUT
, @SerializationFormat AS varchar(50) OUTPUT
, @CompressionKind AS varchar(50) OUTPUT
, @SerializedObject AS nvarchar(MAX) OUTPUT
)
AS
BEGIN

    DECLARE @SerializerDescriptionId int   
    SELECT TOP 1
	   @SerializerDescriptionId = [SerializerDescriptionId]
	 , @SerializedObject = [SerializedObject]
	FROM [{streamName}].[Object]
	WHERE [SerializedObjectId] = @SerializedObjectId
	ORDER BY [RecordCreatedUtc] DESC

	DECLARE @TypeWithoutVersionId int
	SELECT 
		@TypeWithoutVersionId = [SerializationConfigurationTypeWithoutVersionId] 
  	  , @SerializationKind = [SerializationKind]
	  , @SerializationFormat = [SerializationFormat]
	  , @CompressionKind = [CompressionKind]
	FROM [{streamName}].[SerializerDescription] WHERE [Id] = @SerializerDescriptionId
	SELECT @SerializationConfigAssemblyQualifiedNameWithoutVersion = [AssemblyQualifiedName] FROM [{streamName}].[TypeWithoutVersion] WHERE [Id] = @TypeWithoutVersionId
END

			");

            return result;
        }
    }
}
