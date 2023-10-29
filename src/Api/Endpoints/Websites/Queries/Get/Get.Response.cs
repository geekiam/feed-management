using System;
using Azure.Core;
using Domain;

namespace Api.Activities.Website.Queries.Get;

public class Response
{
    public string Name { get; set; }
    public string Domain { get; set; }
    public string Description { get; set; }
}
