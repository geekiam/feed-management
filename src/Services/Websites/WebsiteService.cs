using System.Runtime.Intrinsics.X86;
using Domain;
using Geekiam.Websites.Get;
using Microsoft.EntityFrameworkCore;
using Models;
using Threenine.Data;

namespace Services;

public class WebsiteService : IDomainService<Website, string>
{
    private readonly IUnitOfWork _unitOfWork;
    
    public WebsiteService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        
    }

    public async Task<Website> Get(string identifier)
    {
       
        var source = await _unitOfWork.GetReadOnlyRepositoryAsync<Sources>()
            .SingleOrDefaultAsync(predicate: x => x.Identifier.Equals(identifier),
            include: inc => inc.Include(x => x.Feeds).Include(x => x.Status).Include(X => X.Feeds).ThenInclude(X => X.MediaType));

        var website = new Website(source.Name, source.ToString(), source.Description)
        {
            Feeds = source.Feeds.Select( x => new Feed { Path = x.Path, Type = x.MediaType.Name}
            ).ToList()
        };
        return website;
    }
}