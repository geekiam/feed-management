using Geekiam.Websites;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Models;
using Threenine.Data;

namespace Services;

public class ListingFactory : IFactory<Listing>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<ListingFactory> _logger;

    public ListingFactory(IUnitOfWork unitOfWork, ILogger<ListingFactory> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }
    public async Task<string> Create(Listing obj)
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

        obj.Categories.ForEach(category =>
        {
           
            var key = _unitOfWork.GetReadOnlyRepository<Categories>().SingleOrDefault(x => x.Name.ToLower().Equals(category.ToLower()));
           
            source.Categories.Add(new SourceCategory()
            {
               CategoryId = key.Id
            });
        });
        
        obj.Feeds.ForEach(feed =>
        {
            var media =_unitOfWork.GetReadOnlyRepository<MediaTypes>().SingleOrDefault(x => x.Name.ToLower().Equals(feed.Type.ToLower()));
            source.Feeds.Add(new Feeds()
            {
                Path = feed.Path,
                MediaTypeId = media.Id,
            
            });
        });


        var result = _unitOfWork.GetRepository<Sources>().Insert(source);
        await _unitOfWork.CommitAsync();
      

        return result.Identifier;
    }
  
    private static string CreateIdentifier(string domain)
    {
        var domainParts = domain.Split('.').ToArray();
        var id = domainParts[0] != "www" ? domainParts[0] : domainParts[2];
        return $"g_{id}_{new Random().Next(1, 9999)}";
    }
}