using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using talyanu.Model;

namespace talyanu.ViewModel
{
    public class MainWindowViewModel : ViewModelBase
    {

        public MainWindowViewModel()
        { 
            _content = new StartViewModel(this);
        }
        ViewModelBase _content;
        public ViewModelBase Content
        {
            get => _content;
            set
            {
                if (_content == value) return;
                _content = value;
                OnPropertyChanged();
            }
        }

        #region Заголовок окна
        private string _Windowname = "Маршруты автобусов";
        public string Windowname
        {
            get => _Windowname;
            set => Set(ref _Windowname, value);
        }
        #endregion
    }
}
