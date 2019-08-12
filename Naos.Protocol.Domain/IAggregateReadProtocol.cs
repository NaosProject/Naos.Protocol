namespace Naos.Protocol.Domain.Test {
    public interface IAggregateReadProtocol<K, T>
        : IReturningProtocol<GetLatestOp<T>, T>, IReturningProtocol<GetByKeyOp<K, T>, T>
        where T : class
    {
    }

    public interface IGetIAggregateReadProtocol<K, T>
        : IGetReturningProtocol<GetLatestOp<T>, T>, IGetReturningProtocol<GetByKeyOp<K, T>, T>
        where T : class
    {
        IAggregateReadProtocol<K, T> Get();
    }

    public class AggregateReadProtocolComposer<K, T> : IGetIAggregateReadProtocol<K, T>
        where T : class
    {
        public AggregateReadProtocolCompose

        IAggregateReadProtocol<K, T> IGetIAggregateReadProtocol<K, T>.Get()
        {
            return new AggregateReadProtocol();
        }

        IReturningProtocol<GetLatestOp<T>, T> IGetReturningProtocol<GetLatestOp<T>, T>.Get()
        {
            return this.aggregateReadProtocol;
        }

        IReturningProtocol<GetByKeyOp<K, T>, T> IGetReturningProtocol<GetByKeyOp<K, T>, T>.Get()
        {
            return this.aggregateReadProtocol;
        }
    }

    public class AggregateReadProtocol
    {

    }
}