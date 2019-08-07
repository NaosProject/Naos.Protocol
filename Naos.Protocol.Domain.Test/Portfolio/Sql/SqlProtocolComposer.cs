namespace Naos.Protocol.Domain.Test {
    using System;

    public class SqlProtocolComposer<TKey, TObject, TLocator> : ProtocolComposerBase,
                                                IGetProtocol<GetLatestOp<TObject>>,
                                                IGetProtocol<GetByKeyOp<TKey, TObject>>
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
                    var streamLocatorByKeyProtocol = this.DelegatedGet<DetermineLocatorByKeyOp<TKey, TLocator>, TLocator>();
                    this.sqlProtocol = new SqlProtocol<TKey, TObject, TLocator>(streamLocatorByKeyProtocol);
                }

                return this.sqlProtocol;
            }
        }

        /// <inheritdoc />
        IProtocol<GetLatestOp<TObject>> IGetProtocol<GetLatestOp<TObject>>.Get()
        {
            return this.GetSqlProtocolInstance();
        }

        /// <inheritdoc />
        IProtocol<GetByKeyOp<TKey, TObject>> IGetProtocol<GetByKeyOp<TKey, TObject>>.Get()
        {
            return this.GetSqlProtocolInstance();
        }
    }
}