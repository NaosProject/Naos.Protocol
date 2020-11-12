// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ExecuteOpEvent{TId,TOperation}Test.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Protocol.Domain.Test
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using FakeItEasy;

    using OBeautifulCode.AutoFakeItEasy;
    using OBeautifulCode.CodeAnalysis.Recipes;
    using OBeautifulCode.CodeGen.ModelObject.Recipes;
    using OBeautifulCode.Math.Recipes;
    using Xunit;

    using static System.FormattableString;

    [SuppressMessage("Microsoft.Maintainability", "CA1505:AvoidUnmaintainableCode", Justification = ObcSuppressBecause.CA1505_AvoidUnmaintainableCode_DisagreeWithAssessment)]
    public static partial class ExecuteOpEventTest
    {
        [SuppressMessage("Microsoft.Maintainability", "CA1505:AvoidUnmaintainableCode", Justification = ObcSuppressBecause.CA1505_AvoidUnmaintainableCode_DisagreeWithAssessment)]
        [SuppressMessage("Microsoft.Performance", "CA1810:InitializeReferenceTypeStaticFieldsInline", Justification = ObcSuppressBecause.CA1810_InitializeReferenceTypeStaticFieldsInline_FieldsDeclaredInCodeGeneratedPartialTestClass)]
        static ExecuteOpEventTest()
        {
            ConstructorArgumentValidationTestScenarios
               .RemoveAllScenarios()
               .AddScenario(
                    () =>
                        new ConstructorArgumentValidationTestScenario<ExecuteOpEvent<Version, ExecuteDefaultOperationsOnProtocolOp>>
                        {
                            Name = "constructor should throw ArgumentNullException when parameter 'id' is null scenario",
                            ConstructionFunc = () =>
                                               {
                                                   var referenceObject = A.Dummy<ExecuteOpEvent<Version, ExecuteDefaultOperationsOnProtocolOp>>();

                                                   var result = new ExecuteOpEvent<Version, ExecuteDefaultOperationsOnProtocolOp>(
                                                       null,
                                                       referenceObject.TimestampUtc,
                                                       referenceObject.ExecutedOperation,
                                                       referenceObject.Tags);

                                                   return result;
                                               },
                            ExpectedExceptionType = typeof(ArgumentNullException),
                            ExpectedExceptionMessageContains = new[]
                                                               {
                                                                   "id",
                                                               },
                        })
               .AddScenario(
                    () =>
                        new ConstructorArgumentValidationTestScenario<ExecuteOpEvent<Version, ExecuteDefaultOperationsOnProtocolOp>>
                        {
                            Name = "constructor should throw ArgumentNullException when parameter 'executedOperation' is null scenario",
                            ConstructionFunc = () =>
                                               {
                                                   var referenceObject = A.Dummy<ExecuteOpEvent<Version, ExecuteDefaultOperationsOnProtocolOp>>();

                                                   var result = new ExecuteOpEvent<Version, ExecuteDefaultOperationsOnProtocolOp>(
                                                       referenceObject.Id,
                                                       referenceObject.TimestampUtc,
                                                       null,
                                                       referenceObject.Tags);

                                                   return result;
                                               },
                            ExpectedExceptionType = typeof(ArgumentNullException),
                            ExpectedExceptionMessageContains = new[]
                                                               {
                                                                   "executedOperation",
                                                               },
                        })
               .AddScenario(
                    () =>
                        new ConstructorArgumentValidationTestScenario<ExecuteOpEvent<Version, ExecuteDefaultOperationsOnProtocolOp>>
                        {
                            Name = "constructor should throw ArgumentNullException when parameter 'tags' is null scenario",
                            ConstructionFunc = () =>
                                               {
                                                   var referenceObject = A.Dummy<ExecuteOpEvent<Version, ExecuteDefaultOperationsOnProtocolOp>>();

                                                   var result = new ExecuteOpEvent<Version, ExecuteDefaultOperationsOnProtocolOp>(
                                                       referenceObject.Id,
                                                       referenceObject.TimestampUtc,
                                                       referenceObject.ExecutedOperation,
                                                       null);

                                                   return result;
                                               },
                            ExpectedExceptionType = typeof(ArgumentNullException),
                            ExpectedExceptionMessageContains = new[]
                                                               {
                                                                   "tags",
                                                               },
                        })
               .AddScenario(
                    () =>
                        new ConstructorArgumentValidationTestScenario<ExecuteOpEvent<Version, ExecuteDefaultOperationsOnProtocolOp>>
                        {
                            Name = "constructor should throw ArgumentException when parameter 'tags' is an empty dictionary scenario",
                            ConstructionFunc = () =>
                                               {
                                                   var referenceObject = A.Dummy<ExecuteOpEvent<Version, ExecuteDefaultOperationsOnProtocolOp>>();

                                                   var result = new ExecuteOpEvent<Version, ExecuteDefaultOperationsOnProtocolOp>(
                                                       referenceObject.Id,
                                                       referenceObject.TimestampUtc,
                                                       referenceObject.ExecutedOperation,
                                                       new Dictionary<string, string>());

                                                   return result;
                                               },
                            ExpectedExceptionType = typeof(ArgumentException),
                            ExpectedExceptionMessageContains = new[]
                                                               {
                                                                   "tags",
                                                                   "is an empty dictionary",
                                                               },
                        })
               .AddScenario(
                    () =>
                        new ConstructorArgumentValidationTestScenario<ExecuteOpEvent<Version, ExecuteDefaultOperationsOnProtocolOp>>
                        {
                            Name =
                                "constructor should throw ArgumentException when parameter 'tags' contains a key-value pair with a null value scenario",
                            ConstructionFunc = () =>
                                               {
                                                   var referenceObject = A.Dummy<ExecuteOpEvent<Version, ExecuteDefaultOperationsOnProtocolOp>>();

                                                   var dictionaryWithNullValue = referenceObject.Tags.ToDictionary(_ => _.Key, _ => _.Value);

                                                   var randomKey =
                                                       dictionaryWithNullValue.Keys.ElementAt(
                                                           ThreadSafeRandom.Next(0, dictionaryWithNullValue.Count));

                                                   dictionaryWithNullValue[randomKey] = null;

                                                   var result = new ExecuteOpEvent<Version, ExecuteDefaultOperationsOnProtocolOp>(
                                                       referenceObject.Id,
                                                       referenceObject.TimestampUtc,
                                                       referenceObject.ExecutedOperation,
                                                       dictionaryWithNullValue);

                                                   return result;
                                               },
                            ExpectedExceptionType = typeof(ArgumentException),
                            ExpectedExceptionMessageContains = new[]
                                                               {
                                                                   "tags",
                                                                   "contains at least one key-value pair with a null value",
                                                               },
                        });
        }
    }
}