using System;
using Android.Runtime;

namespace NearByCore.Models
{
	[Preserve(AllMembers = true)]
	public struct Position
	{
        #region Constructor

        public Position(double latitude, double longitude)
		{
			Latitude = Math.Min(Math.Max(latitude, -90.0), 90.0);
			Longitude = Math.Min(Math.Max(longitude, -180.0), 180.0);
		}

        #endregion

        #region Properties

        public double Latitude { get; }

		public double Longitude { get; }

        #endregion

        #region Override Methods

        public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj))
			{
				return false;
			}

			if (obj.GetType() != GetType())
			{
				return false;
			}

			var other = (Position)obj;
			return Latitude == other.Latitude && Longitude == other.Longitude;
		}

        #endregion

        public override int GetHashCode()
		{
			unchecked
			{
				int hashCode = Latitude.GetHashCode();
				hashCode = (hashCode * 397) ^ Longitude.GetHashCode();
				return hashCode;
			}
		}

		public static bool operator ==(Position left, Position right)
		{
			return Equals(left, right);
		}

		public static bool operator !=(Position left, Position right)
		{
			return !Equals(left, right);
		}
	}
}
