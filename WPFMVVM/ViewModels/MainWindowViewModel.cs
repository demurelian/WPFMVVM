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
        #region Тестовый набор данных для графика
        private IEnumerable<MyDataPoint> _TestDataPoints;
        /// <summary>Тестовый набор данных для визуализации графиков</summary>
        public IEnumerable<MyDataPoint> TestDataPoints
        {
            get => _TestDataPoints;
            set => Set(ref _TestDataPoints, value);
        }
        #endregion
        #region Заголовок окна
        private string _Title = "Анализ статистики";
        /// <summary>Заголовок окна</summary>
        public string Title
        {
            get => _Title;
            set => Set(ref _Title, value);
        }
        #endregion
        #region Модель графика
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
        #endregion
        #region Команды
        #region CreateGroupCommand
        public ICommand CreateGroupCommand { get; }
        private bool CanCreateGroupCommandExecute(object p) => true;
        private void OnCreateGroupCommandExecute(object p)
        {
            var group_max_index = Groups.Count + 1;
            var new_group = new Group
            {
                Name = $"Группа {group_max_index}",
                Students = new ObservableCollection<Student>()
            };
            Groups.Add(new_group);
        }
        #endregion
        #region DeleteGroupCommand
        public ICommand DeleteGroupCommand { get; }
        private bool CanDeleteDroupCommandExecute(object p) => p is Group group && Groups.Contains(group);
        private void OnDeleteGroupCommandExecute(object p)
        {
            if (!(p is Group group)) return;
            var group_index = Groups.IndexOf(group);
            Groups.Remove(group);
            if (group_index < Groups.Count)
                SelectedGroup = Groups[group_index];
        }
        #endregion
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
            CreateGroupCommand = new LambdaCommand(OnCreateGroupCommandExecute, CanCreateGroupCommandExecute);
            DeleteGroupCommand = new LambdaCommand(OnDeleteGroupCommandExecute, CanDeleteDroupCommandExecute);
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
                Students = new ObservableCollection<Student>(students),
                Description = $"Описание {i} группы"
            });
            Groups = new ObservableCollection<Group>(groups);
            #endregion
        }
    }
}
