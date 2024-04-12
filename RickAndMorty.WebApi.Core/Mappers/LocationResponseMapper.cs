using RickAndMorty.WebApi.Data.Models;
using RickAndMorty.WebApi.Models.Responses.Locations;


namespace RickAndMorty.WebApi.Core.Mappers;
public static class LocationResponseMapper
{
    public static LocationResponse MapLocationEntityToLocationsResponse(this Location location)
    {
        return new LocationResponse
        {
            ExternalId = location.ExternalId,
            Name = location.Name,
            Type = location.Type,
            Dimension = location.Dimension,
            Url = location.Url,
            Created = location.Created
        };
    }
}

