using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RickAndMorty.Shared.Services.Abstractions;
public interface ICacheService
{
    T SetObjectInCache<T>(string cacheKey, int slidingExpirationInSeconds, int absoluteExpirationInMinutes, T value) where T : class;
    T? GetObjectFromCache<T>(string cacheKey) where T : class;
}
