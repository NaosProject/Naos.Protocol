namespace Naos.Protocol.Domain.Test {
    using System;

    public class SqlProtocolComposer<TKey, TObject, TLocator> : ProtocolComposerBase,
                                                IComposeProtocol<GetLatest<TObject>>,
                                                IComposeProtocol<GetByKey<TKey, TObject>>
        where TLocator : StreamLocatorBase
        where TObject : class
        where TKey : class
    {
        private readonly object syncSqlProtocol = new object();
        private SqlProtocol<TKey, TObject, TLocator> sqlProtocol;

        private SqlProtocol<TKey, TObject, TLocator> GetSqlProtocolInstance()
        {
            lock (this.syncSqlProtocol)
            {
                if (this.sqlProtocol == null)
                {
                    var streamLocatorByKeyProtocol = this.ReComposeWithReturn<DetermineLocatorByKey<TKey, TLocator>, TLocator>();
                    this.sqlProtocol = new SqlProtocol<TKey, TObject, TLocator>(streamLocatorByKeyProtocol);
                }

                return this.sqlProtocol;
            }
        }

        /// <inheritdoc />
        IProtocol<GetLatest<TObject>> IComposeProtocol<GetLatest<TObject>>.Compose()
        {
            return this.GetSqlProtocolInstance();
        }

        /// <inheritdoc />
        IProtocol<GetByKey<TKey, TObject>> IComposeProtocol<GetByKey<TKey, TObject>>.Compose()
        {
            return this.GetSqlProtocolInstance();
        }
    }
}