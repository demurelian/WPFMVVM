using System.Security.Cryptography.Pkcs;
using System.Windows.Input;
using WPFMVVM.Infrastructure.Commands;
using WPFMVVM.Models;
using WPFMVVM.Services;
using WPFMVVM.ViewModels.Base;

namespace WPFMVVM.ViewModels
{
    internal class CountriesStatisticViewModel : ViewModel
    {
        private DataService _DataService;
        private MainWindowViewModel _MainModel { get; }

        private IEnumerable<CountryInfo> _Countries;

        public IEnumerable<CountryInfo> Countries
        {
            get => _Countries;
            private set => Set(ref _Countries, value);
        }

        #region Команды
        public ICommand RefreshDataCommand { get; }

        private void OnRefreshDataCommandExecute(object p)
        {
            Countries = _DataService.GetData();
        }
        #endregion
        public CountriesStatisticViewModel(MainWindowViewModel MainModel)
        {
            _MainModel = MainModel;

            _DataService = new DataService();
            #region Команды
            RefreshDataCommand = new LambdaCommand(OnRefreshDataCommandExecute);
            #endregion
        }
    }
}
