namespace Naos.Protocol.Domain.Test {
    using System;

    public partial class SqlProtocol<TKey, TObject, TLocator>
        where TKey : class
        where TObject : class
        where TLocator : StreamLocatorBase
    {
        private readonly IProtocol<DetermineLocatorByKey<TKey, TLocator>> streamLocatorByKeyProtocol;

        public SqlProtocol(
            IProtocol<DetermineLocatorByKey<TKey, TLocator>> streamLocatorByKeyProtocol)
        {
            this.streamLocatorByKeyProtocol = streamLocatorByKeyProtocol;
        }
    }
}