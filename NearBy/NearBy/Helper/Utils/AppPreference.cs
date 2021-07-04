using System;
using Xamarin.Essentials;
using Xamarin.Forms.Internals;

namespace NearBy.Helper.Utils
{
    [Preserve(AllMembers = true)]
    public static class AppPreference
    {
        #region Methods

        public static void StoreBoolValue(string key, bool value)
        {
            bool hasKey = Preferences.ContainsKey(key);
            if (hasKey)
            {
                Preferences.Remove(key);
            }

            Preferences.Set(key, value);
        }

        public static bool GetBoolValue(string key)
        {
            return Preferences.Get(key, false);
        }

        #endregion
    }
}
