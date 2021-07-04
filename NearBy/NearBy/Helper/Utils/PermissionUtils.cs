using System;
using System.Threading.Tasks;
using NearByCore.Resources;
using Xamarin.Essentials;
using Xamarin.Forms.Internals;

namespace NearBy.Helper.Utils
{
    [Preserve(AllMembers = true)]
    public class PermissionUtils
    {
        #region ReadOnly Fields

        static readonly Lazy<PermissionUtils> lazyInstance = new Lazy<PermissionUtils>(() => new PermissionUtils());

        #endregion

        #region Constructor

        private PermissionUtils()
        {

        }

        #endregion

        #region Properties

        public static PermissionUtils Instance => lazyInstance.Value;

        #endregion

        #region Methods

        public async Task<PermissionStatus> CheckAndRequestLocationPermission()
        {
            var status = await Permissions.CheckStatusAsync<Permissions.LocationWhenInUse>();

            if (status == PermissionStatus.Granted)
            {
                return status;
            }

            if (status == PermissionStatus.Denied && Xamarin.Essentials.DeviceInfo.Platform == DevicePlatform.iOS)
            {
                // Need to prompt the user to turn on in settings.
                // On iOS, once a permission has been denied it may not be requested again from the application.
                await AppUtils.ShowAlertwithCancel(AppResources.LocationPermission,
                    AppResources.LocationAlertMessage, AppResources.Settings);
                return status;
            }

            if (Permissions.ShouldShowRationale<Permissions.LocationWhenInUse>())
            {
                // Need to prompt the user as why the permission is needed.
                await AppUtils.ShowAlertwithCancel(AppResources.LocationPermission,
                    AppResources.LocationAlertMessage, AppResources.Understood);
            }

            status = await Permissions.RequestAsync<Permissions.LocationWhenInUse>();
            return status;
        }

        #endregion
    }

}
