using System;
using System.Globalization;
using Android.Runtime;

namespace NearByCore.Interfaces
{
    [Preserve(AllMembers = true)]
    public interface ILocaleConfiguration
    {
        #region Methods

        CultureInfo GetCurrentCulture();

        void SetCurrentCulture(CultureInfo cultureInfo);

        #endregion
    }
}
