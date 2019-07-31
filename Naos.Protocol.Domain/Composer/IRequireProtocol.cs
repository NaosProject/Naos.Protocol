namespace Naos.Protocol.Domain {
    public interface IRequireProtocol<TOperation> : IComposeProtocol<TOperation>
        where TOperation : OperationBase
    {
    }
}