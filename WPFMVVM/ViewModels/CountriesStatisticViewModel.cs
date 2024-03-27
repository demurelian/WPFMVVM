using OxyPlot;
using System.Security.Cryptography.Pkcs;
using System.Windows;
using System.Windows.Input;
using WPFMVVM.Infrastructure.Commands;
using WPFMVVM.Models;
using WPFMVVM.Services;
using WPFMVVM.Services.Interfaces;
using WPFMVVM.ViewModels.Base;

namespace WPFMVVM.ViewModels
{
    internal class CountriesStatisticViewModel : ViewModel
    {
        private IDataService _DataService;
        public MainWindowViewModel MainModel { get; internal set; }

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

        private PlotModel _MyPlotModel;
        public PlotModel MyPlotModel
        {
            get => _MyPlotModel;
            set => Set(ref _MyPlotModel, value);
        }
        private CountryInfo _SelectedCountry;
        public CountryInfo SelectedCountry
        {
            get => _SelectedCountry;
            set
            {
                MyPlotModel.Series.Clear();
                Set(ref _SelectedCountry, value);
                MyPlotModel.Title = _SelectedCountry.Name;
                MyPlotModel.Series.Add(new OxyPlot.Series.LineSeries
                {
                    StrokeThickness = 2,
                    Color = OxyColor.FromRgb(255, 0, 0),
                    ItemsSource = _SelectedCountry.Counts,
                    DataFieldX = "Date",
                    DataFieldY = "Count"
                });
                MyPlotModel.InvalidatePlot(true);
            }
        }

        public CountriesStatisticViewModel(IDataService DataService)
        { 
            _DataService = DataService;

            RefreshDataCommand = new LambdaCommand(OnRefreshDataCommandExecute);

            InitializePlotModel();
        }
        private void InitializePlotModel()
        {
            MyPlotModel = new PlotModel { Title = "Example" };
            MyPlotModel.Axes.Add(new OxyPlot.Axes.LinearAxis
            {
                Position = OxyPlot.Axes.AxisPosition.Left,
                Title = "Число",
                MajorGridlineStyle = LineStyle.Solid,
                MinorGridlineStyle = LineStyle.Dash
            });
            MyPlotModel.Axes.Add(new OxyPlot.Axes.DateTimeAxis
            {
                Position = OxyPlot.Axes.AxisPosition.Bottom,
                Title = "Дата",
                MajorGridlineStyle = LineStyle.Solid,
                MinorGridlineStyle = LineStyle.Dash
            });
        }
    }
}
