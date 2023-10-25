using MediatR;
using Services;
using Threenine.ApiResponse;

namespace Api.Activities.Website.Queries.Get;

public class Handler : IRequestHandler<Query, SingleResponse<Response>>
{
    private readonly ISiteService _siteService;

    public Handler(ISiteService siteService)
    {
        _siteService = siteService;
    }

    public async Task<SingleResponse<Response>> Handle(Query request, CancellationToken cancellationToken)
    {
        var response = new Response { Website = await _siteService.Get(request.Identifier) };
        
        var result = new SingleResponse<Response>(response);


        return result;
    }
}
