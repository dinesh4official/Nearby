using System;
using System.Collections.Generic;
using NearBy.Models;
using NearByCore.Models;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms.Internals;

namespace NearBy.Helper.Extensions
{
    [Preserve(AllMembers = true)]
    public static class ModelExtensions
    {
        #region Methods

        public static PlaceDetailsInfo ToPlaceDetailsInfo(this PlaceDetails details, Place place)
        {
            return new PlaceDetailsInfo
            {
                AddressDigest = details.AddressDigest,
                Distance = place.Distance.ToDistanceString(),
                FirstPhotoUrl = details.FirstPhotoUrl,
                AverageRating = details.Rating,
                NumberOfRatings = details.NumberOfRatings,
                Name = details.Name,
                PhoneNumber = details.PhoneNumber,
                PriceLevel = details.PriceLevel
            };
        }

        public static List<MapInfo> ToMapInfoList(this ObservableRangeCollection<Place> places)
        {
            List<MapInfo> mapInfos = new List<MapInfo>();
            foreach (Place place in places)
            {
                mapInfos.Add(ToMapInfo(place));
            }

            return mapInfos;
        }

        static MapInfo ToMapInfo(this Place place)
        {
            return new MapInfo
            {
                Label = place.Name,
                Position = place.Position,
                Address = place.Vicinity
            };
        }

        #endregion
    }
}
