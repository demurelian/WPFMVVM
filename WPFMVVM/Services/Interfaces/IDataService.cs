using WPFMVVM.Models;

namespace WPFMVVM.Services.Interfaces
{
    interface IDataService
    {
        IEnumerable<CountryInfo> GetData();
    }
}
