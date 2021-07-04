using System;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms.Internals;

namespace NearBy.Models
{
    [Preserve(AllMembers = true)]
    public class PlacesInfo : ObservableObject
    {
        #region Fields

        string _fDistance, _fImageUrl, _fName, _fPlaceId;

        decimal _fAverageRating;

        bool _fShowStar;

        bool? _fOpenNow;

        #endregion

        #region Constructor

        public PlacesInfo()
        {

        }

        #endregion

        #region Properties

        public decimal AverageRating
        {
            get => _fAverageRating;
            set
            {
                _fAverageRating = value;
                OnPropertyChanged(nameof(AverageRating));
            }
        }

        public string Distance
        {
            get => _fDistance;
            set
            {
                _fDistance = value;
                OnPropertyChanged(nameof(Distance));
            }
        }

        public string ImageUrl
        {
            get => _fImageUrl;
            set
            {
                _fImageUrl = value;
                OnPropertyChanged(nameof(ImageUrl));
            }
        }

        public string Name
        {
            get => _fName;
            set
            {
                _fName = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        public string PlaceId
        {
            get => _fPlaceId;
            set
            {
                _fPlaceId = value;
                OnPropertyChanged(nameof(PlaceId));
            }
        }

        public bool ShowStar
        {
            get => _fShowStar;
            set
            {
                _fShowStar = value;
                OnPropertyChanged(nameof(ShowStar));
            }
        }

        public bool? OpenNow
        {
            get => _fOpenNow;
            set
            {
                _fOpenNow = value;
                OnPropertyChanged(nameof(OpenNow));
            }
        }

        #endregion
    }
}