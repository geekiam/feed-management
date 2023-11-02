using AutoMapper;
using Geekiam.Websites;
using MediatR;
using Services;
using Threenine.ApiResponse;

namespace Api.Activities.Websites.Commands.Post;

public class Handler : IRequestHandler<Command, SingleResponse<Response>>
{
    private readonly IFactory<Listing> _factory;
    private readonly IMapper _mapper;

    public Handler(IFactory<Listing> factory, IMapper mapper )
    {
        _factory = factory;
        _mapper = mapper;
    }

    public async Task<SingleResponse<Response>> Handle(Command request, CancellationToken cancellationToken)
    {
       
        var id = await _factory.Create(request.Body, cancellationToken);
        return new SingleResponse<Response>(new Response { Id = id});
    }
}
