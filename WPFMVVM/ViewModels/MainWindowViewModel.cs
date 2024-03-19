using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFMVVM.ViewModels.Base;

namespace WPFMVVM.ViewModels
{
    internal class MainWindowViewModel : ViewModel
    {
        private string _Title = "Анализ статистики";
        /// <summary>Заголовок окна</summary>
        public string Title
        {
            get => _Title;
            set => Set(ref _Title, value);
        }
    }
}
