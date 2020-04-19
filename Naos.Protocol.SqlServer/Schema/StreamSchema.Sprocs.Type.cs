// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StreamSchema.Sprocs.Type.cs" company="Naos Project">
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
        public static string BuildTypeWithoutVersionSprocName(
            string streamName)
        {
            return FormattableString.Invariant($"[{streamName}].GetIdAddIfNecessaryTypeWithoutVersion");
        }

        /// <summary>
        /// Builds the name of the put stored procedure.
        /// </summary>
        /// <param name="streamName">Name of the stream.</param>
        /// <returns>Name of the put stored procedure.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Sproc", Justification = NaosSuppressBecause.CA1704_IdentifiersShouldBeSpelledCorrectly_SpellingIsCorrectInContextOfTheDomain)]
        public static string BuildCreationScriptForTypeWithoutVersionSproc(
            string streamName)
        {
            return FormattableString.Invariant($@"
CREATE PROCEDURE [{streamName}].GetIdAddIfNecessaryTypeWithoutVersion(
  @AssemblyQualitifiedNameWithoutVersion NVARCHAR(2000),
  @Result int OUTPUT
  )
AS
BEGIN

BEGIN TRANSACTION [GetIdAddTypeWithoutVersion]
  BEGIN TRY
      SELECT @Result = [Id] FROM [{streamName}].[TypeWithoutVersion]
        WHERE [AssemblyQualifiedName] = @AssemblyQualitifiedNameWithoutVersion

	  IF (@Result IS NULL)
	  BEGIN
	      
	      INSERT INTO [{streamName}].[TypeWithoutVersion] ([AssemblyQualifiedName], [CreateDateTimeUtc]) VALUES (@AssemblyQualitifiedNameWithoutVersion, GETUTCDATE())
		  SET @Result = SCOPE_IDENTITY()
	  END

      COMMIT TRANSACTION [GetIdAddTypeWithoutVersion]

  END TRY

  BEGIN CATCH

      SET @Result = NULL
      ROLLBACK TRANSACTION [GetIdAddTypeWithoutVersion]

  END CATCH  
END");
        }

        /// <summary>
        /// Builds the name of the put stored procedure.
        /// </summary>
        /// <param name="streamName">Name of the stream.</param>
        /// <returns>Name of the put stored procedure.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Sproc", Justification = NaosSuppressBecause.CA1704_IdentifiersShouldBeSpelledCorrectly_SpellingIsCorrectInContextOfTheDomain)]
        public static string BuildTypeWithVersionSprocName(
            string streamName)
        {
            return FormattableString.Invariant($"[{streamName}].GetIdAddIfNecessaryTypeWithVersion");
        }

        /// <summary>
        /// Builds the name of the put stored procedure.
        /// </summary>
        /// <param name="streamName">Name of the stream.</param>
        /// <returns>Name of the put stored procedure.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Sproc", Justification = NaosSuppressBecause.CA1704_IdentifiersShouldBeSpelledCorrectly_SpellingIsCorrectInContextOfTheDomain)]
        public static string BuildCreationScriptForTypeWithVersionSproc(
            string streamName)
        {
            return FormattableString.Invariant($@"
CREATE PROCEDURE [{streamName}].GetIdAddIfNecessaryTypeWithVersion(
  @AssemblyQualitifiedNameWithVersion NVARCHAR(2000),
  @Result int OUTPUT
  )
AS
BEGIN

BEGIN TRANSACTION [GetIdAddTypeWithVersion]
  BEGIN TRY
      SELECT @Result = [Id] FROM [{streamName}].[TypeWithVersion]
        WHERE [AssemblyQualifiedName] = @AssemblyQualitifiedNameWithVersion

	  IF (@Result IS NULL)
	  BEGIN
	      
	      INSERT INTO [{streamName}].[TypeWithVersion] ([AssemblyQualifiedName], [CreateDateTimeUtc]) VALUES (@AssemblyQualitifiedNameWithVersion, GETUTCDATE())
		  SET @Result = SCOPE_IDENTITY()
	  END

      COMMIT TRANSACTION [GetIdAddTypeWithVersion]

  END TRY

  BEGIN CATCH

      SET @Result = NULL
      ROLLBACK TRANSACTION [GetIdAddTypeWithVersion]

  END CATCH  
END");
        }
    }
}
