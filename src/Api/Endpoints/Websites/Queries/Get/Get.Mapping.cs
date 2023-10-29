using AutoMapper;
using Domain;
using Models;


namespace Api.Activities.Website.Queries.Get;

public class Mapping : Profile
{
    public Mapping()
    {
        CreateMap<Domain.Websites.Website, Response>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.Domain, opt => opt.MapFrom(src => src.Domain));

        CreateMap<Feeds, Feed>()
            .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.MediaType.Name))
            .ForMember(dest => dest.Path, opt => opt.MapFrom(src => src.Path));
    }
    
 
}
