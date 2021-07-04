using System;
using System.Threading.Tasks;
using Android.Runtime;

namespace NearByCore.Interfaces
{
    [Preserve(AllMembers = true)]
    public interface ICacheHelper
    {
        #region Methods

        Task<T> GetFromMemoryAsync<T>(string key) where T : class, new();

        Task<bool> RemoveFromMemoryAsync(string key);

        Task<bool> StoreInMemoryAsync<T>(string key, T objectToStore) where T : class, new();

        #endregion
    }
}
