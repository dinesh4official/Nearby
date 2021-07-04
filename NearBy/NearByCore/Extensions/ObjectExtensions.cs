using System;
using Android.Runtime;
using JetBrains.Annotations;

namespace NearByCore.Extensions
{
    [Preserve(AllMembers = true)]
    public static class ObjectExtensions
    {
        #region Methods

        [ContractAnnotation("null=>false")]
        public static bool IsNotNull(this object obj)
        {
            return obj != null;
        }

        [ContractAnnotation("null=>true")]
        public static bool IsNull(this object obj)
        {
            return obj == null;
        }

        #endregion
    }
}
