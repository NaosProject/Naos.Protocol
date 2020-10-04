// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GetResourceLocatorByIdOp{TId}Test.cs" company="Naos Project">
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

    public static partial class GetResourceLocatorByIdOpTest
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1810:InitializeReferenceTypeStaticFieldsInline", Justification = NaosSuppressBecause.CA1810_InitializeReferenceTypeStaticFieldsInline_FieldsDeclaredInCodeGeneratedPartialTestClass)]
        static GetResourceLocatorByIdOpTest()
        {
            ConstructorArgumentValidationTestScenarios.RemoveAllScenarios();
            ConstructorArgumentValidationTestScenarios.AddScenario(
                ConstructorArgumentValidationTestScenario<GetResourceLocatorByIdOp<Version>>.ConstructorCannotThrowScenario);
        }
    }
}