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

        private IEnumerable<ConfirmedCount> _Counts;
        public override IEnumerable<ConfirmedCount> Counts { 
            get
            {
                var provinces_quantity = Provinces.Count();
                if (provinces_quantity == 1)
                    return Provinces.First().Counts;

                var records = Provinces.First().Counts.Count();
                var result = Provinces.First().Counts.ToArray();
                var provinces_arr = Provinces.ToArray();

                for (int i = 0; i < provinces_quantity; i++)
                {
                    var temp_arr = provinces_arr[i].Counts.ToArray();

                    for (int j = 1; j < records; j++)
                    {
                        result[j].Count += temp_arr[j].Count;
                    }
                }
                return _Counts = result;
            }
            set => _Counts = value;
        }
    }
}
