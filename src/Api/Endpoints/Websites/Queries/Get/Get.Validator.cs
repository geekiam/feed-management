using System;
using Common;
using FluentValidation;
using Geekiam;

namespace Api.Activities.Website.Queries.Get;

public class Validator : AbstractValidator<Query>
{
    public Validator()
    {
        RuleFor(x => x.Identifier).Matches(RegularExpressions.IdentifierValidator)
            .WithMessage(ErrorMessages.InvalidIdentifierFormat).NotEmpty();
        
       

    }       
}
