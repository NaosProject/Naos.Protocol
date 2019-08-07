namespace Naos.Protocol.Domain.Test {
    using System;

    public partial class SqlProtocol<TKey, TObject, TLocator> : IProtocol<GetByKeyOp<TKey, TObject>>
        where TKey : class
        where TObject : class
        where TLocator : StreamLocatorBase
    {
        /// <inheritdoc />
        public TReturn Execute<TReturn>(
            GetByKeyOp<TKey, TObject> operation)
        {
            var streamLocator = this.streamLocatorByKeyProtocol.Execute(new DetermineLocatorByKeyOp<TKey, TLocator>(operation.Key));

            // operations? run procedure?

            var keyType = typeof(TKey);
            var databaseOperation = DetermineDatabaseOperation(operation);

            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public void Execute(
            GetByKeyOp<TKey, TObject> operation)
        {
            throw new NotImplementedException();
        }
    }
}