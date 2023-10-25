using System.Collections.Generic;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Threenine.ApiResponse;

namespace Api.Activities.Website.Queries.Get;

public class Query : IRequest<SingleResponse<Response>>
{
    [FromRoute(Name = "identifier")] public string Identifier { get; set; }
        
}


