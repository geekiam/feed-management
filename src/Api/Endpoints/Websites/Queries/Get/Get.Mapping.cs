using AutoMapper;
using Domain;
using Models;


namespace Api.Activities.Website.Queries.Get;

public class Mapping : Profile
{
    public Mapping()
    {
        CreateMap<Sources, Site>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.Domain, opt => opt.MapFrom(src => src.ToString()))
            .ForMember(dest => dest.Feeds, opt => opt.MapFrom(src => src.Feeds))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.Name));

        CreateMap<Feeds, Feed>()
            .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.MediaType.Name))
            .ForMember(dest => dest.Path, opt => opt.MapFrom(src => src.Path));
    }
    
 
}
