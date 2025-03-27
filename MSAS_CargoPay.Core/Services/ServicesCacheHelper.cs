using Microsoft.Extensions.Caching.Memory;
using MSAS_CargoPay.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSAS_CargoPay.Core.Services
{
    public class ServicesCacheHelper : IServicesCacheHelper
    {
        private readonly IMemoryCache _memoryCache;
        public ServicesCacheHelper(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }
        public T GetFromCache<T>(string key) where T : class
        {
            _memoryCache.TryGetValue(key, out T cachedResponse);
            return cachedResponse;
        }
        public void SetCache<T>(string key, T value) where T : class
        {
            _memoryCache.Set(key, value);
        }
        public void SetCache<T>(string key, T value, DateTimeOffset duration) where T : class
        {
            _memoryCache.Set(key, value, duration);
        }
        public void SetCache<T>(string key, T value, MemoryCacheEntryOptions options) where T : class
        {
            _memoryCache.Set(key, value, options);
        }
        public void ClearCache(string key)
        {
            _memoryCache.Remove(key);
        }
    }
}
