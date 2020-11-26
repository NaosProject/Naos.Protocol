// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CacheResult{TOperation,TReturn}Test.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Protocol.Domain.Test
{
    using System;

    using FakeItEasy;
    using Naos.CodeAnalysis.Recipes;
    using OBeautifulCode.AutoFakeItEasy;
    using OBeautifulCode.CodeGen.ModelObject.Recipes;
    using Xunit;

    public static partial class CacheResultTOperationTReturnTest
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1810:InitializeReferenceTypeStaticFieldsInline", Justification = NaosSuppressBecause.CA1810_InitializeReferenceTypeStaticFieldsInline_FieldsDeclaredInCodeGeneratedPartialTestClass)]
        static CacheResultTOperationTReturnTest()
        {
            ConstructorArgumentValidationTestScenarios.RemoveAllScenarios();
            ConstructorArgumentValidationTestScenarios
               .AddScenario(
                    () =>
                        new ConstructorArgumentValidationTestScenario<CacheResult<Version, Version>>
                        {
                            Name = "constructor should throw ArgumentNullException when parameter 'operation' is null scenario",
                            ConstructionFunc = () =>
                                               {
                                                   var referenceObject = A.Dummy<CacheResult<Version, Version>>();

                                                   var result = new CacheResult<Version, Version>(
                                                       null,
                                                       referenceObject.CachedObject,
                                                       referenceObject.FreshnessInUtc);

                                                   return result;
                                               },
                            ExpectedExceptionType = typeof(ArgumentNullException),
                            ExpectedExceptionMessageContains = new[]
                                                               {
                                                                   "operation",
                                                               },
                        });
        }
    }
}