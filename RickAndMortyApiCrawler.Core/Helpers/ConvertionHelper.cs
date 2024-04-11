
using RickAndMortyApiCrawler.Core.Clients.RickAndMortyApi.Models.Responses;

namespace RickAndMortyApiCrawler.Core.Helpers;

public class ConversionHelper
{
    public static async Task<T?> ConvertResponseToObjectAsync<T>(HttpResponseMessage httpResponseMessage)
    {
        var content = await httpResponseMessage.Content.ReadAsStringAsync();
        var result = System.Text.Json.JsonSerializer.Deserialize<T>(content);

        return result;
    }
}