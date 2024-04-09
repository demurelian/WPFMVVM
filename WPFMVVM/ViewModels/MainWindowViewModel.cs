using System.Windows;
using System.Windows.Input;
using WPFMVVM.Infrastructure.Commands;
using WPFMVVM.Models;
using WPFMVVM.ViewModels.Base;
using OxyPlot;
using System.Collections.ObjectModel;
using WPFMVVM.Models.Decanat;
using System.Windows.Data;
using System.ComponentModel;
using WPFMVVM.Services;
using System.Windows.Controls;

namespace WPFMVVM.ViewModels
{
    internal class MainWindowViewModel : ViewModel
    {
        public CountriesStatisticViewModel CountriesStatisticViewModel { get; }

        private readonly IAsyncDataService _AsyncData;

        private void ComputeValue()
        {
            DataValue = _AsyncData.GetResult(DateTime.Now);
        }
        

        #region Директории
        public DirectoryViewModel DiskRootDir { get; } = new DirectoryViewModel("c:\\");
        private DirectoryViewModel _SelectedDirectory;
        public DirectoryViewModel SelectedDirectory
        {
            get => _SelectedDirectory;
            set => Set(ref _SelectedDirectory, value);
        }
        #endregion
        #region Студенты
        public ObservableCollection<Group> Groups { get; set; }
        public IEnumerable<Student> TestStudentsGenerate => Enumerable.Range(1, App.IsDesignMode ? 10 : 100_000)
            .Select(i => new Student
            {
                Name = $"Имя {i}",
                Surname = $"Фамилия {i}",
                Patronymic = $"Отчество {i}"
            });
        private readonly CollectionViewSource _SelectedGroupStudentCollection = new CollectionViewSource();

        public ICollectionView SelectedGroupStudentsCollection => _SelectedGroupStudentCollection?.View;
        /// <summary>Фильтр студентов</summary>
        private string _StudentFilterText;
        public string StudentFilterText
        {
            get => _StudentFilterText;
            set
            {
                if (!Set(ref _StudentFilterText, value)) return;
                _SelectedGroupStudentCollection.View.Refresh();
            }
        }
        private void OnStudentsFilter(Object sender, FilterEventArgs e)
        {
            if (!(e.Item is Student student))
            {
                e.Accepted = false;
                return;
            }
            if (student.Name is null || student.Surname is null)
            {
                e.Accepted = false;
                return;
            }
            var filter_text = _StudentFilterText;
            if (string.IsNullOrEmpty(filter_text)) return;

            if (student.Name.Contains(filter_text, StringComparison.OrdinalIgnoreCase)) return;
            if (student.Surname.Contains(filter_text, StringComparison.OrdinalIgnoreCase)) return;
            if (student.Patronymic.Contains(filter_text, StringComparison.OrdinalIgnoreCase)) return;

            e.Accepted = false;
        }
        #endregion
        #region Свойства
        private string _DataValue;
        /// <summary>Результат длительной асинхронной операции</summary>
        public string DataValue
        {
            get => _DataValue;
            private set => Set(ref _DataValue, value);
        }
        #region Выбранная группа
        private Group _SelectedGroup;
        /// <summary>Выбранная группы</summary>
        public Group SelectedGroup
        {
            get => _SelectedGroup;
            set
            {
                if (!Set(ref _SelectedGroup, value)) return;
                _SelectedGroupStudentCollection.Source = value?.Students;
                OnPropertyChanged(nameof(SelectedGroupStudentsCollection));
            }
        }
        #endregion
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
        public ICommand StartProcessCommand { get; }
        private bool CanStartProcessCommandExecute(object p) => true;
        private void OnStartProcessCommandExecute(object p)
        {
            new Thread(ComputeValue).Start();
        }

        public ICommand StopProcessCommand { get; }
        private bool CanStopProcessCommandExecute(object p) => true;
        private void OnStopProcessCommandExecute(object p)
        {

        }

        public ICommand DrawGraphCommand { get; }
        private bool CanDrawGraphCommandExecute(object p) => true;
        private void OnDrawGraphCommandExecute(object p)
        {
            MyPlotModel.Series.Clear();
            MyPlotModel.Series.Add(new OxyPlot.Series.LineSeries { ItemsSource = TestDataPoints, DataFieldX = "XValue", DataFieldY = "YValue", Color = OxyColor.FromRgb(255, 0, 0) });
            MyPlotModel.InvalidatePlot(true);
        }
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
        public MainWindowViewModel(CountriesStatisticViewModel Statistic, IAsyncDataService AsyncData)
        {
            CountriesStatisticViewModel = Statistic;
            _AsyncData = AsyncData;
            Statistic.MainModel = this;
            #region Команды
            StartProcessCommand = new LambdaCommand(OnStartProcessCommandExecute, CanStartProcessCommandExecute);
            StopProcessCommand = new LambdaCommand(OnStopProcessCommandExecute, CanStopProcessCommandExecute);

            DrawGraphCommand = new LambdaCommand(OnDrawGraphCommandExecute, CanDrawGraphCommandExecute);

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

            //MyPlotModel.Series.Add(new OxyPlot.Series.LineSeries { ItemsSource = TestDataPoints, DataFieldX = "XValue", DataFieldY = "YValue", Color = OxyColor.FromRgb(255, 0, 0) });
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
            _SelectedGroupStudentCollection.Filter += OnStudentsFilter;
            _SelectedGroupStudentCollection.SortDescriptions.Add(new SortDescription("Name", ListSortDirection.Ascending));
            #endregion

        }
    }
}
