namespace Naos.Protocol.Domain.Test {
    using System;
    using Naos.Configuration.Domain;

    public class ConfigGet<T> : IProtocol<GetLatest<T>, T>
        where T : class
    {
        /// <inheritdoc />
        public void Execute(
            GetLatest<T> operation)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public TReturn Execute<TReturn>(
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