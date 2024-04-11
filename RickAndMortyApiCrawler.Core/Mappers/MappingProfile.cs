﻿using AutoMapper;

using RickAndMorty.Net.Api.Models.Dto;
using RickAndMorty.Net.Api.Models.Responses;

namespace RickAndMortyApiCrawler.Core.Mappers;
public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<CharacterResponse, CharacterDto>();
        CreateMap<CharacterLocationResponseResult, CharacterLocationDto>()
            .ForMember(f => f.LinksToResidents, opt => opt.MapFrom(f => f.residents))
            .ForMember(f => f.ExternalId, opt => opt.MapFrom(f => f.id));
        CreateMap<CharacterOriginResponse, CharacterOriginDto>();
    }
}