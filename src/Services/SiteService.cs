using AutoMapper;
using Domain;
using Microsoft.EntityFrameworkCore;
using Models;
using Threenine.Data;

namespace Services;

public class SiteService : ISiteService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public SiteService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    public async Task<Site> Get(string identifier)
    {
        var repo = _unitOfWork.GetReadOnlyRepositoryAsync<Sources>();
        var website = await repo.SingleOrDefaultAsync(predicate: x => x.Identifier.Equals(identifier),
            include: inc => inc.Include(x => x.Feeds).Include(x => x.Status) );
        return _mapper.Map<Site>(website);
    }
}