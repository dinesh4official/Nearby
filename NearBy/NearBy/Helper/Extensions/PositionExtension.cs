using System;
using Xamarin.Forms.Internals;

namespace NearBy.Helper.Extensions
{
    [Preserve(AllMembers = true)]
    public static class PositionExtension
    {
        #region Methods

        public static Xamarin.Forms.Maps.Position ToMapPosition(this NearByCore.Models.Position position)
        {
            return new Xamarin.Forms.Maps.Position(position.Latitude, position.Longitude);
        }

        public static NearByCore.Models.Position ToCorePosition(this Xamarin.Forms.Maps.Position position)
        {
            return new NearByCore.Models.Position(position.Latitude, position.Longitude);
        }

        #endregion
    }
}
