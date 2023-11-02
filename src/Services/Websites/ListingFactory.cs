using Geekiam.Persistence;
using Geekiam.Websites;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Models;

namespace Services;

public class ListingFactory :BaseService, IFactory<Listing>
{
    private readonly FeedsDbContext _dbContext;
    private readonly ILogger<ListingFactory> _logger;

    public ListingFactory(FeedsDbContext dbContext, ILogger<ListingFactory> logger): base(dbContext)
    {
        _dbContext = dbContext;
        _logger = logger;
    }
    
    public async Task<string> Create(Listing obj, CancellationToken cancellationToken)
    {
        var source = new Sources
        {
            Identifier = CreateIdentifier(obj.Domain),
            Name = obj.Name,
            Description = obj.Description,
            Protocol = obj.Protocol,
            Domain = obj.Domain,
            Feeds = new List<Feeds>(),
            Categories = new List<SourceCategory>(),
          
        };


        var categories = await ManageCategories(obj.Categories);
         categories.ForEach(id => source.Categories.Add(new SourceCategory { CategoryId = id}));
         obj.Feeds.ForEach(feed =>
         {
             source.Feeds.Add(new Feeds
             {
                 Path = feed.Path,
                 MediaTypeId = BaseMediaTypes.Where(x => x.Name.Equals(feed.Type)).Select(x => x.Id).Single()
             });
         });

         await Save(source, cancellationToken); 
        return source.Identifier;
    }

    private async Task<List<Guid>> ManageCategories(List<string> categories)
    {
        var all = await BaseCategoryQuery.Select(x => x.Name)
            .ToListAsync();
        
        var notExisting = categories.Except(all, StringComparer.OrdinalIgnoreCase).ToList();
        if (notExisting.Count > 0) await AddNotExistingCategories(notExisting);
        
        return BaseCategoryQuery.Where(x => categories.Contains(x.Name))
            .Select(x => x.Id)
            .ToList();
    }

    private async Task AddNotExistingCategories(List<string> categories)
    {
        var categoriesList = new List<Categories>();
        categories.ForEach(cat =>
        {
            categoriesList.Add( new Categories
            {
                Name = cat,
                Active = true,
                Created = DateTime.UtcNow,
                Modified = DateTime.UtcNow
            });
        });
        await Insert(categoriesList.AsEnumerable(), new CancellationToken()); 
    }
    private IQueryable<Categories> BaseCategoryQuery =>  _dbContext.Set<Categories>()
        .AsNoTrackingWithIdentityResolution()
        .Where(x => x.Active.Equals(true));

    private IQueryable<MediaTypes> BaseMediaTypes => _dbContext.Set<MediaTypes>()
        .AsNoTrackingWithIdentityResolution()
        .Where(x => x.Active.Equals(true));
    private static string CreateIdentifier(string domain)
    {
        var domainParts = domain.Split('.').ToArray();
        var id = domainParts[0] != "www" ? domainParts[0] : domainParts[2];
        return $"g_{id}_{new Random().Next(1, 9999)}";
    }
}