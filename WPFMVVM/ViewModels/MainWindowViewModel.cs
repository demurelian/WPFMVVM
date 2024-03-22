using System.Windows;
using System.Windows.Input;
using WPFMVVM.Infrastructure.Commands;
using WPFMVVM.Models;
using WPFMVVM.ViewModels.Base;
using OxyPlot;
using System.Collections.ObjectModel;
using WPFMVVM.Models.Decanat;

namespace WPFMVVM.ViewModels
{
    internal class MainWindowViewModel : ViewModel
    {
        public ObservableCollection<Group> Groups { get; set; }

        private Group _SelectedGroup;
        /// <summary>Выбранная группы</summary>
        public Group SelectedGroup
        {
            get => _SelectedGroup;
            set => Set(ref _SelectedGroup, value);
        }
        #region Свойства
        private IEnumerable<MyDataPoint> _TestDataPoints;
        /// <summary>Тестовый набор данных для визуализации графиков</summary>
        public IEnumerable<MyDataPoint> TestDataPoints
        {
            get => _TestDataPoints;
            set => Set(ref _TestDataPoints, value);
        }
        private string _Title = "Анализ статистики";
        /// <summary>Заголовок окна</summary>
        public string Title
        {
            get => _Title;
            set => Set(ref _Title, value);
        }
        /// <summary>Модель графика</summary>
        private PlotModel _MyPlotModel;
        public PlotModel MyPlotModel
        {
            get => _MyPlotModel;
            set => Set(ref _MyPlotModel, value);
        }
        #endregion
        #region Статус программы
        private string _Status = "Готов";
        ///<summary>Статус программы</summary>
        public string Status
        {
            get => _Status;
            set => Set(ref _Status, value);
        }
        #endregion
        #region Команды
        #region CloseApplicationCommand
        public ICommand CloseApplicationCommand { get; }
        private void OnCloseApplicationCommandExecute(object p)
        {
            Application.Current.Shutdown();
        }
        private bool CanCloseApplicationCommandExecute(object p) => true;
        #endregion
        #endregion
        public MainWindowViewModel()
        {
            #region Команды
            CloseApplicationCommand = new LambdaCommand(OnCloseApplicationCommandExecute, CanCloseApplicationCommandExecute);
            #endregion
            #region График
            var data_points = new List<MyDataPoint>((int)(360 / 0.1));
            for (var x = 0d; x <= 360; x += 0.1)
            {
                const double to_rad = Math.PI / 180;
                var y = Math.Sin(x * to_rad);
                data_points.Add(new MyDataPoint { XValue = x, YValue = y });
            }
            TestDataPoints = data_points;

            MyPlotModel = new PlotModel { Title = "Example" };
            MyPlotModel.Axes.Add(new OxyPlot.Axes.LinearAxis { Position = OxyPlot.Axes.AxisPosition.Left });
            MyPlotModel.Axes.Add(new OxyPlot.Axes.LinearAxis { Position = OxyPlot.Axes.AxisPosition.Bottom });

            MyPlotModel.Series.Add(new OxyPlot.Series.LineSeries { ItemsSource = TestDataPoints, DataFieldX = "XValue", DataFieldY = "YValue", Color = OxyColor.FromRgb(255, 0, 0) });
            #endregion
            #region Студенты
            var students = Enumerable.Range(1, 10).Select(i => new Student
            {
                Name = $"Name {i}",
                Surname = $"Surname {i}",
                Patronymic = $"Patronymic {i}",
                Birthday = DateTime.Now,
                Rating = 0
            });
            var groups = Enumerable.Range(1, 20).Select(i => new Group
            {
                Name = $"Группа {i}",
                Students = new ObservableCollection<Student>(students)
            });
            Groups = new ObservableCollection<Group>(groups);
            #endregion
        }
    }
}
