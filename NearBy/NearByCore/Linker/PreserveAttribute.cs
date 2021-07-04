using System;
namespace Android.Runtime
{
    public sealed class PreserveAttribute : Attribute
    {
        public bool AllMembers;
        public bool Conditional;
    }
}