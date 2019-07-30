namespace Naos.Protocol.Domain {
    public interface IComposeProtocol<TOperation>
        where TOperation : OperationBase
    {
        IProtocol<TOperation> Compose();
    }
}