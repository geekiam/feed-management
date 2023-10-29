using AutoMapper;
using MediatR;
using Services;
using Threenine.ApiResponse;

namespace Api.Activities.Website.Queries.Get;

public class Handler : IRequestHandler<Query, SingleResponse<Response>>
{
    private readonly IDomainService<Domain.Websites.Website, string> _service;
    private readonly IMapper _mapper;


    public Handler(IDomainService<Domain.Websites.Website, string>  service, IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    public async Task<SingleResponse<Response>> Handle(Query request, CancellationToken cancellationToken)
    {
        var site = await _service.Get(request.Identifier);

        var response = _mapper.Map<Response>(site);
        
        var result = new SingleResponse<Response>(response);
        return result;
    }
}
