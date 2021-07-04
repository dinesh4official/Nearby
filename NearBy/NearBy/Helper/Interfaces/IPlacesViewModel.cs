using System;
using System.Windows.Input;
using NearBy.Models;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms.Internals;

namespace NearBy.Helper.Interfaces
{
    [Preserve(AllMembers = true)]
    public interface IPlacesViewModel
    {
        #region Properties

        int ItemsThreshold { get; set; }

        ICommand LoadItemsInPage { get; }

        ObservableRangeCollection<PlacesInfo> PlacesInfos { get; }

        ICommand ShowOnMap { get; }

        ICommand ShowSortMenu { get; }

        #endregion
    }
}
