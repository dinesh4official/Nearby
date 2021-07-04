using System;
using System.Threading.Tasks;
using Android.Runtime;
using NearByCore.Models;

namespace NearByCore.Interfaces
{
    [Preserve(AllMembers = true)]
    public interface IPlacesRepository
    {
        #region Methods

        Task<DetailsResponse> GetDetails(string placeId, string apikey);

        Task<PlacesResponse> GetPlaces(Position position, double radius, string apiKey, string nextPageToken = null);

        #endregion
    }
}
