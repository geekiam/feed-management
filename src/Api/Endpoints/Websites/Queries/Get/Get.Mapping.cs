using AutoMapper;
using Domain;
using Models;
namespace Api.Activities.Website.Queries.Get;

public class Mapping : Profile
{
    public Mapping()
    {
        CreateMap<Geekiam.Websites.Website, Response>(MemberList.None)
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.Domain, opt => opt.MapFrom(src => src.Domain))
            .ForMember(dest => dest.Feeds , opt => opt.MapFrom(src => src.Feeds))
            .ForMember(dest => dest.Categories, opt => opt.MapFrom(src => src.Categories));

        CreateMap<Domain.Feed, Feed>(MemberList.None)
            .ForMember(dest => dest.Media, opt => opt.MapFrom(src => src.Type))
            .ForMember(dest => dest.Path, opt => opt.MapFrom(src => src.Path));
    }
}
