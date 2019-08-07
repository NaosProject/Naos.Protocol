namespace Naos.Protocol.Domain.Test {
    using System;

    public partial class SqlProtocol<TKey, TObject, TLocator> : IProtocol<GetLatestOp<TObject>>
        where TKey : class
        where TObject : class
        where TLocator : StreamLocatorBase
    {
        /// <inheritdoc />
        public TReturn Execute<TReturn>(
            GetLatestOp<TObject> operation)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public void Execute(
            GetLatestOp<TObject> operation)
        {
            throw new NotImplementedException();
        }

    }
}