using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.OS;
using FFImageLoading.Forms.Platform;
using Xamarin;
using Android.Views;
using NearBy.Helper.Utils;
using NearByCore.Container;
using NearBy.Droid.Helpers;

namespace NearBy.Droid
{
    [Xamarin.Forms.Internals.Preserve(AllMembers = true)]
    [Activity(Label = "NearBy", Icon = "@drawable/map_icon", Theme = "@style/MainTheme", MainLauncher = false, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize, ScreenOrientation = ScreenOrientation.Portrait)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

           // Window.AddFlags(WindowManagerFlags.Fullscreen);
           // Window.ClearFlags(WindowManagerFlags.ForceNotFullscreen);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            FormsMaps.Init(this, savedInstanceState);
            CachedImageRenderer.Init(true);
            AppInitializer.Init(new SetupAndroid(), new SetupUi());
            LoadApplication(new App());
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}