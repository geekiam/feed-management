using Geekiam.Websites;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Threenine.ApiResponse;
namespace Api.Activities.Websites.Commands.Post;

public class Command : IRequest<SingleResponse<Response>>
{
        [FromBody] public Listing Body { get; set; }
}


