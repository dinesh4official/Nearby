using System;
using Android.Runtime;

namespace NearByCore.Interfaces
{
    [Preserve(AllMembers = true)]
    public interface IMessage
    {
        #region Methods

        void LongAlert(string message);

        void ShortAlert(string message);

        #endregion
    }
}
