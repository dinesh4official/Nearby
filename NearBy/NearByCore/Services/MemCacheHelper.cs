using System;
using System.Threading.Tasks;
using Android.Runtime;
using NearByCore.Interfaces;
using System.Collections.Concurrent;

namespace NearByCore.Services
{
    [Preserve(AllMembers = true)]
    public class MemCacheHelper : ICacheHelper
    {
        #region Fields

        private readonly ConcurrentDictionary<string, object> _fCachedData =
            new ConcurrentDictionary<string, object>();

        #endregion

        #region Methods

        public async Task<T> GetFromMemoryAsync<T>(string key) where T : class, new()
        {
            await Task.Delay(0);

            return _fCachedData.TryGetValue(key, out object memObject)
                ? (T)memObject
                : default;
        }

        public async Task<bool> RemoveFromMemoryAsync(string key)
        {
            await Task.Delay(0);

            if (_fCachedData.TryRemove(key, out _)) { return true; }

            return !_fCachedData.ContainsKey(key);
        }

        public async Task<bool> StoreInMemoryAsync<T>(string key, T objectToStore) where T : class, new()
        {
            await Task.Delay(0);

            return _fCachedData.TryAdd(key, objectToStore);
        }

        #endregion
    }
}
