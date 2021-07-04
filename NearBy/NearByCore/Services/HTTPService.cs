using System;
using System.Net.Http;
using System.Threading.Tasks;
using Android.Runtime;
using NearByCore.Constants;
using NearByCore.Extensions;
using NearByCore.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace NearByCore.Services
{
    [Preserve(AllMembers = true)]
    public class HTTPService : IHTTPService
    {
        #region Interface Methods

        public async Task<T> GetAsync<T>(string url)
        {
            using (var client = new HttpClient())
            using (var request = new HttpRequestMessage(HttpMethod.Get, url))
            using (var response = await client.SendAsync(request))
            {
                if (!response.IsSuccessStatusCode)
                {
                    return default;
                }

                var result = await response.Content.ReadAsStringAsync();

                if (result.IsBlank()) { return default; }

                if (result != AppConstants.Error)
                {
                    return JsonConvert.DeserializeObject<T>(result, new JsonSerializerSettings
                    {
                        Error = HandleDeserializationError
                    });
                }

                return default;
            }
        }

        #endregion

        #region Methods

        void HandleDeserializationError(object sender, ErrorEventArgs e)
        {
            e.ErrorContext.Handled = true;
        }

        #endregion
    }
}
