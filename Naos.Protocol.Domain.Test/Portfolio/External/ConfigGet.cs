namespace Naos.Protocol.Domain.Test {
    using System;
    using Naos.Configuration.Domain;

    public class ConfigGet<T> : IReturningProtocol<GetLatestOp<T>, T>
        where T : class
    {
        /// <inheritdoc />
        public T Execute(
            GetLatestOp<T> operation)
        {
            return Config.Get<T>();
        }
    }
}