using System;
using Android.Runtime;
using Autofac;

namespace NearByCore.Interfaces
{
    [Preserve(AllMembers = true)]
    public interface ISetup
    {
        #region Methods

        void Init(ContainerBuilder builder);

        void MakeOSSpecificUpdates();

        #endregion
    }
}
