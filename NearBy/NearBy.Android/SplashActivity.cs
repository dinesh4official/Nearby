using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Runtime;
using AndroidX.AppCompat.App;

namespace NearBy.Droid
{
    [Xamarin.Forms.Internals.Preserve(AllMembers = true)]
    [Activity(Label = "NearBy", Icon = "@drawable/map_icon", Theme = "@style/SplashScreen", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize, NoHistory = true, ScreenOrientation = ScreenOrientation.Portrait)]
    public class SplashActivity : AppCompatActivity
    {
        #region Override Methods

        protected async override void OnResume()
        {
            base.OnResume();
            await StartupApp();
        }

        public override void OnBackPressed() { }

        #endregion

        #region Private Methods


        private async Task StartupApp()
        {
            await Task.Delay(100);
            Intent intent = new Intent(this, typeof(MainActivity));
            intent.SetFlags(ActivityFlags.ClearTop);
            StartActivity(intent);
        }

        #endregion
    }
}
