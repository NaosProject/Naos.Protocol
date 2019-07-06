// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ProxyBuilder.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Protocol.Domain.Test
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using static System.FormattableString;

    /// <summary>
    /// Proxy builder to create a constrained set of operations.
    /// </summary>
    public static class ProxyBuilder
    {
        /// <summary>
        /// Builds the proxies to contain a set of <see cref="OperationBase" />'s.
        /// </summary>
        /// <param name="serviceName">Name of the service.</param>
        /// <param name="operationTypes">Operation types to consolidate into an operational contract.</param>
        /// <returns>The C Sharp code as text to implement the operational contract.</returns>
        public static string BuildAllProxies(string serviceName, IReadOnlyCollection<Type> operationTypes)
        {
            var resultBuilder = new StringBuilder();

            // MERGED RUNNER
            resultBuilder.AppendLine(Invariant($"public class {serviceName}Runner : "));
            var runnerInterfaces = operationTypes.Select(_ => typeof(IHandleOperations<,>).MakeGenericType(_, _.BaseType.GetGenericArguments().Single())).ToList();
            var interfaceImplementationLines = operationTypes.ToList().Select(_ => Invariant($"IHandleOperations<{_.FullName}, {_.BaseType.GetGenericArguments().Single().FullName}>")).ToList();
            resultBuilder.AppendLine(string.Join("," + Environment.NewLine, interfaceImplementationLines));

            resultBuilder.AppendLine("{");

            // Member variables
            foreach (var runnerInterface in runnerInterfaces)
            {
                var genericArguments = runnerInterface.GetGenericArguments();
                var operationType = genericArguments[0];
                var returnType = genericArguments[1];
                resultBuilder.AppendLine(Invariant($"private readonly IHandleOperations<{operationType}, {returnType}> {operationType.Name.WithLowercaseFirstLetter()}Runner;"));
            }

            resultBuilder.AppendLine();

            // Constructor
            var runnerConstructorParameterLines = runnerInterfaces.Select(runnerInterface =>
            {
                var genericArguments = runnerInterface.GetGenericArguments();
                var operationType = genericArguments[0];
                var returnType = genericArguments[1];
                return Invariant($"IHandleOperations<{operationType}, {returnType}> {operationType.Name.WithLowercaseFirstLetter()}Runner ");
            });

            resultBuilder.AppendLine(Invariant($"public {serviceName}Runner("));
            resultBuilder.Append(string.Join("," + Environment.NewLine, runnerConstructorParameterLines));
            resultBuilder.AppendLine(")");
            resultBuilder.AppendLine("{");
            foreach (var runnerInterface in runnerInterfaces)
            {
                var operationType = runnerInterface.GetGenericArguments().First();
                resultBuilder.AppendLine(Invariant($"this.{operationType.Name.WithLowercaseFirstLetter()}Runner = {operationType.Name.WithLowercaseFirstLetter()}Runner;"));
            }

            resultBuilder.AppendLine("}");
            resultBuilder.AppendLine();

            // Methods
            var methodsLines = operationTypes.Select(operationType =>
            {
                var localBuilder = new StringBuilder();
                var returnType = operationType.BaseType.GetGenericArguments().Single();
                localBuilder.AppendLine(Invariant($"public async Task<{returnType.FullName}> HandleAsync({operationType.FullName} operation)"));
                localBuilder.AppendLine("{");
                localBuilder.AppendLine(Invariant($"var result = await this.{operationType.Name.WithLowercaseFirstLetter()}Runner.HandleAsync(operation);"));
                localBuilder.AppendLine("return result;");
                localBuilder.AppendLine("}");
                return localBuilder.ToString();
            });

            resultBuilder.Append(string.Join(Environment.NewLine, methodsLines));
            resultBuilder.AppendLine();
            resultBuilder.AppendLine(Invariant($"public I{serviceName} ToInterface()"));
            resultBuilder.AppendLine("{");
            resultBuilder.AppendLine(Invariant($"return new {serviceName}(this);"));
            resultBuilder.AppendLine("}");
            resultBuilder.AppendLine("}");

            // INTERFACE
            resultBuilder.AppendLine();
            resultBuilder.AppendLine(Invariant($"public interface I{serviceName}"));
            resultBuilder.AppendLine("{");
            
            //Methods
            var interfaceMethodLines = operationTypes.Select(operationType =>
            {
                var localBuilder = new StringBuilder();
                var returnType = operationType.BaseType.GetGenericArguments().Single();
                localBuilder.AppendLine(Invariant($"Task<{returnType.FullName}> {operationType.Name}("));
                var constructorParams = operationType.GetConstructors().Single().GetParameters().ToList();
                var constructorParamLines = constructorParams.Select(parameter =>
                {
                    return Invariant($"{parameter.ParameterType.FullName} {parameter.Name}");
                });

                localBuilder.Append(string.Join("," + Environment.NewLine, constructorParamLines));
                localBuilder.AppendLine(");");
                return localBuilder.ToString();
            });

            resultBuilder.Append(string.Join(Environment.NewLine, interfaceMethodLines));

            resultBuilder.AppendLine("}");

            // CONCRETE
            resultBuilder.AppendLine();
            resultBuilder.AppendLine(Invariant($"public class {serviceName} : I{serviceName}"));
            resultBuilder.AppendLine("{");

            // Members
            resultBuilder.AppendLine(Invariant($"private readonly {serviceName}Runner runner;"));
            resultBuilder.AppendLine();

            // Constructor
            resultBuilder.AppendLine(Invariant($"public {serviceName}("));
            resultBuilder.Append(Invariant($"{serviceName}Runner runner"));
            resultBuilder.AppendLine(")");
            resultBuilder.AppendLine("{");
            resultBuilder.AppendLine("this.runner = runner;");
            resultBuilder.AppendLine("}");
            resultBuilder.AppendLine();

            // Methods
            var concreteMethodLines = operationTypes.Select(operationType =>
            {
                var localBuilder = new StringBuilder();
                var returnType = operationType.BaseType.GetGenericArguments().Single();
                localBuilder.AppendLine(Invariant($"public async Task<{returnType.FullName}> {operationType.Name}("));
                var constructorParams = operationType.GetConstructors().Single().GetParameters().ToList();
                var constructorParamLines = constructorParams.Select(parameter =>
                {
                    return Invariant($"{parameter.ParameterType.FullName} {parameter.Name}");
                });

                localBuilder.Append(string.Join("," + Environment.NewLine, constructorParamLines));
                localBuilder.AppendLine(")");
                localBuilder.AppendLine("{");

                var constructorParamsForNewLines = constructorParams.Select(parameter =>
                {
                    return Invariant($"{parameter.Name}");
                });

                localBuilder.AppendLine(Invariant($"var operation = new {operationType.FullName}({string.Join(",", constructorParamsForNewLines)});"));
                localBuilder.AppendLine(Invariant($"var result = await this.runner.HandleAsync(operation);"));
                localBuilder.AppendLine("return result;");
                localBuilder.AppendLine("}");
                return localBuilder.ToString();
            });

            resultBuilder.Append(string.Join(Environment.NewLine, concreteMethodLines));
            resultBuilder.AppendLine("}");

            return resultBuilder.ToString().Replace("System.String", "string");
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1308:NormalizeStringsToUppercase", Justification = "Lowercase is preference for camel casing.")]
        private static string WithLowercaseFirstLetter(this string input)
        {
            var result = input.First().ToString().ToLowerInvariant() + input.Substring(1);
            return result;
        }
    }
}
