// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SimpleReadWriteRunner.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Protocol.Domain.Test
{
    using System.Threading.Tasks;

    public class SimpleReadWriteRunner :
    IRunOperations<Naos.Protocol.Domain.Test.GetSimple, Naos.Protocol.Domain.Test.Simple>,
    IRunOperations<Naos.Protocol.Domain.Test.PutSimple, Naos.Protocol.Domain.NoReturnType>
    {
        private readonly IRunOperations<Naos.Protocol.Domain.Test.GetSimple, Naos.Protocol.Domain.Test.Simple> getSimpleRunner;
        private readonly IRunOperations<Naos.Protocol.Domain.Test.PutSimple, Naos.Protocol.Domain.NoReturnType> putSimpleRunner;

        public SimpleReadWriteRunner(
        IRunOperations<Naos.Protocol.Domain.Test.GetSimple, Naos.Protocol.Domain.Test.Simple> getSimpleRunner,
        IRunOperations<Naos.Protocol.Domain.Test.PutSimple, Naos.Protocol.Domain.NoReturnType> putSimpleRunner)
        {
            this.getSimpleRunner = getSimpleRunner;
            this.putSimpleRunner = putSimpleRunner;
        }

        public async Task<Naos.Protocol.Domain.Test.Simple> RunAsync(Naos.Protocol.Domain.Test.GetSimple operation)
        {
            var result = await this.getSimpleRunner.RunAsync(operation);
            return result;
        }

        public async Task<Naos.Protocol.Domain.NoReturnType> RunAsync(Naos.Protocol.Domain.Test.PutSimple operation)
        {
            var result = await this.putSimpleRunner.RunAsync(operation);
            return result;
        }
    }

    public interface ISimpleReadWrite
    {
        Task<Naos.Protocol.Domain.Test.Simple> GetSimple(
        string identityToLocate);

        Task<Naos.Protocol.Domain.NoReturnType> PutSimple(
        Naos.Protocol.Domain.Test.Simple newSimple);
    }

    public class SimpleReadWrite : ISimpleReadWrite
    {
        private readonly SimpleReadWriteRunner runner;

        public SimpleReadWrite(
        SimpleReadWriteRunner runner)
        {
            this.runner = runner;
        }

        public async Task<Naos.Protocol.Domain.Test.Simple> GetSimple(
        string identityToLocate)
        {
            var operation = new Naos.Protocol.Domain.Test.GetSimple(identityToLocate);
            var result = await this.runner.RunAsync(operation);
            return result;
        }

        public async Task<Naos.Protocol.Domain.NoReturnType> PutSimple(
        Naos.Protocol.Domain.Test.Simple newSimple)
        {
            var operation = new Naos.Protocol.Domain.Test.PutSimple(newSimple);
            var result = await this.runner.RunAsync(operation);
            return result;
        }
    }
}
