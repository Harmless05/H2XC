using System;
using System.Windows;
using H2XC.Core;

namespace H2XC.MVVM.ViewModel
{
    class MainVM : ObservableObject
    {
        public RelayCommand HomeViewCommand { get; set; }
        public RelayCommand IP2ADRSViewCommand { get; set; }
        public RelayCommand LocInfoViewCommand { get; set; }
        public RelayCommand AboutViewCommand { get; set; }

        public HomeVM HVM { get; set; }
        public IP2ADRSVM IPVM { get; set; }
        public LocInfoVM LIVM { get; set; }
        public AboutVM AVM { get; set; }

        private object _currentView;
        public object CurrentView
        {
            get { return _currentView; }
            set
            {
                _currentView = value;
                OnPropertyChanged();
            }
        }

        public MainVM()
        {
            HVM = new HomeVM();
            IPVM = new IP2ADRSVM();
            LIVM = new LocInfoVM();
            AVM = new AboutVM();
            CurrentView = HVM;

            HomeViewCommand = new RelayCommand(o => CurrentView = HVM);
            IP2ADRSViewCommand = new RelayCommand(o => CurrentView = IPVM);
            LocInfoViewCommand = new RelayCommand(o =>
            {
                var confirmResult = MessageBox.Show("Your PUBLIC IP address WILL be VISIBLE. Do you want to continue?", "Continue?", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (confirmResult == MessageBoxResult.Yes)
                {
                    CurrentView = LIVM;
                }
            });
            AboutViewCommand = new RelayCommand(o => CurrentView = AVM);
        }
    }
}