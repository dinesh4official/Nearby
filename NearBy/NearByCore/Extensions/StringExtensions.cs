using System;
using Android.Runtime;

namespace NearByCore.Extensions
{
    [Preserve(AllMembers = true)]
    public static class StringExtensions
    {
        #region Methods

        public static bool IsBlank(this string toCheck)
        {
            return string.IsNullOrWhiteSpace(toCheck);
        }

        public static bool IsNotBlank(this string toCheck)
        {
            return !string.IsNullOrWhiteSpace(toCheck);
        }

        #endregion
    }
}
