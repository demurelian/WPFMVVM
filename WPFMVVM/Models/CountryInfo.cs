using System.Windows;

namespace WPFMVVM.Models
{
    internal class CountryInfo : PlaceInfo
    {
        private Point? _Location;

        public override Point Location
        {
            get
            {
                if (Provinces is null) return default;

                var avg_x = Provinces.Average(p => p.Location.X);
                var avg_y = Provinces.Average(p => p.Location.Y);

                return (Point)(_Location = new Point(avg_x, avg_y));
            }
            set => _Location = value;
        }
        public IEnumerable<PlaceInfo> Provinces { get; set; }
    }
}
