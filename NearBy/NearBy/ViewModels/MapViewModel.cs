using System;
using System.Collections.Generic;
using NearBy.Models;
using NearByCore.Models;
using NearBy.Helper.Extensions;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms.Internals;

namespace NearBy.ViewModels
{
    [Preserve(AllMembers = true)]
    public class MapViewModel : ObservableObject
    {
        #region Fields

        Position _fposition;
        bool _fIsBusy;

        #endregion

        #region Constructor

        public MapViewModel(ObservableRangeCollection<Place> _places, Position _position)
        {
            _fposition = _position;
            IsBusy = true;
            UpdateData(_places);
        }

        #endregion

        #region Properties

        public List<MapInfo> MapInfos { get; set; }

        public Position UserPosition => _fposition;

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

        #region Method

        void UpdateData(ObservableRangeCollection<Place> places)
        {
            MapInfos = places.ToMapInfoList();
            IsBusy = false;
        }

        #endregion
    }
}
