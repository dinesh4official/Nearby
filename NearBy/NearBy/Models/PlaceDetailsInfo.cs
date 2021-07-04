using System;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms.Internals;

namespace NearBy.Models
{
    [Preserve(AllMembers = true)]
    public class PlaceDetailsInfo : ObservableObject
    {
        #region Fields

        string _fAddressDigest, _fPhoto, _fName,
            _fNumber, _fdistance;

        long _fRatings;

        int _fPrice;

        decimal _fAverageRating;

        #endregion

        #region Constructor

        public PlaceDetailsInfo()
        {

        }

        #endregion

        #region Properties

        public string AddressDigest
        {
            get => _fAddressDigest;
            set
            {
                _fAddressDigest = value;
                OnPropertyChanged(nameof(AddressDigest));
            }
        }

        public string FirstPhotoUrl
        {
            get => _fPhoto;
            set
            {
                _fPhoto = value;
                OnPropertyChanged(nameof(FirstPhotoUrl));
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

        public string PhoneNumber
        {
            get => _fNumber;
            set
            {
                _fNumber = value;
                OnPropertyChanged(nameof(PhoneNumber));
            }
        }

        public long NumberOfRatings
        {
            get => _fRatings;
            set
            {
                _fRatings = value;
                OnPropertyChanged(nameof(NumberOfRatings));
            }
        }

        public int PriceLevel
        {
            get => _fPrice;
            set
            {
                _fPrice = value;
                OnPropertyChanged(nameof(PriceLevel));
            }
        }

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
            get => _fdistance;
            set
            {
                _fdistance = value;
                OnPropertyChanged(nameof(Distance));
            }
        }

        #endregion
    }
}