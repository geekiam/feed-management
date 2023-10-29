namespace Domain;

public interface IAggregateRoot< TDomain, TKey>
{
    TKey Id { get; }

}