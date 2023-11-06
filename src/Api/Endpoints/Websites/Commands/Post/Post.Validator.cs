using FluentValidation;
namespace Api.Activities.Websites.Commands.Post;

public class Validator : AbstractValidator<Command>
{
    public Validator()
    {
        RuleFor(x => x.Body.Name).NotEmpty();
        RuleFor(x => x.Body.Description).NotEmpty();
        RuleFor(x => x.Body.Domain).NotEmpty();
        
        

    }       
}
