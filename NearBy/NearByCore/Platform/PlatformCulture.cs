using System;
using Android.Runtime;
using NearByCore.Resources;

namespace NearByCore.Platform
{
    [Preserve(AllMembers = true)]
    public class PlatformCulture
    {
        #region Constructors

        public PlatformCulture(string platformCultureString)
        {
            if (string.IsNullOrEmpty(platformCultureString))
                throw new ArgumentException(AppResources.UnExpectedCulture, nameof(platformCultureString));

            //.NET expects dash, not underscore
            PlatformString = platformCultureString.Replace("_", "-"); 

            var dashIndex = PlatformString.IndexOf("-", StringComparison.Ordinal);
            if (dashIndex > 0)
            {
                var parts = PlatformString.Split('-');
                LanguageCode = parts[0];
                LocaleCode = parts[1];
            }
            else
            {
                LanguageCode = PlatformString;
                LocaleCode = "";
            }
        }

        #endregion

        #region Properties

        public string LanguageCode { get; }

        public string LocaleCode { get; }

        public string PlatformString { get; }

        #endregion

        #region Methods

        public override string ToString()
        {
            return PlatformString;
        }

        #endregion
    }
}
