using System;
using System.Threading.Tasks;
using Android.Runtime;
using NearByCore.Constants;
using NearByCore.Extensions;
using NearByCore.Interfaces;
using NearByCore.Models;

namespace NearByCore.Repositories
{
    [Preserve(AllMembers = true)]
    public class PlacesRepository : IPlacesRepository
    {
        #region Fields

        private readonly ICacheHelper _fCacheHelper;
        private readonly IHTTPService _fHttpHelper;

        #endregion

        #region Constructors

        public PlacesRepository(IHTTPService httpHelper, ICacheHelper cacheHelper)
        {
            _fHttpHelper = httpHelper;
            _fCacheHelper = cacheHelper;
        }

        #endregion

        #region Methods

        public async Task<DetailsResponse> GetDetails(string placeId, string apikey)
        {
            if (apikey.IsBlank()) { return null; }

            var url = $"https://maps.googleapis.com/maps/api/place/details/json?placeid={placeId}&key={apikey}";
            var memkey = $"details_{placeId}";

            var storedPlace = await _fCacheHelper.GetFromMemoryAsync<DetailsResponse>(memkey);

            if (storedPlace.IsNotNull()
                && storedPlace.RetrievedDate >= DateTime.Now - TimeSpan.FromMinutes(30))
            {
                return storedPlace;
            }

            var response = await _fHttpHelper.GetAsync<DetailsResponse>(url);

            if (response.IsNull()) { return response; }

            response.RetrievedDate = DateTime.Now;

            await _fCacheHelper.StoreInMemoryAsync(memkey, response);

            return response;
        }

        public async Task<PlacesResponse> GetPlaces(Position position, double radius, string apiKey,
            string nextPageToken = null)
        {
            if (apiKey.IsBlank()) { return null; }

            var memkey = $"{position.Latitude}_{position.Longitude}_{radius}";
            memkey = nextPageToken.IsBlank() ? memkey : $"{memkey}_{nextPageToken}";

            try
            {
                var storedPlace = await _fCacheHelper.GetFromMemoryAsync<PlacesResponse>(memkey);

                if (storedPlace.IsNotNull()
                    && storedPlace.RetrievedDate >= DateTime.Now - TimeSpan.FromMinutes(3))
                {
                    return storedPlace;
                }


                var url = nextPageToken.IsBlank()
                    ? BuildPlacesUrl(position, radius, apiKey)
                    : BuildPagingUrl(position, radius, nextPageToken, apiKey);

                var response = await _fHttpHelper.GetAsync<PlacesResponse>(url);

                if (response.IsNull()) { return response; }

                response.RetrievedDate = DateTime.Now;

                response.Results?.RemoveAll(r => ContainsLocality(r));

                await _fCacheHelper.StoreInMemoryAsync(memkey, response);

                return response;
            }
            catch
            {
                return null;
            }
        }

        private string BuildPagingUrl(Position position, double radius, string nextToken, string apiKey)
        {
            var url = BuildPlacesUrl(position, radius, apiKey);
            return $"{url}&pagetoken={nextToken}";
        }

        private string BuildPlacesUrl(Position position, double radius, string apiKey)
        {
            var url = "https://maps.googleapis.com/maps/api/place/nearbysearch/json?";
            return $"{url}location={position.Latitude},{position.Longitude}&radius={radius}&key={apiKey}";
        }

        private bool ContainsLocality(PlaceResult placeResult)
        {
            return placeResult.PlaceTypes.Contains(AppConstants.Locality) ||
                   placeResult.PlaceTypes.Contains(AppConstants.SubLocality) ||
                   placeResult.PlaceTypes.Contains(AppConstants.Neighborhood);
        }

        #endregion
    }
}
