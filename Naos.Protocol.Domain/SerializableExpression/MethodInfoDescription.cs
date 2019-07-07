//// --------------------------------------------------------------------------------------------------------------------
//// <copyright file="Channel.cs" company="Naos Project">
////    Copyright (c) Naos Project 2019. All rights reserved.
//// </copyright>
//// --------------------------------------------------------------------------------------------------------------------

//namespace Naos.Protocol.Domain
//{
//    using System.Linq;
//    using System.Reflection;
//    using System.Runtime.InteropServices;
//    using System.Text;
//    using OBeautifulCode.Type;
//    using static System.FormattableString;

//    public class MethodInfoDescription
//    {
//        public MethodInfoDescription()
//        {

//        }
//    }

//    public static class MethodInfoDescriptionExtensions
//    {
//        public static string GetSignatureHash(this MethodInfo methodInfo)
//        {
//            var declaringType = methodInfo.DeclaringType?.FullName;
//            var methodName = methodInfo.Name;
//            var generics = methodInfo.IsGenericMethod ? string.Join(",", methodInfo.GetGenericArguments().Select(_ => _.FullName)) : null;
//            var genericsAddIn = generics == null ? string.Empty : Invariant($"<{generics}>");
//            var parameters = string.Join(",", methodInfo.GetParameters().Select(_ => Invariant($"{_.ParameterType}-{_.Name}")));
//            var result = Invariant($"{declaringType}->{methodName}{genericsAddIn}({parameters})");
//            return result;
//        }

//        public static MethodInfoDescription ToDescription(this MethodInfo methodInfo)
//        {
//            var methodHash = methodInfo.GetSignatureHash();
//            var result = new MethodInfoDescription(methodInfo.DeclaringType.ToTypeDescription(), methodHash);
//            return result;
//        }

//        public static MethodInfo FromDescription(this MethodInfoDescription methodInfo)
//        {

//        }
//    }
//}