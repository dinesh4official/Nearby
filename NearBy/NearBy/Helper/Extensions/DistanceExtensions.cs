using System;
using Xamarin.Forms.Internals;

namespace NearBy.Helper.Extensions
{
    [Preserve(AllMembers = true)]
    public static class DistanceExtensions
    {
        #region Methods

        public static string ToDistanceString(this double distance)
        {
            var numberOfFeet = distance * 5280;

            if (numberOfFeet > 2640)
            {
                return distance.ToString("#.# mi");
            }
            else if (numberOfFeet > 300)
            {
                return (numberOfFeet / 3d).ToString("### yd");
            }
            else
            {
                return numberOfFeet.ToString("### ft");
            }
        }

        #endregion
    }
}
