﻿using System;
using System.Collections.Generic;
using System.Linq;
using FFImageLoading.Forms.Platform;
using Foundation;
using NearBy.Helper.Utils;
using NearBy.iOS.Helpers;
using NearByCore.Container;
using UIKit;

namespace NearBy.iOS
{
    [Preserve(AllMembers = true)]
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            global::Xamarin.Forms.Forms.Init();
            Xamarin.FormsMaps.Init();
            CachedImageRenderer.Init();
            AppInitializer.Init(new SetupiOS(), new SetupUi());
            LoadApplication(new App());

            return base.FinishedLaunching(app, options);
        }
    }
}
