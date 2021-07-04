using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Autofac;
using NearBy.Helper.Extensions;
using NearBy.Helper.Interfaces;
using NearBy.Helper.Utils;
using NearBy.Models;
using NearBy.Views;
using NearByCore;
using NearByCore.Constants;
using NearByCore.Extensions;
using NearByCore.Interfaces;
using NearByCore.Models;
using NearByCore.Resources;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace NearBy.ViewModels
{
    [Preserve(AllMembers = true)]
    public class PlacesViewModel : ObservableObject, IPlacesViewModel
    {
        #region Fields

        readonly string SortDistanceAscending = AppResources.SortByDistance + AppResources.Ascending;
        readonly string SortDistanceDescending = AppResources.SortByDistance + AppResources.Descending;
        readonly string SortRatingAscending = AppResources.SortByRating + AppResources.Ascending;
        readonly string SortRatingDescending = AppResources.SortByRating + AppResources.Descending;

        readonly ObservableRangeCollection<Place> _fPlaces;
        readonly IPlacesService _fPlacesService;

        bool _fSortedByDistance, _fOrderByDistance, _fSortedByRating,
            _fOrderByRating, _fIsBusy;
        Position _fPosition;

        #endregion

        #region Constructor

        public PlacesViewModel()
        {
            _fPlacesService = AppContainer.Current.Resolve<IPlacesService>();
            _fPlacesService.ApiKey = APIConstants.GooglePlacesApiKey;
            _fPlaces = new ObservableRangeCollection<Place>();
            ShowSortMenu = new Command(async () => await ShowSortMenuAsync());
            LoadItemsInPage = new Command(async () => await LoadItemsAsync());
            ShowOnMap = new Command(async () => await ShowOnMapAsync());
            ItemSelected = new Command<PlacesInfo>(async (item) => await OnItemSelected(item));
            PlacesInfos = new ObservableRangeCollection<PlacesInfo>();
            _ = LoadItemsAsync();
        }

        #endregion

        #region Properties

        public int ItemsThreshold { get; set; } = 0;

        public ICommand LoadItemsInPage { get; }

        public ObservableRangeCollection<PlacesInfo> PlacesInfos { get; }

        public ICommand ShowOnMap { get; }

        public ICommand ShowSortMenu { get; }

        public ICommand ItemSelected { get; }

        public bool IsBusy
        {
            get => _fIsBusy;
            set
            {
                if (_fIsBusy == value) { return; }

                _fIsBusy = value;
                OnPropertyChanged(nameof(IsBusy));
            }
        }

        #endregion

        #region Load Pages

        async Task LoadItemsAsync()
        {
            if (IsBusy) { return; }

            IsBusy = true;

            if (ItemsThreshold <= 0)
            {
                bool isGranted = await ValidateLocationPermission();
                if (isGranted)
                {
                    await LoadFirstPageAsync();
                }
            }
            else
            {
                await LoadNextPageAsync();
            }

            ItemsThreshold = PlacesInfos.Count <= 1 ? 0 : PlacesInfos.Count - 1;
        }

        async Task LoadFirstPageAsync()
        {
            try
            {
                var currentPosition = await GetCurrentPosition();
                if (currentPosition.IsNull()) { return; }

                _fPosition = currentPosition.Value;
                var places = await _fPlacesService.GetNearbyPlaceData(_fPosition, 8047);

                if (places.IsEmpty()) { return; }

                GetValuesFromPreferences();
                _fPlaces.ReplaceRange(places);
                await ArrangePlaceDisplays(places, true);
            }
            finally
            {
                IsBusy = false;
            }
        }

        async Task LoadNextPageAsync()
        {
            try
            {
                var places = await _fPlacesService.GetNextPage();

                if (places.IsEmpty()) { return; }

                _fPlaces.AddRange(places);
                await ArrangePlaceDisplays(places, false);
            }
            finally
            {
                IsBusy = false;
            }
        }

        #endregion

        #region Item Selected

        public async Task<PlaceDetailsInfo> GetDetails(PlacesInfo placesInfo)
        {
            var place = _fPlaces.FirstOrDefault(p => p.PlaceId == placesInfo.PlaceId);

            return place.IsNull() ? null : (await _fPlacesService.GetPlaceDetails(place.PlaceId)).ToPlaceDetailsInfo(place);
        }

        async Task OnItemSelected(PlacesInfo item)
        {
            PlaceDetailsInfo detailsInfo = await GetDetails(item);

            if (detailsInfo.IsNull()) { return; }

            await App.Current.MainPage.Navigation.PushAsync(new PlaceDetailPage()
            {
                BindingContext = detailsInfo
            });
           
        }

        #endregion

        #region Sorting

        async Task ShowSortMenuAsync()
        {
            await Task.Delay(0);

            if (_fPlaces.IsEmpty()) { return; }

            string result = await AppUtils.ShowActionSheet(AppResources.Sort,
                SortDistanceAscending, SortDistanceDescending, SortRatingAscending, SortRatingDescending);

            if (result == SortDistanceAscending || result == SortDistanceDescending)
            {
                _fOrderByDistance = result == SortDistanceAscending;
                await SortByDistance();
            }
            else if (result == SortRatingAscending || result == SortRatingDescending)
            {
                _fOrderByRating = result == SortRatingAscending;
                await SortByRating();
            }
        }

        async Task SortByDistance()
        {
            await Task.Delay(0);

            _fSortedByRating = false;
            _fSortedByDistance = true;

            var sortPlaces = _fOrderByDistance ? _fPlaces.OrderBy(f => f.Distance) :
                         _fPlaces.OrderByDescending(f => f.Distance);

            AppPreference.StoreBoolValue(AppResources.OrderByDistance, _fOrderByDistance);
            AppPreference.StoreBoolValue(AppResources.SortByRating, _fSortedByRating);
            AppPreference.StoreBoolValue(AppResources.SortByDistance, _fSortedByDistance);
            UpdatePlaceDisplays(sortPlaces.ToList(), true);
        }

        async Task SortByRating()
        {
            await Task.Delay(0);
            _fSortedByRating = true;
            _fSortedByDistance = false;

            var sortPlaces = _fOrderByRating ? _fPlaces.OrderBy(f => f.Rating) :
                _fPlaces.OrderByDescending(f => f.Rating);

            AppPreference.StoreBoolValue(AppResources.OrderByRating, _fOrderByRating);
            AppPreference.StoreBoolValue(AppResources.SortByRating, _fSortedByRating);
            AppPreference.StoreBoolValue(AppResources.SortByDistance, _fSortedByDistance);
            UpdatePlaceDisplays(sortPlaces.ToList(), true);
        }

        async Task ArrangePlaceDisplays(List<Place> places, bool needToReplace)
        {
            if (_fSortedByDistance)
            {
                await SortByDistance();
            }
            else if (_fSortedByRating)
            {
                await SortByRating();
            }
            else
            {
                UpdatePlaceDisplays(places, needToReplace);
            }
        }

        void UpdatePlaceDisplays(List<Place> places, bool needToReplace)
        {
            var items = places.Select(GetPlacesInfo);
            if (needToReplace)
            {
                PlacesInfos.ReplaceRange(items);
            }
            else
            {
                PlacesInfos.AddRange(items);
            }
        }

        #endregion

        #region Show Map

        async Task ShowOnMapAsync()
        {
            await Task.Delay(0);

            if (_fPlaces.IsEmpty()) { return; }

            bool isGranted = await ValidateLocationPermission();
            if (isGranted)
            {
                var currentPosition = await GetCurrentPosition();
                if (currentPosition.IsNull()) { return; }

                await App.Current.MainPage.Navigation.PushAsync(new MapViewPage()
                {
                    BindingContext = new MapViewModel(_fPlaces, currentPosition.Value)
                });
            }
        }

        async Task<Position?> GetCurrentPosition()
        {
            try
            {
                var currentPosition = await Geolocation.GetLocationAsync();
                if (currentPosition.IsNull())
                {
                    AppUtils.ShowAlert(AppResources.UnableToFindLocation, false);
                    return null;
                }

                return new Position(currentPosition.Latitude, currentPosition.Longitude);
            }
            catch
            {
                AppUtils.ShowAlert(AppResources.UnableToFindLocation, false);
                return null;
            }
        }

        #endregion

        #region Preferences

        void GetValuesFromPreferences()
        {
            _fSortedByRating = AppPreference.GetBoolValue(AppResources.SortByRating);
            _fSortedByDistance = AppPreference.GetBoolValue(AppResources.SortByDistance);
            _fOrderByDistance = AppPreference.GetBoolValue(AppResources.OrderByDistance);
            _fOrderByRating = AppPreference.GetBoolValue(AppResources.OrderByRating);
        }

        #endregion

        #region Static Methods

        static PlacesInfo GetPlacesInfo(Place place)
        {
            return new PlacesInfo
            {
                Distance = place.Distance.ToDistanceString(),
                Name = place.Name,
                PlaceId = place.PlaceId,
                AverageRating = place.Rating,
                ShowStar = place.Rating > 0,
                ImageUrl = place.ImageUrl,
                OpenNow = place.OpenNow
            };
        }

        #endregion

        #region Setup

        async Task<bool> ValidateLocationPermission()
        {
            PermissionStatus permissionStatus = await PermissionUtils.Instance.CheckAndRequestLocationPermission();
            if (permissionStatus != PermissionStatus.Granted)
            {
                AppUtils.ShowAlert(AppResources.LocationPermissionDenied, false);
                return false;
            }
            else
            {
                return true;
            }
        }

        #endregion
    }
}