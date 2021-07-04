using System;
using System.Collections.Generic;
using Android.Runtime;

namespace NearByCore.Models
{
    [Preserve(AllMembers = true)]
    public class Place
    {
        #region Properties

        public double Distance { get; set; }

        public string ImageUrl { get; set; }

        public string Name { get; set; }

        public string PlaceId { get; set; }

        public List<string> PlaceTypes { get; set; }

        public Position Position { get; set; }

        public decimal Rating { get; set; }

        public bool? OpenNow { get; set; }

        public string Vicinity { get; set; }

        #endregion
    }
}
