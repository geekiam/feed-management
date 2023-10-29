using Domain;
using Domain.Websites;
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
        var repo = _unitOfWork.GetReadOnlyRepositoryAsync<Sources>();
        var website = await repo.SingleOrDefaultAsync(predicate: x => x.Identifier.Equals(identifier),
            include: inc => inc.Include(x => x.Feeds).Include(x => x.Status));

        return new Website(website.Name, website.ToString(), website.Description);
    }
}