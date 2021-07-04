using System;
using System.Collections;
using Android.Runtime;

namespace NearByCore.Extensions
{
    [Preserve(AllMembers = true)]
    public static class ListExtensions
    {
        #region Methods

        public static bool IsEmpty(this IList toCheck)
        {
            return toCheck.IsNull() || toCheck.Count == 0;
        }

        public static bool IsNotEmpty(this IList toCheck)
        {
            return toCheck.IsNotNull() || toCheck.Count > 0;
        }

        #endregion
    }
}
