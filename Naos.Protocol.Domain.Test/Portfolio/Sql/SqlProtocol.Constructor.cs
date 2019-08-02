namespace Naos.Protocol.Domain.Test {
    using System;

    public partial class SqlProtocol<TKey, TObject, TLocator>
        where TKey : class
        where TObject : class
        where TLocator : StreamLocatorBase
    {
        private readonly IProtocolWithReturn<DetermineLocatorByKey<TKey, TLocator>, TLocator> streamLocatorByKeyProtocol;

        public SqlProtocol(
            IProtocolWithReturn<DetermineLocatorByKey<TKey, TLocator>, TLocator> streamLocatorByKeyProtocol)
        {
            this.streamLocatorByKeyProtocol = streamLocatorByKeyProtocol;
        }
    }
}