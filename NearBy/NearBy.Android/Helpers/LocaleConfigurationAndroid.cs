using System;
using System.Globalization;
using System.Threading;
using Java.Util;
using NearByCore.Interfaces;
using NearByCore.Platform;
using Xamarin.Forms.Internals;

namespace NearBy.Droid.Helpers
{
    [Preserve(AllMembers = true)]
    public class LocaleConfigurationAndroid : ILocaleConfiguration
    {
        #region Methods

        private string AndroidToDotNetLanguage(string androidLanguage)
        {
            var dotNetLanguage = androidLanguage;

            // Certain languages need to be converted to CultureInfo equivalent
            switch (androidLanguage)
            {
                case "ms-BN": // "Malaysian (Brunei)" not supported .NET culture
                case "ms-MY": // "Malaysian (Malaysia)" not supported .NET culture
                case "ms-SG": // "Malaysian (Singapore)" not supported .NET culture
                    dotNetLanguage = "ms"; // closest supported
                    break;

                case "in-ID": // "Indonesian (Indonesia)" has different code in  .NET
                    dotNetLanguage = "id-ID"; // correct code for .NET
                    break;

                case "gsw-CH": // "Schwiizertüütsch (Swiss German)" not supported .NET culture
                    dotNetLanguage = "de-CH"; // closest supported
                    break;
                case "zh-Hans":
                    dotNetLanguage = "zs";
                    break;
                case "zh-Hant":
                    dotNetLanguage = "zh";
                    break;
                    // Add more application-specific cases here (if required)
                    // ONLY use cultures that have been tested and known to work
            }

            return dotNetLanguage;
        }

        private string ToDotNetFallbackLanguage(PlatformCulture platCulture)
        {
            var dotNetLanguage = platCulture.LanguageCode;

            switch (platCulture.LanguageCode)
            {
                case "gsw":
                    dotNetLanguage = "de-CH"; // equivalent to German (Switzerland) for this app
                    break;

                    // Add more application-specific cases here (if required)
                    // ONLY use cultures that have been tested and known to work
            }

            return dotNetLanguage;
        }

        #endregion

        #region ILocaleConfigurationService Members

        public CultureInfo GetCurrentCulture()
        {
            var androidLocale = Locale.Default;

            var dotNetLanguage = AndroidToDotNetLanguage(androidLocale.ToString().Replace("_", "-"));

            // This gets called a lot - try/catch can be expensive so consider caching or something
            CultureInfo ci;

            try
            {
                ci = new CultureInfo(dotNetLanguage);
            }
            catch (CultureNotFoundException)
            {
                // Android locale not valid .NET culture, so fallback to language code
                try
                {
                    var fallback = ToDotNetFallbackLanguage(new PlatformCulture(dotNetLanguage));

                    ci = new CultureInfo(fallback);
                }
                catch (CultureNotFoundException)
                {
                    // Android language not valid .NET culture, so fall back to ENGLISH
                    ci = new CultureInfo("en");
                }
            }

            return ci;
        }

        public void SetCurrentCulture(CultureInfo ci)
        {
            Thread.CurrentThread.CurrentCulture = ci;
            Thread.CurrentThread.CurrentUICulture = ci;
        }

        #endregion
    }
}
