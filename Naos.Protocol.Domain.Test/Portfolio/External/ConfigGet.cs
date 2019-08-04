namespace Naos.Protocol.Domain.Test {
    using System;
    using Naos.Configuration.Domain;

    public class ConfigGet<T> : IProtocolWithReturn<GetLatest<T>, T>
        where T : class
    {
        /// <inheritdoc />
        public T ExecuteScalar(
            GetLatest<T> operation)
        {
            return Config.Get<T>();
        }
    }
}