using BugTrackerUICore.Interfaces;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTrackerUICore.Services
{
    public class CachingService : ICachingService
    {
        private readonly IDistributedCache _cache;
        public CachingService(IDistributedCache cache)
        {
            _cache = cache;
        }

        public bool ExistsinCache(string key, string data)
        {
            
            return _cache.GetString(key).Equals(null) || _cache.GetString(key).Equals(data);
            
        }

        public string GetFromDistributedCache(string key)
        {
            byte[] encodedData = _cache.Get(key);

            if(encodedData == null)
            {
                return "";
            }

            return String.IsNullOrEmpty(Encoding.UTF8.GetString(encodedData)) ? "" : Encoding.UTF8.GetString(encodedData);
        }

        public void SetInDistributedCache(string key, string data)
        {
            byte[] encodedData = Encoding.UTF8.GetBytes(data);

            var cacheOptions = new DistributedCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromMinutes(30));

            _cache.Set(key, encodedData, cacheOptions);
            
        }
    }
}
