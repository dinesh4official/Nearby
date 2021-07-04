using System;
using Android.Runtime;
using Autofac;
using NearByCore.Interfaces;
using NearByCore.Repositories;
using NearByCore.Services;

namespace NearByCore.Container
{
    [Preserve(AllMembers = true)]
    public static class AppInitializer
    {
        #region Methods

        public static void Init(ISetup osSetup, ISetup formsSetup)
        {
            RegisterDependencies(osSetup, formsSetup);
        }

        private static void RegisterDependencies(ISetup osSetup, ISetup formsSetup)
        {
            var builder = new ContainerBuilder();

            osSetup.Init(builder);

            //Helpers
            builder.RegisterType<HTTPService>().As<IHTTPService>().SingleInstance();
            builder.RegisterType<MemCacheHelper>().As<ICacheHelper>().SingleInstance();

            //Repositories
            builder.RegisterType<PlacesRepository>().As<IPlacesRepository>().SingleInstance();

            //Services
            builder.RegisterType<PlacesService>().As<IPlacesService>().SingleInstance();

            //Forms UI
            formsSetup.Init(builder);

            AppContainer.Current = builder.Build();
            osSetup.MakeOSSpecificUpdates();
        }

        #endregion
    }
}
