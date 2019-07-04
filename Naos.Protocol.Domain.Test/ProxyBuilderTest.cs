// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ProxyBuilderTest.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Protocol.Domain.Test
{
    using System.Threading.Tasks;
    using OBeautifulCode.Validation.Recipes;
    using Xunit;
    using Xunit.Abstractions;
    using static System.FormattableString;

    /// <summary>
    /// Proxy builder tests.
    /// </summary>
    public class ProxyBuilderTest
    {
        private readonly ITestOutputHelper testOutputHelper;

        public ProxyBuilderTest(ITestOutputHelper testOutputHelper)
        {
            this.testOutputHelper = testOutputHelper;
        }

        [Fact]
        public void BuildSimpleProxy()
        {
            var text = ProxyBuilder.BuildAllProxies("SimpleReadWrite", new[] { typeof(GetSimple), typeof(PutSimple) });
            this.testOutputHelper.WriteLine(text);
        }
    }

    public class GetSimpleRunner : IRunOperations<GetSimple, Simple>
    {
        public Task<Simple> RunAsync(GetSimple operation)
        {
            new { operation }.Must().NotBeNull();
            return Task.FromResult(new Simple { Property = Invariant($"Values: {nameof(operation.IdentityToLocate)} = {operation.IdentityToLocate}.") });
        }
    }

    public class PutSimpleRunner : IRunOperations<PutSimple, NoReturnType>
    {
        public Task<NoReturnType> RunAsync(PutSimple operation)
        {
            new { operation }.Must().NotBeNull();
            return Task.FromResult((NoReturnType)null);
        }
    }

    public class GetSimple : ReadOperationBase<Simple>
    {
        public GetSimple(string identityToLocate)
        {
            this.IdentityToLocate = identityToLocate;
        }

        public string IdentityToLocate { get; private set; }
    }

    public class PutSimple : WriteOperationBase<NoReturnType>
    {
        public PutSimple(Simple newSimple)
        {
            this.NewSimple = newSimple;
        }

        public Simple NewSimple { get; private set; }
    }

    public class Simple
    {
        public string Property { get; set; }
    }
}
