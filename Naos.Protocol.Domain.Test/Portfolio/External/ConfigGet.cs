namespace Naos.Protocol.Domain.Test {
    using System;
    using Naos.Configuration.Domain;

    public class ConfigGet<T> : IProtocolWithReturn<GetLatest<T>, T>
        where T : class
    {
        /// <inheritdoc />
        public TReturn ExecuteScalar<TReturn>(
            GetLatest<T> operation)
        {
            if (typeof(T) != typeof(TReturn))
            {
                throw new ArgumentException("Type mismatch T and TReturn");
            }

            return Config.Get<TReturn>();
        }
    }
}