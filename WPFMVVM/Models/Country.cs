using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFMVVM.Models
{
    internal class PlaceInfo
    {
        public string Name { get; set; }
        public Point Location { get; set; }
        public IEnumerable<ConfirmedCount> Counts { get; set; }
    }
    internal class CountryInfo : PlaceInfo
    {
        public IEnumerable<ProvinceInfo> Provinces { get; set; }
    }
    internal class ProvinceInfo : PlaceInfo { }
    internal struct ConfirmedCount
    {
        public DateTime Date { get; set; }
        public int Count { get; set; }
    }
    internal struct MyDataPoint
    {
        public double XValue { get; set; }
        public double YValue { get; set; }
    }
}
