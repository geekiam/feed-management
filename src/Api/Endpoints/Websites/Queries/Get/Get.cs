using Ardalis.ApiEndpoints;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Diagnostics;
using Swashbuckle.AspNetCore.Annotations;
using Threenine.ApiResponse;

namespace Api.Activities.Website.Queries.Get;

[Route(Routes.Websites)]
public class Get : EndpointBaseAsync.WithRequest<Query>.WithActionResult<SingleResponse<Response>>
{
    private readonly IMediator _mediator;
    private readonly ILogger<Get> _logger;

    public Get(IMediator mediator, ILogger<Get> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }
        
    [HttpGet("{identifier}")]
    [SwaggerOperation(
        Summary = "Get",
        Description = "Get",
        OperationId = "a858baa0-9318-4f6c-8fc1-c5c6274b82df",
        Tags = new[] { Routes.Websites})
    ]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response))]
    [ProducesErrorResponseType(typeof(BadRequestObjectResult))]
    public override async Task<ActionResult<SingleResponse<Response>>> HandleAsync([FromRoute] Query request, CancellationToken cancellationToken = new())
    {
        var result = await _mediator.Send(request, cancellationToken);
       
        if (result.IsValid)
            return new OkObjectResult(result.Item);
        
        return await HandleErrors(result.Errors);
    }
    
    private Task<ActionResult> HandleErrors(List<KeyValuePair<string, string[]>> errors)
    {
        _logger.LogError("Error Executing {0} - {1}", nameof(Get), errors[0].Key);
        ActionResult result = null;
        errors.ForEach(error =>
        {
            result = error.Key switch
            {
                ErrorKeyNames.Conflict => new ConflictResult(),
                _ => new BadRequestObjectResult(errors)
            };
        });
        return Task.FromResult(result);
    }
}
