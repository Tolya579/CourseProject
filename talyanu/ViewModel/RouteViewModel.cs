using System.Collections.Generic;
using System.Windows.Input;
using talyanu.Interface;
using talyanu.Model;

namespace talyanu.ViewModel
{
    public class RouteViewModel : ViewModelBase
    {
        private MainWindowViewModel mwvm;
        private StartViewModel startViewModel;
        

        public RouteViewModel(MainWindowViewModel mwvm, StartViewModel startViewModel, IRouteSelectionStrategy routeSelectionStrategy)
        {
            this.mwvm = mwvm;
            this.startViewModel = startViewModel;
            SelectedStation = startViewModel.SelectedStation;
            SelectedStationSec = startViewModel.SelectedStationSec;
            SelectedBuses = routeSelectionStrategy.SelectRoute(StartPoint, EndPoint);
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

        private string StartPoint;
        private string EndPoint; 

        public string SelectedStation
        {
            get { return StartPoint; }
            set
            {
                if (StartPoint != value)
                {
                    StartPoint = value;
                    OnPropertyChanged(nameof(SelectedStation));
                }
            }
        }
        public string SelectedStationSec
        {
            get { return EndPoint; }
            set
            {
                if (EndPoint != value)
                {
                    EndPoint = value;
                    OnPropertyChanged(nameof(SelectedStation));
                }
            }
        }
        private List<string> _selectedBuses = new List<string>();
        public List<string> SelectedBuses
        {
            get { return _selectedBuses; }
            set
            {
                if (_selectedBuses != value)
                {
                    _selectedBuses = value;
                    OnPropertyChanged(nameof(SelectedBuses));
                }
            }
        }

    }
}