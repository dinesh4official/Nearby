using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Android.Runtime;
using NearByCore.Extensions;
using NearByCore.Interfaces;
using NearByCore.Models;

namespace NearByCore.Services
{
    [Preserve(AllMembers = true)]
    public class PlacesService : IPlacesService
    {
        #region Fields

        private readonly IPlacesRepository _fPlacesRepository;
        private Position _fCurrentPosition;
        private PlacesResponse _fLastResponse;
        private double _fRadius;

        #endregion

        #region Constructors

        public PlacesService(IPlacesRepository placesRepository)
        {
            _fPlacesRepository = placesRepository;
        }

        #endregion

        #region Properties

        public string ApiKey { get; set; }

        #endregion

        #region Methods

        public async Task<List<Place>> GetNearbyPlaceData(Position position, double radius)
        {
            if (ApiKey.IsBlank()) { return new List<Place>(); }

            _fCurrentPosition = position;
            _fRadius = radius;

            _fLastResponse = await _fPlacesRepository.GetPlaces(position, radius, ApiKey);
            return _fLastResponse.Results.Select(p => BuildPlace(p, position)).ToList();
        }

        public async Task<List<Place>> GetNextPage()
        {
            if (_fLastResponse?.NextPageToken.IsBlank() ?? true)
            {
                return new List<Place>();
            }

            _fLastResponse =
                await _fPlacesRepository.GetPlaces(_fCurrentPosition, _fRadius, ApiKey, _fLastResponse.NextPageToken);

            return _fLastResponse.Results.Select(p => BuildPlace(p, _fCurrentPosition)).ToList();
        }

        public async Task<PlaceDetails> GetPlaceDetails(string placeId)
        {
            await Task.Delay(0);

            var detailsResponse = await _fPlacesRepository.GetDetails(placeId, ApiKey);

            if (detailsResponse.IsNull()) return null;

            var result = detailsResponse.Result;

            var photoUrls = result.Photos?.Select(GetImageUrl).ToList();
            return new PlaceDetails
            {
                AddressDigest = result.FormattedAddress,
                Rating = result.Rating,
                FirstPhotoUrl = photoUrls?.FirstOrDefault() ?? string.Empty,
                Name = result.Name,
                NumberOfRatings = result.Reviews?.Count ?? 0,
                PlaceId = result.PlaceId,
                PriceLevel = result.PriceLevel,
                PhotoUrls = photoUrls ?? new List<string>(),
                PlaceTypes = result.Types,
                PhoneNumber = result.FormattedPhoneNumber
            };
        }

        private static double DegreesToRadians(double deg)
        {
            return deg * Math.PI / 180.0;
        }

        private static Position GetPosition(PlaceResult placeResult)
        {
            return new Position(placeResult.Geometry.Location.Latitude, placeResult.Geometry.Location.Longitude);
        }

        private static double RadiansToDegrees(double rad)
        {
            return rad / Math.PI * 180.0;
        }

        private static bool? GetStatus(PlaceResult placeResult)
        {
            bool? openNow = null;
            if (placeResult.OpeningHours.IsNotNull())
            {
                openNow = placeResult.OpeningHours.OpenNow;
            }

            return openNow;
        }

        private Place BuildPlace(PlaceResult placeResult, Position startingPosition)
        {
            var details = new Place
            {
                Distance = CalculateDistance(startingPosition, placeResult.Geometry.Location),
                ImageUrl = GetFirstImageUrl(placeResult.Photos),
                Name = placeResult.Name,
                PlaceId = placeResult.PlaceId,
                Position = GetPosition(placeResult),
                PlaceTypes = placeResult.PlaceTypes,
                Rating = placeResult.Rating,
                OpenNow = GetStatus(placeResult),
                Vicinity = placeResult.Vicinity
            };

            return details;
        }

        private double CalculateDistance(Position startingPosition, Location location)
        {
            return CalculateDistance(startingPosition.Latitude, startingPosition.Longitude, location.Latitude,
                location.Longitude);
        }

        //Formula from http://www.geodatasource.com/developers/c-sharp
        private double CalculateDistance(double lat1, double lon1, double lat2, double lon2)
        {
            var theta = lon1 - lon2;
            var dist = Math.Sin(DegreesToRadians(lat1)) * Math.Sin(DegreesToRadians(lat2)) +
                       Math.Cos(DegreesToRadians(lat1)) * Math.Cos(DegreesToRadians(lat2)) *
                       Math.Cos(DegreesToRadians(theta));

            dist = Math.Acos(dist);
            dist = RadiansToDegrees(dist);
            dist = dist * 60 * 1.1515;

            return dist;
        }

        private string GetFirstImageUrl(IEnumerable<PlacesPhoto> photos)
        {
            var photo = photos?.FirstOrDefault();

            return GetImageUrl(photo);
        }

        private string GetImageUrl(PlacesPhoto photo)
        {
            const string baseUrl = "https://maps.googleapis.com/maps/api/place/photo";
            return photo.IsNull()
                ? string.Empty
                : $"{baseUrl}?key={ApiKey}&maxwidth=1200&maxheight=1200&photoreference={photo.PhotoReferenceId}";
        }

        #endregion
    }
}
