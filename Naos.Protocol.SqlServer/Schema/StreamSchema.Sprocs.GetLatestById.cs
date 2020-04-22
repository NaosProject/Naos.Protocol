// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StreamSchema.Sprocs.GetLatestById.cs" company="Naos Project">
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
        public static string BuildGetLatestByIdSprocName(
            string streamName)
        {
            return FormattableString.Invariant($"[{streamName}].GetLatestById");
        }

        /// <summary>
        /// Builds the creation script for put sproc.
        /// </summary>
        /// <param name="streamName">Name of the stream.</param>
        /// <returns>System.String.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "ForGet", Justification = NaosSuppressBecause.CA1704_IdentifiersShouldBeSpelledCorrectly_SpellingIsCorrectInContextOfTheDomain)]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Sproc", Justification = NaosSuppressBecause.CA1704_IdentifiersShouldBeSpelledCorrectly_SpellingIsCorrectInContextOfTheDomain)]
        public static string BuildCreationScriptForGetLatestByIdSproc(
            string streamName)
        {
            var result = FormattableString.Invariant($@"
CREATE PROCEDURE [{streamName}].GetLatestById(
  @SerializedObjectId AS nvarchar(450)
, @SerializationConfigAssemblyQualifiedNameWithoutVersion AS nvarchar(2000) OUTPUT
, @SerializationKind AS varchar(50) OUTPUT
, @SerializationFormat AS varchar(50) OUTPUT
, @CompressionKind AS varchar(50) OUTPUT
, @ObjectAssemblyQualifiedNameWithoutVersion AS nvarchar(2000) OUTPUT
, @ObjectAssemblyQualifiedNameWithVersion AS nvarchar(2000) OUTPUT
, @SerializedObjectString AS nvarchar(MAX) OUTPUT
, @SerializedObjectBinary AS varbinary(MAX) OUTPUT
)
AS
BEGIN

    DECLARE @SerializerDescriptionId int   
	DECLARE @ObjectTypeWithoutVersionId int
	DECLARE @ObjectTypeWithVersionId int
    SELECT TOP 1
	   @SerializerDescriptionId = [SerializerDescriptionId]
	 , @ObjectTypeWithoutVersionId = [ObjectTypeWithoutVersionId]
	 , @ObjectTypeWithVersionId = [ObjectTypeWithVersionId]
	 , @SerializedObjectString = [SerializedObjectString]
	 , @SerializedObjectBinary = [SerializedObjectBinary]
	FROM [{streamName}].[Object]
	WHERE [SerializedObjectId] = @SerializedObjectId
	ORDER BY [Id] DESC

	DECLARE @SerializationConfigTypeWithoutVersionId int
	SELECT 
		@SerializationConfigTypeWithoutVersionId = [SerializationConfigurationTypeWithoutVersionId] 
  	  , @SerializationKind = [SerializationKind]
	  , @SerializationFormat = [SerializationFormat]
	  , @CompressionKind = [CompressionKind]
	FROM [{streamName}].[SerializerDescription] WHERE [Id] = @SerializerDescriptionId

	SELECT @SerializationConfigAssemblyQualifiedNameWithoutVersion = [AssemblyQualifiedName] FROM [{streamName}].[TypeWithoutVersion] WHERE [Id] = @SerializationConfigTypeWithoutVersionId
	SELECT @ObjectAssemblyQualifiedNameWithoutVersion = [AssemblyQualifiedName] FROM [{streamName}].[TypeWithoutVersion] WHERE [Id] = @ObjectTypeWithoutVersionId
	SELECT @ObjectAssemblyQualifiedNameWithVersion = [AssemblyQualifiedName] FROM [{streamName}].[TypeWithVersion] WHERE [Id] = @ObjectTypeWithVersionId
END

			");

            return result;
        }
    }
}
