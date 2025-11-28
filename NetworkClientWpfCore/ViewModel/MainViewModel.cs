using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace NetworkClientWpfCore.ViewModel
{
    public class MainViewModel : ObserveableObject
    {
        public HomeViewModel HomeVM { get; private set; }

        public UdpClientViewModel UdpClientVM { get; private set; }
        public UdpServerViewModel UdpServerVM { get; private set; }
        public HttpServerViewModel HttpServerVM { get; private set; }

        public RelayCommand HomeViewCommand { get; set; }

        public RelayCommand UdpClientViewCommand { get; set; }
        public RelayCommand UdpServerViewCommand { get; set; }
        public RelayCommand HttpServerViewCommand { get; set; }

        public RelayCommand CloseAppCommand { get; set; }
 

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
        public MainViewModel()
        {
            HomeVM = new HomeViewModel();
            UdpClientVM = new UdpClientViewModel();
            UdpServerVM = new UdpServerViewModel();
            HttpServerVM = new HttpServerViewModel();
            CurrentView = HomeVM;

            HomeViewCommand = new RelayCommand(() =>
            {
                CurrentView = HomeVM;
            });

            UdpClientViewCommand = new RelayCommand(() =>
            {
                CurrentView = UdpClientVM;
            });

            UdpServerViewCommand = new RelayCommand(() =>
            {
                CurrentView = UdpServerVM;
            });
            HttpServerViewCommand = new RelayCommand(() =>
            {
                CurrentView = HttpServerVM;
                HttpServerVM.ShowMessage();
            });


            CloseAppCommand = new RelayCommand(() =>
            {
                Application.Current.Shutdown();
            });
        }
    }
}
