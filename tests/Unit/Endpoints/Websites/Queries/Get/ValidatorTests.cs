using Api.Activities.Website.Queries.Get;
using FluentValidation.TestHelper;
using Xunit;

namespace Geekiam.Endpoints.Websites.Queries.Get;

public class ValidatorTests
{
    private readonly Validator _validator = new();

    [Fact]
    public void Should_Have_Validation_Error_For_Empty_Identifier()
    {
        var query = new Query { Identifier = string.Empty };
        var result = _validator.TestValidate(query);
        result.ShouldHaveValidationErrorFor(x => x.Identifier);
    }

    [Theory]
    [InlineData("ce4d8f8e-3e02-4689-97f3-84a691507ab4")]
    [InlineData("somerandomestring")]
    [InlineData("f_desk_17890")]
    public void Should_Have_Validation_Error_For_Invalid_Identifier(string identifier)
    {
        var query = new Query { Identifier = identifier };
        var result = _validator.TestValidate(query);
        result.ShouldHaveValidationErrorFor(x => x.Identifier);
    }

    [Theory]
    [InlineData("g_randomstring_0001")]
    [InlineData("g_helloworld_1000")]
    public void Should_Not_Have_Validation_Error_For_valid_Identifier(string identifier)
    {
        var query = new Query { Identifier = identifier };
        var result = _validator.TestValidate(query);
        result.ShouldNotHaveValidationErrorFor(x => x.Identifier);
    }
}