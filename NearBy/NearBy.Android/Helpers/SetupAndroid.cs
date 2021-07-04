using System;
using Autofac;
using NearByCore.Interfaces;
using Xamarin.Forms.Internals;

namespace NearBy.Droid.Helpers
{
    [Preserve(AllMembers = true)]
    public class SetupAndroid : ISetup
    {
        #region Methods

        public void Init(ContainerBuilder builder)
        {
            builder.RegisterType<LocaleConfigurationAndroid>().As<ILocaleConfiguration>().SingleInstance();
        }

        public void MakeOSSpecificUpdates()
        {

        }

        #endregion
    }
}
