namespace Domain;

public  abstract class AggregateRoot<TDomain, TKey> : IAggregateRoot<TDomain,TKey>
{
    public TKey Id { get; }

}