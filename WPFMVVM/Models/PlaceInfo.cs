﻿using System.Windows;

namespace WPFMVVM.Models
{
    internal class PlaceInfo
    {
        public string Name { get; set; }
        public virtual Point Location { get; set; }
        public virtual IEnumerable<ConfirmedCount> Counts { get; set; }
    }
}
