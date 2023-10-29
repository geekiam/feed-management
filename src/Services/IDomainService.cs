
using Models;

namespace Services;

public interface IDomainService<TDomain, TKey>
{
    Task<TDomain> Get(TKey id);
}