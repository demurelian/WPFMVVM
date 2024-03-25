using WPFMVVM.Services;
using WPFMVVM.ViewModels.Base;

namespace WPFMVVM.ViewModels
{
    internal class CountriesStatisticViewModel : ViewModel
    {
        private DataService _DataService;
        private MainWindowViewModel _MainModel { get; }
        public CountriesStatisticViewModel(MainWindowViewModel MainModel)
        {
            _MainModel = MainModel;

            _DataService = new DataService();
        }
    }
}
