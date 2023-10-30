using Api.Activities.Website.Queries.Get;
using AutoMapper;
using FizzWare.NBuilder;
using Geekiam.Websites.Get;
using Shouldly;
using Xunit;
using Feed = Domain.Feed;


namespace Geekiam.Endpoints.Websites.Queries.Get;

public class MappingTests
{
    private readonly IMapper _mapper;

    public MappingTests()
    {
        var mapperConfiguration = new MapperConfiguration(configuration => configuration.AddProfile<Mapping>());
        mapperConfiguration.AssertConfigurationIsValid();
        _mapper = mapperConfiguration.CreateMapper();
    }

    private  Website TestWebsite =>  new Website("Test", "test.com", "test website")
        {
            Feeds = Builder<Domain.Feed>.CreateListOfSize(2).Build().ToList()
        };
      

    
  
    

    [Fact]
    public void Should_map_Actor_to_Response()
    {
        var response = _mapper.Map<Response>(TestWebsite);

      

      
        response.ShouldSatisfyAllConditions(
            _ =>  response.ShouldBeOfType<Response>(),
            _ => _.Description.ShouldBeEquivalentTo(TestWebsite.Description),
            _ => _.Domain.ShouldBeEquivalentTo(TestWebsite.Domain),
            _ => _.Feeds.Count.ShouldBeEquivalentTo(TestWebsite.Feeds.Count)
            );
    } 
}