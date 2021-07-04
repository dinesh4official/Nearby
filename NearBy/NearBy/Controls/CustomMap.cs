using System;
using System.Collections.Generic;
using NearBy.Helper.Extensions;
using NearBy.Models;
using NearByCore.Resources;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Maps;
using CorePosition = NearByCore.Models.Position;

namespace NearBy.Controls
{
    [Preserve(AllMembers = true)]
    public class CustomMap : Map
    {
        #region Bindable Properties

        public static readonly BindableProperty PinsItemsSourceProperty =
            BindableProperty.Create(nameof(PinsItemsSource), typeof(List<MapInfo>), typeof(CustomMap),
               null, propertyChanged: OnPinsItemsSourceChanged);

        public static readonly BindableProperty CurrentPositionProperty =
          BindableProperty.Create(nameof(CurrentPosition), typeof(CorePosition), typeof(CustomMap),
             null, propertyChanged: OnCurrentPositionChanged);

        #endregion

        #region Constructor

        public CustomMap()
        {

        }

        #endregion

        #region Properties

        public List<MapInfo> PinsItemsSource
        {
            get { return (List<MapInfo>)GetValue(PinsItemsSourceProperty); }
            set { SetValue(PinsItemsSourceProperty, value); }
        }

        public CorePosition CurrentPosition
        {
            get { return (CorePosition)GetValue(CurrentPositionProperty); }
            set { SetValue(CurrentPositionProperty, value); }
        }


        #endregion

        #region Callback Methods

        private static void OnPinsItemsSourceChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is CustomMap customMap)
            {
                List<MapInfo> mapInfos = (List<MapInfo>)newValue;
                if (mapInfos == null)
                {
                    customMap.Pins.Clear();
                }
                else
                {
                    foreach (MapInfo item in mapInfos)
                    {
                        Pin pin = new Pin()
                        {
                            Label = item.Label,
                            Address = item.Address,
                            Position = item.Position.ToMapPosition()
                        };

                        customMap.Pins.Add(pin);
                    }
                }
            }
            else
            {
                throw new Exception(AppResources.BindableObjectError);
            }
        }

        private static void OnCurrentPositionChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is CustomMap customMap)
            {
                CorePosition corePosition = (CorePosition)newValue;
                Position position = corePosition.ToMapPosition();
                customMap.MoveToRegion(MapSpan.FromCenterAndRadius(position, Distance.FromMiles(5)));
            }
            else
            {
                throw new Exception(AppResources.BindableObjectError);
            }
        }

        #endregion
    }
}
