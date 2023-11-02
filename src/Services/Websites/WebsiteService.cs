using Domain;
using Geekiam.Persistence;
using Geekiam.Websites;
using Microsoft.EntityFrameworkCore;
using Models;


namespace Services;

public class WebsiteService : BaseService, IDomainService<Website, string>
{
    private readonly FeedsDbContext _dbContext;

    public WebsiteService(FeedsDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
      
    }

    public async Task<Website> Get(string identifier)
    {

        var source = await BaseSourcesQuery
            .SingleOrDefaultAsync(x => x.Identifier.Equals(identifier));
     

        return new Website(source.Name, source.ToString(), source.Description)
        {
            Feeds = source.Feeds.Select( x => new Feed { Path = x.Path, Type = x.MediaType.Name}
            ).ToList(),
            Categories = source.Categories.Select(x => x.Category.Name).ToList()
        };
    }

    private IQueryable<Sources> BaseSourcesQuery => _dbContext.Set<Sources>()
        .Include(x => x.Feeds)
        .Include(x => x.Status)
        .Include(x => x.Feeds)
        .ThenInclude(x => x.MediaType)
        .Include(x => x.Categories)
        .ThenInclude(x => x.Category);
}