namespace Naos.Protocol.Domain.Test {
    using System;

    public partial class SqlProtocol<TKey, TObject, TLocator> : IProtocol<GetByKey<TKey, TObject>>
        where TKey : class
        where TObject : class
        where TLocator : StreamLocatorBase
    {
        /// <inheritdoc />
        public TReturn Execute<TReturn>(
            GetByKey<TKey, TObject> operation)
        {
            var streamLocator = this.streamLocatorByKeyProtocol.Execute<TLocator>(new DetermineLocatorByKey<TKey, TLocator>(operation.Key));

            // operations? run procedure?

            var keyType = typeof(TKey);
            var databaseOperation = DetermineDatabaseOperation(operation);

            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public void Execute(
            GetByKey<TKey, TObject> operation)
        {
            throw new NotImplementedException();
        }
    }
}