using AutoMapper;

using RickAndMorty.Net.Api.Models.Dto;

using RickAndMortyApiCrawler.Core.Clients.RickAndMortyApi.Models.Responses;

namespace RickAndMortyApiCrawler.Core.Mappers;
public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<CharacterResponseResult, CharacterDto>()
            .ForMember(f => f.ExternalId, opt => opt.MapFrom(f => f.id));

        CreateMap<CharacterLocationResponseResult, CharacterLocationDto>()
            .ForMember(f => f.LinksToResidents, opt => opt.MapFrom(f => f.residents))
            .ForMember(f => f.ExternalId, opt => opt.MapFrom(f => f.id));
    }
}