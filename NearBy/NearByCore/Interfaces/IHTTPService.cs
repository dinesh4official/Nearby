using System;
using System.Threading.Tasks;
using Android.Runtime;

namespace NearByCore.Interfaces
{
    [Preserve(AllMembers = true)]
    public interface IHTTPService
    {
        #region Methods

        Task<T> GetAsync<T>(string url);

        #endregion
    }
}