// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ConsoleAbstractionBase.cs" company="Naos Project">
//   Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// <auto-generated>
//   Sourced from NuGet package. Will be overwritten with package update except in 'Naos.Bootstrapper.Recipes.Core.Test' source.
// </auto-generated>
// --------------------------------------------------------------------------------------------------------------------

#if NaosBootstrapper
namespace Naos.Bootstrapper
#else
namespace Naos.Protocol.SqlServer.Test
#endif
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using CLAP;
    using Naos.Bootstrapper;
    using OBeautifulCode.Assertion.Recipes;
    using OBeautifulCode.Representation.System;
    using OBeautifulCode.Type.Recipes;

    /// <summary>
    /// Default implementation of the ConsoleAbstraction layer.
    /// </summary>
    public partial class ConsoleAbstraction :
#if !NaosBootstrapper
    ConsoleAbstractionBase
#else
    ConsoleAbstractionBaseStandin
#endif
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1810:InitializeReferenceTypeStaticFieldsInline", Justification = "Newing the serializer in a field initializer is fine.")]
        static ConsoleAbstraction()
        {
            ExceptionTypeRepresentationsToOnlyPrintMessage = new[] { typeof(TestFailedException).ToRepresentation() };
        }

        /// <summary>
        /// Runs the tests in the specified type.
        /// </summary>
        /// <param name="typeName">Name of the type.</param>
        /// <param name="debug">Optional value indicating that the debugger should be started; DEFAULT is false.</param>
        /// <exception cref="Naos.Bootstrapper.TestFailedException"></exception>
        [Verb(Aliases = "run", IsDefault = false, Description = "Runs the specified type's xUnit tests.")]
        public static void RunTestsInType(
            [Aliases("")] [Description("Name of type to run tests in.")] string typeName,
            [Aliases("")] [Description("Launches the debugger.")] [DefaultValue(false)] bool debug)
        {
            if (debug)
            {
                Debugger.Launch();
            }

            var testAssembly = typeof(ConsoleAbstraction).Assembly;

            var types = testAssembly.GetTypes().Where(_ => !_.IsClosedAnonymousType()).ToList();
            var typeCandidates = types.Where(_ => _?.FullName?.Contains(typeName) ?? false).ToList();

            new { typeCandidates }.Must().NotBeEmptyEnumerable();
            new { candidates = typeCandidates.Count() }.Must().BeEqualTo(1);
            var testType = typeCandidates.Single();

            bool success;
            using (var runner = new TestRunner(Console.WriteLine))
            {
                success = runner.RunAllTestsInTypeUsingXunit(testType);
            }

            if (!success)
            {
                throw new TestFailedException(testType);
            }
        }
    }
}
