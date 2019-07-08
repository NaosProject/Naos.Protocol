// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Channel.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Protocol.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.InteropServices;
    using System.Text;
    using OBeautifulCode.Type;
    using static System.FormattableString;

    public class MethodInfoDescription
    {
        public MethodInfoDescription(TypeDescription type, string methodHash, List<TypeDescription> genericArguments)
        {
            this.Type = type;
            this.MethodHash = methodHash;
            this.GenericArguments = genericArguments;
        }

        public List<TypeDescription> GenericArguments { get; private set; }

        public string MethodHash { get; private set; }

        public TypeDescription Type { get; private set; }
    }

    public static class MethodInfoDescriptionExtensions
    {
        /// <summary>Gets the signature hash.</summary>
        /// <param name="methodInfo">The method information.</param>
        /// <returns>Hash of method signature.</returns>
        public static string GetSignatureHash(this MethodInfo methodInfo)
        {
            var declaringType = methodInfo.DeclaringType?.FullName ?? "<Unknown-MaybeDynamic>";
            var methodName = methodInfo.Name;
            var generics = methodInfo.IsGenericMethod ? string.Join(",", methodInfo.GetGenericArguments().Select(_ => _.FullName)) : null;
            var genericsAddIn = generics == null ? string.Empty : Invariant($"<{generics}>");
            var parameters = string.Join(",", methodInfo.GetParameters().Select(_ => Invariant($"{_.ParameterType}-{_.Name}")));
            var result = Invariant($"{declaringType}->{methodName}{genericsAddIn}({parameters})");
            return result;
        }

        public static MethodInfoDescription ToDescription(this MethodInfo methodInfo)
        {
            var methodHash = methodInfo.GetSignatureHash();
            var genericArguments = methodInfo.GetGenericArguments().Select(_ => _.ToTypeDescription()).ToList();
            var def = methodInfo.GetGenericMethodDefinition();
            var result = new MethodInfoDescription(methodInfo.DeclaringType.ToTypeDescription(), methodHash, genericArguments);
            return result;
        }

        public static MethodInfo FromDescription(this MethodInfoDescription description)
        {
            var methodHash = description.MethodHash;
            var type = description.Type.ResolveFromLoadedTypes();
            var methodInfos = type.GetAllMethodInfos();

            return methodInfos.Single(methodInfo =>
            {
                return methodInfo.GetSignatureHash().Equals(methodHash, StringComparison.OrdinalIgnoreCase);
            });
        }

        public static IReadOnlyCollection<MethodInfo> GetAllMethodInfos(this Type type)
        {
            var methodInfos = new List<MethodInfo>();

            var considered = new List<Type>();
            var queue = new Queue<Type>();
            considered.Add(type);
            queue.Enqueue(type);
            while (queue.Count > 0)
            {
                var subType = queue.Dequeue();
                foreach (var subInterface in subType.GetInterfaces())
                {
                    if (considered.Contains(subInterface))
                    {
                        continue;
                    }

                    considered.Add(subInterface);
                    queue.Enqueue(subInterface);
                }

                var typeProperties = subType.GetMethods(
                    BindingFlags.FlattenHierarchy
                    | BindingFlags.Public
                    | BindingFlags.Instance);

                var newPropertyInfos = typeProperties
                    .Where(x => !methodInfos.Contains(x));

                methodInfos.InsertRange(0, newPropertyInfos);
            }

            return methodInfos;
        }
    }
}