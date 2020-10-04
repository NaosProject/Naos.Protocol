// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ExecutingOpEvent{TId,TOperation}Test.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Protocol.Domain.Test
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using FakeItEasy;
    using Naos.CodeAnalysis.Recipes;
    using OBeautifulCode.AutoFakeItEasy;
    using OBeautifulCode.CodeGen.ModelObject.Recipes;
    using OBeautifulCode.Math.Recipes;
    using Xunit;

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1505:AvoidUnmaintainableCode", Justification = NaosSuppressBecause.CA1505_AvoidUnmaintainableCode_DisagreeWithAssessment)]
    public static partial class ExecutingOpEventTest
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1810:InitializeReferenceTypeStaticFieldsInline", Justification = NaosSuppressBecause.CA1810_InitializeReferenceTypeStaticFieldsInline_FieldsDeclaredInCodeGeneratedPartialTestClass)]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1505:AvoidUnmaintainableCode", Justification = NaosSuppressBecause.CA1505_AvoidUnmaintainableCode_DisagreeWithAssessment)]
        static ExecutingOpEventTest()
        {
            ConstructorArgumentValidationTestScenarios.RemoveAllScenarios();
            ConstructorArgumentValidationTestScenarios
               .AddScenario(
                    () =>
                        new ConstructorArgumentValidationTestScenario<ExecutingOpEvent<Version,
                            HandleEventOp<ExecutedOpEvent<Version, GetIdFromObjectOp<Version, Version>>, Version>>>
                        {
                            Name = "constructor should throw ArgumentNullException when parameter 'executedOperation' is null scenario",
                            ConstructionFunc = () =>
                                               {
                                                   var referenceObject =
                                                       A.Dummy<ExecutingOpEvent<Version, HandleEventOp<
                                                           ExecutedOpEvent<Version, GetIdFromObjectOp<Version, Version>>, Version>>>();

                                                   var result =
                                                       new ExecutingOpEvent<Version, HandleEventOp<
                                                           ExecutedOpEvent<Version, GetIdFromObjectOp<Version, Version>>, Version>>(
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
                        });
        }
    }
}