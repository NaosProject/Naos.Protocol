namespace Naos.Protocol.Domain.Test {
    using System;

    public partial class SqlProtocol<TKey, TObject, TLocator>
        where TKey : class
        where TObject : class
        where TLocator : StreamLocatorBase
    {
        private readonly IReturningProtocol<DetermineLocatorByKeyOp<TKey, TLocator>, TLocator> streamLocatorByKeyProtocol;

        public SqlProtocol(
            IReturningProtocol<DetermineLocatorByKeyOp<TKey, TLocator>, TLocator> streamLocatorByKeyProtocol)
        {
            this.streamLocatorByKeyProtocol = streamLocatorByKeyProtocol;
        }
    }
}