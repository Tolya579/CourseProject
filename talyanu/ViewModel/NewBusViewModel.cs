using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using talyanu.Model;

namespace talyanu.ViewModel
{
    public class NewBusViewModel : ViewModelBase
    {
        private MainWindowViewModel mwvm;
        private StartViewModel startViewModel;

        public NewBusViewModel(MainWindowViewModel mwvm, StartViewModel startViewModel)
        {
            this.mwvm = mwvm;
            this.startViewModel = startViewModel;
        }

        private string _stops;
        public string Stops
        {
            get { return _stops; }
            set
            {
                if (_stops != value)
                {
                    _stops = value;
                    OnPropertyChanged(nameof(Stops));
                }
            }
        }

        private string _numBus;
        public string NumBus
        {
            get { return _numBus; }
            set
            {
                if (_numBus != value)
                {
                    _numBus = value;
                    OnPropertyChanged(nameof(NumBus));
                }
            }
        }

        public ICommand AddBus
        {
            get
            {
                return new RelayCommand
                    (
                    (_) => AddBusImplementation()
                    );
            }
        }

        private void AddBusImplementation()
        {
            bool status = DataWorker.AddData(Stops, NumBus);
            // добавляйте свои действия в соответствии с бизнес-логикой вашего приложения
            if (status)
            {
                Status = "Данные добавлены в базу данных";
            }
            else
            {
                Status = "Произошла ошибка при добавлении данных";
            }
        }

        private string _status;
        public string Status
        {
            get { return _status; }
            set
            {
                if (_status != value)
                {
                    _status = value;
                    OnPropertyChanged(nameof(Status));
                }
            }
        }

        public ICommand BackMenu
        {
            get
            {
                return new RelayCommand
                    (
                    (_) => BackMenuImplementation()
                    );
            }
        }

        public void BackMenuImplementation()
        {
            mwvm.Content = startViewModel;
        }
    }
}
