using System.Security.Cryptography.Pkcs;
using System.Windows;
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
        public CountriesStatisticViewModel() : this(null)
        {
            if (!App.IsDesignMode)
                throw new InvalidOperationException("ОШИБКА: Вызов конструктора, предназначенного для дизайнера");

            _Countries = Enumerable.Range(1, 10)
                .Select(i => new CountryInfo
                {
                    Name = $"Country {i}",
                    Provinces = Enumerable.Range(1, 10).Select(j => new PlaceInfo
                    {
                        Name = $"Province {j}",
                        Location = new Point(i, j),
                        Counts = Enumerable.Range(1, 10).Select(k => new ConfirmedCount
                        {
                            Date = DateTime.Now.Subtract(TimeSpan.FromDays(100 - k)),
                            Count = k
                        }).ToArray()
                    }).ToArray()
                }).ToArray();
        }
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
