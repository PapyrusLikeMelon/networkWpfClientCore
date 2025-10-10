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

        public RelayCommand HomeViewCommand { get; set; }

        public RelayCommand UdpClientViewCommand { get; set; }
        public RelayCommand UdpServerViewCommand { get; set; }
        public RelayCommand TcpClientViewCommand { get; set; }
        public RelayCommand TcpServerViewCommand { get; set; }

        public RelayCommand CloseAppCommand { get; set; }
 
        public RelayCommand HttpServerViewCommand { get; set; }

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

            CurrentView = HomeVM;

            HomeViewCommand = new RelayCommand(o =>
            {
                CurrentView = HomeVM;
            });

            UdpClientViewCommand = new RelayCommand(o =>
            {
                CurrentView = UdpClientVM;
            });

            UdpServerViewCommand = new RelayCommand(o =>
            {
                CurrentView = UdpServerVM;
            });


            CloseAppCommand = new RelayCommand(o =>
            {
                Application.Current.Shutdown();
            });
        }
    }
}
