using Domain;

namespace Services;

public interface ISiteService
{
    Task<Site> Get(string indentifier);
}