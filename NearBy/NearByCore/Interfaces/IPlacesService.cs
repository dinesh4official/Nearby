using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Android.Runtime;
using NearByCore.Models;

namespace NearByCore.Interfaces
{
    [Preserve(AllMembers = true)]
    public interface IPlacesService
    {
        #region Properties

        string ApiKey { get; set; }

        #endregion

        #region Methods

        Task<List<Place>> GetNearbyPlaceData(Position position, double radius);

        Task<List<Place>> GetNextPage();

        Task<PlaceDetails> GetPlaceDetails(string placeId);

        #endregion
    }
}
