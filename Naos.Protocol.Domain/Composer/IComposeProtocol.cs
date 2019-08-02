namespace Naos.Protocol.Domain {
    public interface IComposeProtocol<TOperation>
        where TOperation : OperationNoReturnBase
    {
        IProtocol<TOperation> Compose();
    }

    public interface IComposeProtocolWithReturn<TOperation, TReturn>
        where TOperation : OperationWithReturnBase<TReturn>
    {
        IProtocolWithReturn<TOperation, TReturn> Compose();
    }

    public interface IComposeProtocolNoReturn<TOperation>
        where TOperation : OperationNoReturnBase
    {
        IProtocolNoReturn<TOperation> Compose();
    }
}