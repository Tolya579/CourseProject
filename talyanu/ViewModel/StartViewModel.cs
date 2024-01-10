using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using talyanu.Interface;
using talyanu.Model;
using talyanu.View;

namespace talyanu.ViewModel
{
    public class StartViewModel : ViewModelBase
    {
        private IRouteSelectionStrategy routeSelectionStrategy;
        MainWindowViewModel mwvm;
        public StartViewModel(MainWindowViewModel mainWindowViewModel)
        {
            
            mwvm = mainWindowViewModel;
            var StartView = new StartView();
            StartView.DataContext = this;

        }

        public void SetRouteSelectionStrategy(IRouteSelectionStrategy strategy)
        {
            routeSelectionStrategy = strategy;
        }

        public ICommand ShowRoute
        {
            get => new RelayCommand(
                (_) => ShowRouteImplementation()
                );
        }
        private void ShowRouteImplementation()
        {
            var routeViewModel = new RouteViewModel(mwvm, this, routeSelectionStrategy);
            mwvm.Content = routeViewModel;
        }


        public ICommand ShowNewBus
        {
            get => new RelayCommand(
                (_) => ShowNewBusImplementation()
                );
        }
        private void ShowNewBusImplementation()
        {
            mwvm.Content = new NewBusViewModel(mwvm, this);
        }

        public List<string> StationNames { get; } = DataWorker.GetAllStations().Select(station => station.Name).ToList();

        private string _selectedStation;
        public string SelectedStation
        {
            get { return _selectedStation; }
            set
            {
                if (_selectedStation != value)
                {
                    _selectedStation = value;
                    OnPropertyChanged(nameof(SelectedStation));
                }
            }
        }

        private string _selectedStationSec;
        public string SelectedStationSec
        {
            get { return _selectedStationSec; }
            set
            {
                if (_selectedStationSec != value)
                {
                    _selectedStationSec = value;
                    OnPropertyChanged(nameof(SelectedStationSec));
                }
            }
        }
        private bool _isBusSelectionChecked;
        public bool IsBusSelectionChecked
        {
            get { return _isBusSelectionChecked; }
            set
            {
                if (_isBusSelectionChecked != value)
                {
                    _isBusSelectionChecked = value;
                    OnPropertyChanged(nameof(IsBusSelectionChecked));
                    if (value)
                    {
                        SetRouteSelectionStrategy(new BusSelectionStrategy());
                    }
                }
            }
        }

        private bool _isOneBusSelectionChecked;
        public bool IsOneBusSelectionChecked
        {
            get { return _isOneBusSelectionChecked; }
            set
            {
                if (_isOneBusSelectionChecked != value)
                {
                    _isOneBusSelectionChecked = value;
                    OnPropertyChanged(nameof(IsOneBusSelectionChecked));
                    if (value)
                    {
                        SetRouteSelectionStrategy(new OneBusSelectionStrategy());
                    }
                }
            }
        }

    }
}
