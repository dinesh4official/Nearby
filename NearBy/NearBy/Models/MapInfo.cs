using System;
using NearByCore.Models;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms.Internals;

namespace NearBy.Models
{
    [Preserve(AllMembers = true)]
    public class MapInfo : ObservableObject
    {
        #region Fields

        string _fLabel, _fAddress;
        Position _fPosition;

        #endregion

        #region Constructor

        public MapInfo()
        {

        }

        #endregion

        #region Properties

        public string Label
        {
            get => _fLabel;
            set
            {
                _fLabel = value;
                OnPropertyChanged(nameof(Label));
            }
        }

        public string Address
        {
            get => _fAddress;
            set
            {
                _fAddress = value;
                OnPropertyChanged(nameof(Address));
            }
        }

        public Position Position
        {
            get => _fPosition;
            set
            {
                _fPosition = value;
                OnPropertyChanged(nameof(Position));
            }
        }

        #endregion
    }
}
