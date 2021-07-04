using System;
using System.Threading.Tasks;
using NearByCore.Interfaces;
using NearByCore.Resources;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace NearBy.Helper.Utils
{
    [Preserve(AllMembers = true)]
    public static class AppUtils
    {
        #region Methods

        public static void ShowAlert(string message, bool isLong)
        {
            if (isLong)
            {
                DependencyService.Get<IMessage>().LongAlert(message);
            }
            else
            {
                DependencyService.Get<IMessage>().ShortAlert(message);
            }
        }

        public static async Task ShowAlertwithCancel(string title, string message, string cancel)
        {
            await App.Current.MainPage.DisplayAlert(title, message, cancel);
        }

        public static async Task<string> ShowActionSheet(string title, params string[] buttons)
        {
            return await Application.Current.MainPage.DisplayActionSheet(title, AppResources.Cancel, null, buttons);
        }

        #endregion
    }
}
