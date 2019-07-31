namespace Naos.Protocol.Domain.Test {
    using System;

    public partial class SqlProtocol<TKey, TObject, TLocator> : IProtocol<GetLatest<TObject>>
        where TKey : class
        where TObject : class
        where TLocator : StreamLocatorBase
    {
        /// <inheritdoc />
        public TReturn Execute<TReturn>(
            GetLatest<TObject> operation)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public void Execute(
            GetLatest<TObject> operation)
        {
            throw new NotImplementedException();
        }

    }
}