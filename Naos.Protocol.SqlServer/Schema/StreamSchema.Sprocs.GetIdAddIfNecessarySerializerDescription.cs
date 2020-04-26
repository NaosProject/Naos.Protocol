// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StreamSchema.Sprocs.GetIdAddIfNecessarySerializerDescription.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Protocol.SqlServer
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Naos.Protocol.SqlServer.Internal;
    using OBeautifulCode.Compression;
    using OBeautifulCode.Serialization;

    using SerializationFormat = OBeautifulCode.Serialization.SerializationFormat;

    /// <summary>
    /// Stream schema.
    /// </summary>
    public static partial class StreamSchema
    {
        /// <summary>
        /// Stored procedures.
        /// </summary>
        public static partial class Sprocs
        {
            /// <summary>
            /// Stored procedure: GetIdAddIfNecessarySerializerDescription.
            /// </summary>
            public static class GetIdAddIfNecessarySerializerDescription
            {
                /// <summary>
                /// Input parameter names.
                /// </summary>
                public enum InputParamNames
                {
                    /// <summary>
                    /// The serialization configuration assembly qualified name without version
                    /// </summary>
                    ConfigAssemblyQualifiedNameWithoutVersion,

                    /// <summary>
                    /// The serialization configuration assembly qualified name with version
                    /// </summary>
                    ConfigAssemblyQualifiedNameWithVersion,

                    /// <summary>
                    /// The serialization kind.
                    /// </summary>
                    SerializationKind,

                    /// <summary>
                    /// The serialization format.
                    /// </summary>
                    SerializationFormat,

                    /// <summary>
                    /// The compression kind.
                    /// </summary>
                    CompressionKind,

                    /// <summary>
                    /// The unregistered type encountered strategy.
                    /// </summary>
                    UnregisteredTypeEncounteredStrategy,
                }

                /// <summary>
                /// Output parameter names.
                /// </summary>
                public enum OutputParamNames
                {
                    /// <summary>
                    /// The identifier.
                    /// </summary>
                    Id,
                }

                /// <summary>
                /// Builds the execute stored procedure operation.
                /// </summary>
                /// <param name="streamName">Name of the stream.</param>
                /// <param name="configAssemblyQualifiedNameWithoutVersion">The serialization configuration assembly qualified name without version.</param>
                /// <param name="configAssemblyQualifiedNameWithVersion">The serialization configuration assembly qualified name with version.</param>
                /// <param name="serializationKind">The <see cref="SerializationKind"/>.</param>
                /// <param name="serializationFormat">The <see cref="SerializationFormat"/>.</param>
                /// <param name="compressionKind">The <see cref="CompressionKind"/>.</param>
                /// <param name="unregisteredTypeEncounteredStrategy">The <see cref="UnregisteredTypeEncounteredStrategy"/>.</param>
                /// <returns>ExecuteStoredProcedureOp.</returns>
                public static ExecuteStoredProcedureOp BuildExecuteStoredProcedureOp(
                    string streamName,
                    string configAssemblyQualifiedNameWithoutVersion,
                    string configAssemblyQualifiedNameWithVersion,
                    SerializationKind serializationKind,
                    SerializationFormat serializationFormat,
                    CompressionKind compressionKind,
                    UnregisteredTypeEncounteredStrategy unregisteredTypeEncounteredStrategy)
                {
                    var sprocName = FormattableString.Invariant($"[{streamName}].{nameof(GetIdAddIfNecessarySerializerDescription)}");

                    var parameters = new List<SqlParameterRepresentationBase>()
                                     {
                                         new SqlInputParameterRepresentation<string>(nameof(InputParamNames.ConfigAssemblyQualifiedNameWithoutVersion), Tables.TypeWithoutVersion.AssemblyQualifiedName.DataType, configAssemblyQualifiedNameWithoutVersion),
                                         new SqlInputParameterRepresentation<string>(nameof(InputParamNames.ConfigAssemblyQualifiedNameWithVersion), Tables.TypeWithVersion.AssemblyQualifiedName.DataType, configAssemblyQualifiedNameWithVersion),
                                         new SqlInputParameterRepresentation<string>(nameof(InputParamNames.SerializationKind), Tables.SerializerDescription.SerializationKind.DataType, serializationKind.ToString()),
                                         new SqlInputParameterRepresentation<string>(nameof(InputParamNames.SerializationFormat), Tables.SerializerDescription.SerializationFormat.DataType, serializationFormat.ToString()),
                                         new SqlInputParameterRepresentation<string>(nameof(InputParamNames.CompressionKind), Tables.SerializerDescription.CompressionKind.DataType, compressionKind.ToString()),
                                         new SqlInputParameterRepresentation<string>(nameof(InputParamNames.UnregisteredTypeEncounteredStrategy), Tables.SerializerDescription.SerializationKind.DataType, unregisteredTypeEncounteredStrategy.ToString()),
                                         new SqlOutputParameterRepresentation<int>(nameof(OutputParamNames.Id), Tables.SerializerDescription.Id.DataType),
                                     };

                    var parameterNameToRepresentationMap = parameters.ToDictionary(k => k.Name, v => v);

                    var result = new ExecuteStoredProcedureOp(sprocName, parameterNameToRepresentationMap);

                    return result;
                }

                /// <summary>
                /// Builds the name of the put stored procedure.
                /// </summary>
                /// <param name="streamName">Name of the stream.</param>
                /// <returns>Name of the put stored procedure.</returns>
                [System.Diagnostics.CodeAnalysis.SuppressMessage(
                    "Microsoft.Naming",
                    "CA1704:IdentifiersShouldBeSpelledCorrectly",
                    MessageId = "Sproc",
                    Justification = NaosSuppressBecause.CA1704_IdentifiersShouldBeSpelledCorrectly_SpellingIsCorrectInContextOfTheDomain)]
                public static string BuildCreationScript(
                    string streamName)
                {
                    return FormattableString.Invariant(
                        $@"
CREATE PROCEDURE [{streamName}].GetIdAddIfNecessarySerializerDescription(
  @{InputParamNames.ConfigAssemblyQualifiedNameWithoutVersion} AS {Tables.TypeWithoutVersion.AssemblyQualifiedName.DataType.DeclarationInSqlSyntax}
, @{InputParamNames.ConfigAssemblyQualifiedNameWithVersion} AS {Tables.TypeWithVersion.AssemblyQualifiedName.DataType.DeclarationInSqlSyntax}
, @{InputParamNames.SerializationKind} {Tables.SerializerDescription.SerializationKind.DataType.DeclarationInSqlSyntax}
, @{InputParamNames.SerializationFormat} AS {Tables.SerializerDescription.SerializationFormat.DataType.DeclarationInSqlSyntax}
, @{InputParamNames.CompressionKind} AS {Tables.SerializerDescription.CompressionKind.DataType.DeclarationInSqlSyntax}
, @{InputParamNames.UnregisteredTypeEncounteredStrategy} AS {Tables.SerializerDescription.UnregisteredTypeEncounteredStrategy.DataType.DeclarationInSqlSyntax}
, @{OutputParamNames.Id} {Tables.SerializerDescription.Id.DataType.DeclarationInSqlSyntax} OUTPUT
)
AS
BEGIN

BEGIN TRANSACTION [GetIdAddSerializerDescription]
  BEGIN TRY
      DECLARE @TypeWithoutVersionId {Tables.TypeWithoutVersion.Id.DataType.DeclarationInSqlSyntax}
      EXEC [{streamName}].[GetIdAddIfNecessaryTypeWithoutVersion] @{InputParamNames.ConfigAssemblyQualifiedNameWithoutVersion}, @TypeWithoutVersionId OUTPUT
      DECLARE @TypeWithVersionId {Tables.TypeWithVersion.Id.DataType.DeclarationInSqlSyntax}
      EXEC [{streamName}].[GetIdAddIfNecessaryTypeWithVersion] @{InputParamNames.ConfigAssemblyQualifiedNameWithVersion}, @TypeWithVersionId OUTPUT
      
      SELECT @{nameof(OutputParamNames.Id)} = [{nameof(Tables.SerializerDescription.Id)}] FROM [{streamName}].[{nameof(Tables.SerializerDescription)}]
        WHERE [{nameof(Tables.SerializerDescription.SerializationConfigurationTypeWithVersionId)}] = @TypeWithVersionId
        AND [{nameof(Tables.SerializerDescription.SerializationConfigurationTypeWithoutVersionId)}] = @TypeWithoutVersionId
        AND [{nameof(Tables.SerializerDescription.SerializationKind)}] = @{InputParamNames.SerializationKind}
        AND [{nameof(Tables.SerializerDescription.SerializationFormat)}] = @{InputParamNames.SerializationFormat}
        AND [{nameof(Tables.SerializerDescription.CompressionKind)}] = @{InputParamNames.CompressionKind}
        AND [{nameof(Tables.SerializerDescription.UnregisteredTypeEncounteredStrategy)}] = @{InputParamNames.UnregisteredTypeEncounteredStrategy}

	  IF (@{nameof(OutputParamNames.Id)} IS NULL)
	  BEGIN
	      INSERT INTO [{streamName}].[{nameof(Tables.SerializerDescription)}] (
		    [{nameof(Tables.SerializerDescription.SerializationKind)}]
		  , [{nameof(Tables.SerializerDescription.SerializationFormat)}]
		  , [{nameof(Tables.SerializerDescription.SerializationConfigurationTypeWithoutVersionId)}]
		  , [{nameof(Tables.SerializerDescription.SerializationConfigurationTypeWithVersionId)}]
		  , [{nameof(Tables.SerializerDescription.CompressionKind)}]
		  , [{nameof(Tables.SerializerDescription.UnregisteredTypeEncounteredStrategy)}]
		  , [{nameof(Tables.SerializerDescription.RecordCreatedUtc)}]
		  ) VALUES (
	        @{InputParamNames.SerializationKind}
		  , @{InputParamNames.SerializationFormat}
		  , @TypeWithoutVersionId
		  , @TypeWithVersionId
		  , @{InputParamNames.CompressionKind}
		  , @{InputParamNames.UnregisteredTypeEncounteredStrategy}
		  , GETUTCDATE()
		  )

	      SET @{nameof(OutputParamNames.Id)} = SCOPE_IDENTITY()
	  END

      COMMIT TRANSACTION [GetIdAddSerializerDescription]

  END TRY

  BEGIN CATCH
      SET @{nameof(OutputParamNames.Id)} = NULL
      DECLARE @ErrorMessage nvarchar(max), 
              @ErrorSeverity int, 
              @ErrorState int

      SELECT @ErrorMessage = ERROR_MESSAGE() + ' Line ' + cast(ERROR_LINE() as nvarchar(5)), @ErrorSeverity = ERROR_SEVERITY(), @ErrorState = ERROR_STATE()

      IF (@@trancount > 0)
      BEGIN
         ROLLBACK TRANSACTION [GetIdAddSerializerDescription]
      END
    RAISERROR (@ErrorMessage, @ErrorSeverity, @ErrorState)
  END CATCH
END");
                }
            }
        }
    }
}
