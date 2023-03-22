using System.Text.Json;
using Microsoft.Extensions.Caching.Distributed;

namespace WebApi.Helpers;
public static class CacheHelper
    {
        public static async Task SetRecordAsync<T>(this IDistributedCache cache,
                                                   string recordId,
                                                   T data,
                                                   TimeSpan? absoluteExpireTime = null,
                                                   TimeSpan? slidingExpireTime = null)
        {
            var options = new DistributedCacheEntryOptions();

            //options.AbsoluteExpirationRelativeToNow = absoluteExpireTime ?? TimeSpan.FromSeconds(60);
            //options.SlidingExpiration = slidingExpireTime;
            var jsonOptions = new JsonSerializerOptions
            {
                ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve
            };
            var jsonData = JsonSerializer.Serialize(data, jsonOptions);
            await cache.SetStringAsync(recordId, jsonData, options);
        }

        public static async Task<T> GetRecordAsync<T>(this IDistributedCache cache,
                                                       string recordId)
        {
            var jsonData = await cache.GetStringAsync(recordId);

            if (jsonData is null)
            {
                return default(T);
            }
            var jsonOptions = new JsonSerializerOptions
            {
                ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve
            };
            return JsonSerializer.Deserialize<T>(jsonData, jsonOptions);
        }

        public static async Task RemoveRecordAsync<T>(this IDistributedCache cache,
                                                   string recordId)
        {
            await cache.RemoveAsync(recordId);
        }
    }