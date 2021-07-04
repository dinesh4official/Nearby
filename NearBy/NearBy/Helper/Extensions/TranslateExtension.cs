using System;
using System.Globalization;
using System.Resources;
using Autofac;
using NearByCore;
using NearByCore.Constants;
using NearByCore.Extensions;
using NearByCore.Interfaces;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace NearBy.Helper.Extensions
{
    [ContentProperty(AppConstants.Key)]
    [Preserve(AllMembers = true)]
    public class TranslateExtension : IMarkupExtension
    { 
        #region Properties

        public string Key { get; set; }
        public bool AllCaps { get; set; }
        public bool AllLower { get; set; }

        public static CultureInfo CurrentCulture => AppContainer.Current == null
            ? CultureInfo.CurrentCulture :
            AppContainer.Current.Resolve<ILocaleConfiguration>().GetCurrentCulture();


        #endregion

        #region Methods

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            if (Key.IsBlank())
            {
                return string.Empty;
            }

            return GetValue(Key, AllCaps, AllLower);
        }

        public static string GetValue(string key, bool allCaps = false, bool allLower = false)
        {
            var cultureInfo = CurrentCulture;
            var resourceManager = new ResourceManager(typeof(NearByCore.Resources.AppResources));
            string translation = null;

            try
            {
                translation = resourceManager.GetString(key, cultureInfo);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            if (translation == null)
            {
                try
                {
                    translation = resourceManager.GetString(key);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }

            // If no translation is found, check for a platform-specific translation
            if (translation == null)
            {
                var platformSpecificResourceName = $"{key}_{Device.RuntimePlatform}";

                translation = resourceManager.GetString(platformSpecificResourceName, cultureInfo);
            }

            // If no translation is found, the resource must be missing
            if (translation == null)
            {
#if DEBUG
                throw new ArgumentException(
                    $@"Key '{key}' was not found in resources '{AppConstants.ResourceFileNamespace}' for culture '{cultureInfo.Name}'.", nameof(key));
#else
                return string.Empty;
#endif
            }

            if (allCaps)
            {
                return translation.ToUpper(cultureInfo);
            }
            else if (allLower)
            {
                return translation.ToLower(cultureInfo);
            }
            else
            {
                return translation;
            }
        }
        #endregion
    }
}
