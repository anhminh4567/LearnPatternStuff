using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using StackExchange.Redis;
using System.Collections.Concurrent;
using System.Text.Json;
using TestConceptPattern.Databases.Model;

namespace TestConceptPattern.Caching
{
    public class RedisCachesService : ICacheService
    {
        private static ConcurrentDictionary<string,bool> CacheKeys = new ConcurrentDictionary<string,bool>();
        private readonly IConnectionMultiplexer _connectionMultiplexer;

        public RedisCachesService(IConnectionMultiplexer connectionMultiplexer)
        {
            _connectionMultiplexer = connectionMultiplexer;
        }
        //public async Task<string?> GetCacheValueAsync(string key)
        //{
        //    var db = _connectionMultiplexer.GetDatabase();
        //    return await db.StringGetAsync(key);
        //}
        //public async Task SetCacheValueAsync(string key, string value)
        //{
        //    var db = _connectionMultiplexer.GetDatabase();
        //    await db.StringSetAsync(key, value);
        //}
        //public async Task SetCacheValueStudentAsync(string key, Student student)
        //{
        //    await SetCacheValueClassAsync(key, student);
        //}
        //public async Task SetCacheValueClassAsync<T>(string key, T t)
        //{
        //    var db = _connectionMultiplexer.GetDatabase();
        //    await db.StringSetAsync(key, JsonSerializer.Serialize(t));
        //}

        public async Task<T?> GetAsync<T>(string key, CancellationToken cancellationToken = default) where T : class
        {
            var db = _connectionMultiplexer.GetDatabase();
            var value = await db.StringGetAsync(key);
            if (value.IsNull || value.HasValue is false)
                return null;
            T? returnValue =  JsonConvert.DeserializeObject<T>(value);
            return returnValue;
        }

        public async Task SetAsync<T>(string key, T value, CancellationToken cancellationToken = default) where T : class
        {
            var db = _connectionMultiplexer.GetDatabase();
            string cacheValue = JsonConvert.SerializeObject(value);
            await db.StringSetAsync(key, cacheValue); 
            CacheKeys.TryAdd(key, true); // true false is nto a problem
        }

        public async Task RemoveAsync(string key, CancellationToken cancellationToken = default)
        {
            var db = _connectionMultiplexer.GetDatabase();
            var deletedValue = await db.StringGetDeleteAsync(key);
            CacheKeys.TryRemove(key, out bool _) ;
        }

        public  async Task RemoveByPrefixAsync(string prefix, CancellationToken cancellationToken = default)
        {
            var db =  _connectionMultiplexer.GetDatabase();
            IEnumerable<Task> removeKeysTask = CacheKeys.Keys
                .Where(k => k.StartsWith(prefix))
                .Select(k => db.StringGetDeleteAsync(k)); ;
            await Task.WhenAll(removeKeysTask);
        }
    }
}
