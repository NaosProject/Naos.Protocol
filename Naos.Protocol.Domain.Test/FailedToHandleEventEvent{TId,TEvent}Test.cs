// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FailedToHandleEventEvent{TId,TEvent}Test.cs" company="Naos Project">
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
    public static partial class FailedToHandleEventEventTIdTEventTest
    {
        [SuppressMessage("Microsoft.Maintainability", "CA1505:AvoidUnmaintainableCode", Justification = ObcSuppressBecause.CA1505_AvoidUnmaintainableCode_DisagreeWithAssessment)]
        [SuppressMessage("Microsoft.Performance", "CA1810:InitializeReferenceTypeStaticFieldsInline", Justification = ObcSuppressBecause.CA1810_InitializeReferenceTypeStaticFieldsInline_FieldsDeclaredInCodeGeneratedPartialTestClass)]
        static FailedToHandleEventEventTIdTEventTest()
        {
            ConstructorArgumentValidationTestScenarios
               .RemoveAllScenarios()
               .AddScenario(
                    () =>
                        new ConstructorArgumentValidationTestScenario<
                            FailedToHandleEventEvent<Version, ExecuteOperationRequestedEvent<Version, GetProtocolByTypeOp>>>
                        {
                            Name = "constructor should throw ArgumentNullException when parameter 'exceptionToString' is null scenario",
                            ConstructionFunc = () =>
                                               {
                                                   var referenceObject =
                                                       A.Dummy<FailedToHandleEventEvent<Version,
                                                           ExecuteOperationRequestedEvent<Version, GetProtocolByTypeOp>>>();

                                                   var result =
                                                       new FailedToHandleEventEvent<Version,
                                                           ExecuteOperationRequestedEvent<Version, GetProtocolByTypeOp>>(
                                                           referenceObject.Id,
                                                           null,
                                                           referenceObject.TimestampUtc,
                                                           referenceObject.Tags);

                                                   return result;
                                               },
                            ExpectedExceptionType = typeof(ArgumentNullException),
                            ExpectedExceptionMessageContains = new[]
                                                               {
                                                                   "exceptionToString",
                                                               },
                        })
               .AddScenario(
                    () =>
                        new ConstructorArgumentValidationTestScenario<
                            FailedToHandleEventEvent<Version, ExecuteOperationRequestedEvent<Version, GetProtocolByTypeOp>>>
                        {
                            Name = "constructor should throw ArgumentException when parameter 'exceptionToString' is white space scenario",
                            ConstructionFunc = () =>
                                               {
                                                   var referenceObject =
                                                       A.Dummy<FailedToHandleEventEvent<Version,
                                                           ExecuteOperationRequestedEvent<Version, GetProtocolByTypeOp>>>();

                                                   var result =
                                                       new FailedToHandleEventEvent<Version,
                                                           ExecuteOperationRequestedEvent<Version, GetProtocolByTypeOp>>(
                                                           referenceObject.Id,
                                                           Invariant($"  {Environment.NewLine}  "),
                                                           referenceObject.TimestampUtc,
                                                           referenceObject.Tags);

                                                   return result;
                                               },
                            ExpectedExceptionType = typeof(ArgumentException),
                            ExpectedExceptionMessageContains = new[]
                                                               {
                                                                   "exceptionToString",
                                                                   "white space",
                                                               },
                        });
        }
    }
}