using AutoMapper;

namespace RickAndMorty.WebApi.Core.Mappers;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        //CreateMap<AddProductRequest, Product>()
        //    .ForMember(f => f.Price, opt => opt.MapFrom(src => decimal.Parse(src.Price.ToString(), new CultureInfo("nl-NL"))));
    }
}
