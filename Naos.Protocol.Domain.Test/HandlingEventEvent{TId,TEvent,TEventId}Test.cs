// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HandlingEventEvent{TId,TEvent,TEventId}Test.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Protocol.Domain.Test
{
    using System;

    using FakeItEasy;

    using OBeautifulCode.AutoFakeItEasy;
    using OBeautifulCode.CodeGen.ModelObject.Recipes;
    using Xunit;

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1505:AvoidUnmaintainableCode")]
    public static partial class HandlingEventEventTest
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1810:InitializeReferenceTypeStaticFieldsInline")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1505:AvoidUnmaintainableCode")]
        static HandlingEventEventTest()
        {
            ConstructorArgumentValidationTestScenarios.RemoveAllScenarios();
            ConstructorArgumentValidationTestScenarios

               .AddScenario(
                    () =>
                        new ConstructorArgumentValidationTestScenario<HandlingEventEvent<Version,
                                ExecutedOpEvent<Version, HandleEventOp<ExecutingOpEvent<Version, GetIdFromObjectOp<Version, Version>>, Version>>,
                                Version>
                        >
                        {
                            Name = "constructor should throw ArgumentNullException when parameter 'handledEvent' is null scenario",
                            ConstructionFunc = () =>
                                               {
                                                   var referenceObject =
                                                       A.Dummy<HandlingEventEvent<Version,
                                                           ExecutedOpEvent<Version, HandleEventOp<
                                                               ExecutingOpEvent<Version, GetIdFromObjectOp<Version, Version>>, Version>>, Version>>();

                                                   var result =
                                                       new HandlingEventEvent<Version,
                                                           ExecutedOpEvent<Version, HandleEventOp<
                                                               ExecutingOpEvent<Version, GetIdFromObjectOp<Version, Version>>, Version>>, Version>(
                                                           referenceObject.Id,
                                                           referenceObject.TimestampUtc,
                                                           null,
                                                           referenceObject.Tags);

                                                   return result;
                                               },
                            ExpectedExceptionType = typeof(ArgumentNullException),
                            ExpectedExceptionMessageContains = new[]
                                                               {
                                                                   "handledEvent",
                                                               },
                        });
        }
    }
}