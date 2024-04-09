using AutoMapper;

using RickAndMorty.Net.Api.Models.Dto;
using RickAndMorty.Net.Api.Models.Responses;

using System.Globalization;

namespace RickAndMortyApiCrawler.Core.Mappers;
public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<CharacterResponse, CharacterDto>();
        CreateMap<CharacterLocationResponse, CharacterLocationDto>();
        CreateMap<CharacterOriginResponse, CharacterOriginDto>();
    }
}