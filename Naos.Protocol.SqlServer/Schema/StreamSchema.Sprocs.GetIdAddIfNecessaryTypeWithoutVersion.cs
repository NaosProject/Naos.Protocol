﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StreamSchema.Sprocs.GetIdAddIfNecessaryTypeWithoutVersion.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Protocol.SqlServer
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Object table schema.
    /// </summary>
    public static partial class StreamSchema
    {
        /// <summary>
        /// Stored procedures.
        /// </summary>
        public static partial class Sprocs
        {
            /// <summary>
            /// Class TypeWithoutVersion.
            /// </summary>
            public static class GetIdAddIfNecessaryTypeWithoutVersion
            {
                /// <summary>
                /// Input parameter names.
                /// </summary>
                public enum InputParamNames
                {
                    /// <summary>
                    /// The object assembly qualified name without version.
                    /// </summary>
                    AssemblyQualifiedNameWithoutVersion,
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
                /// <param name="assemblyQualifiedNameWithoutVersion">The assembly qualified name.</param>
                /// <returns>ExecuteStoredProcedureOp.</returns>
                public static ExecuteStoredProcedureOp BuildExecuteStoredProcedureOp(
                    string streamName,
                    string assemblyQualifiedNameWithoutVersion)
                {
                    var sprocName = FormattableString.Invariant($"[{streamName}].{nameof(GetIdAddIfNecessaryTypeWithoutVersion)}");

                    var parameters = new List<SqlParameterRepresentationBase>()
                                     {
                                         new SqlInputParameterRepresentation<string>(
                                             nameof(InputParamNames.AssemblyQualifiedNameWithoutVersion),
                                             Tables.TypeWithoutVersion.AssemblyQualifiedName.DataType,
                                             assemblyQualifiedNameWithoutVersion),
                                         new SqlOutputParameterRepresentation<int>(nameof(OutputParamNames.Id), Tables.Object.Id.DataType),
                                     };

                    var parameterNameToDetailsMap = parameters.ToDictionary(k => k.Name, v => v);

                    var result = new ExecuteStoredProcedureOp(sprocName, parameterNameToDetailsMap);

                    return result;
                }

                /// <summary>
                /// Builds the name of the put stored procedure.
                /// </summary>
                /// <param name="streamName">Name of the stream.</param>
                /// <returns>Name of the put stored procedure.</returns>
                public static string BuildCreationScript(
                    string streamName)
                {
                    return FormattableString.Invariant(
                        $@"
CREATE PROCEDURE [{streamName}].{nameof(GetIdAddIfNecessaryTypeWithoutVersion)}(
  @{nameof(InputParamNames.AssemblyQualifiedNameWithoutVersion)} {Tables.TypeWithoutVersion.AssemblyQualifiedName.DataType.DeclarationInSqlSyntax},
  @{nameof(OutputParamNames.Id)} {Tables.TypeWithoutVersion.Id.DataType.DeclarationInSqlSyntax} OUTPUT
  )
AS
BEGIN

BEGIN TRANSACTION [GetIdAddTypeWithoutVersion]
  BEGIN TRY
      SELECT @{nameof(OutputParamNames.Id)} = [{nameof(Tables.TypeWithoutVersion.Id)}] FROM [{streamName}].[{nameof(Tables.TypeWithoutVersion)}]
        WHERE [{nameof(Tables.TypeWithoutVersion.AssemblyQualifiedName)}] = @{nameof(InputParamNames.AssemblyQualifiedNameWithoutVersion)}

	  IF (@{nameof(OutputParamNames.Id)} IS NULL)
	  BEGIN
	      
	      INSERT INTO [{streamName}].[{nameof(Tables.TypeWithoutVersion)}] ([{nameof(Tables.TypeWithoutVersion.AssemblyQualifiedName)}], [{nameof(Tables.TypeWithoutVersion.RecordCreatedUtc)}]) VALUES (@{nameof(InputParamNames.AssemblyQualifiedNameWithoutVersion)}, GETUTCDATE())
		  SET @{nameof(OutputParamNames.Id)} = SCOPE_IDENTITY()
	  END

      COMMIT TRANSACTION [GetIdAddTypeWithoutVersion]

  END TRY
  BEGIN CATCH
      SET @{nameof(OutputParamNames.Id)} = NULL
      DECLARE @ErrorMessage nvarchar(max), 
              @ErrorSeverity int, 
              @ErrorState int

      SELECT @ErrorMessage = ERROR_MESSAGE() + ' Line ' + cast(ERROR_LINE() as nvarchar(5)), @ErrorSeverity = ERROR_SEVERITY(), @ErrorState = ERROR_STATE()

      IF (@@trancount > 0)
      BEGIN
         ROLLBACK TRANSACTION [GetIdAddTypeWithoutVersion]
      END
    RAISERROR (@ErrorMessage, @ErrorSeverity, @ErrorState)
  END CATCH
END");
                }
            }
        }
    }
}