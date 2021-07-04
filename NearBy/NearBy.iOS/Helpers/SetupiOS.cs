using System;
using Autofac;
using Foundation;
using NearByCore.Interfaces;

namespace NearBy.iOS.Helpers
{
    [Preserve(AllMembers = true)]
    public class SetupiOS : ISetup
    {
        #region Methods

        public void Init(ContainerBuilder builder)
        {
            builder.RegisterType<LocaleConfigurationiOS>().As<ILocaleConfiguration>().SingleInstance();
        }

        public void MakeOSSpecificUpdates()
        {

        }

        #endregion
    }
}
