namespace Naos.Protocol.Domain {
    public interface IRequireProtocolNoReturn<TOperation> : IComposeProtocolNoReturn<TOperation>
        where TOperation : OperationNoReturnBase
    {
    }
    public interface IRequireProtocolWithReturn<TOperation, TReturn> : IComposeProtocolWithReturn<TOperation, TReturn>
        where TOperation : OperationWithReturnBase<TReturn>
    {
    }
}