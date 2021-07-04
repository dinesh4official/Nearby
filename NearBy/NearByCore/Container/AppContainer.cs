using System;
using Android.Runtime;
using Autofac;

namespace NearByCore
{
    [Preserve(AllMembers = true)]
    public static class AppContainer
    {
        #region Properties

        public static IContainer Current { get; set; }

        #endregion
    }
}
