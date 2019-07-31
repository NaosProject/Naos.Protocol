namespace Naos.Protocol.Domain.Test {
    using System;
    using static System.FormattableString;

    public partial class SqlProtocol<TKey, TObject, TLocator>
        where TKey : class
        where TObject : class
        where TLocator : StreamLocatorBase
    {
        private static string DetermineProcedureByKeyType(
            Type keyType)
        {
            var procedureName = string.Empty;
            if (keyType == typeof(string))
            {
                procedureName = "QueryLatestEventByKeyOfTypeString";
            }
            else
            {
                throw new NotSupportedException(Invariant($"Key type '{keyType}' is not supported."));
            }

            return procedureName;
        }

        private static ExecuteProcedure<TObject> DetermineDatabaseOperation<TOperation>(TOperation operation)
            where TOperation : OperationBase<TObject>
        {
            throw new NotImplementedException();
        }
    }

    internal class ExecuteProcedure<TResult> : OperationBase<TResult>
    {

    }
}