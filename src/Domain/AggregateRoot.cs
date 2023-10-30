namespace Domain;

public  abstract record AggregateRoot<TDomain, TKey> : IAggregateRoot<TDomain,TKey>
{
    public TKey Id { get; }
}