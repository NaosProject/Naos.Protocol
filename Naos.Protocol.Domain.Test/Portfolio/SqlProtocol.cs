namespace Naos.Protocol.Domain.Test {
    using System;

    public class SqlProtocol<TKey, T, TLocator> : IProtocol<GetByKey<TKey, T>>, IProtocol<GetLatest<T>>
        where TKey : class
        where T : class
        where TLocator : StreamLocatorBase
    {
        private readonly IProtocol<DetermineStreamLocatorByKey<TKey, TLocator>, TLocator> streamLocatorByKeyProtocol;

        public SqlProtocol(
            IProtocol<DetermineStreamLocatorByKey<TKey, TLocator>, TLocator> streamLocatorByKeyProtocol)
        {
            this.streamLocatorByKeyProtocol = streamLocatorByKeyProtocol;
        }

        /// <inheritdoc />
        public void Execute(
            GetByKey<TKey, T> operation)
        {
            var streamLocator = this.streamLocatorByKeyProtocol.Execute<TLocator>(new DetermineStreamLocatorByKey<TKey, TLocator>(operation.Key));
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public void Execute(
            GetLatest<T> operation)
        {
            throw new NotImplementedException();
        }
    }
}