namespace WPFMVVM.Models
{
    internal class CountryInfo : PlaceInfo
    {
        public IEnumerable<PlaceInfo> Provinces { get; set; }
    }
}
