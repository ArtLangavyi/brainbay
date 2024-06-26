﻿using System.Text.Json;
using System.Text;

namespace RickAndMorty.Shared;

public class ConversionHelper
{
    public static async Task<T?> ConvertResponseToObjectAsync<T>(HttpResponseMessage httpResponseMessage)
    {
        var content = await httpResponseMessage.Content.ReadAsStringAsync();
        var result = System.Text.Json.JsonSerializer.Deserialize<T>(content);

        return result;
    }

    public static HttpContent CreateJsonHttpContent<T>(T dataObject)
    {
        var json = JsonSerializer.Serialize(dataObject);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        return content;
    }
}