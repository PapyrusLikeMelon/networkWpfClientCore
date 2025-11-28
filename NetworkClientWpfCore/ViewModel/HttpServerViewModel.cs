using CommunityToolkit.Mvvm.Input;
using System.Net.Sockets;
using System.Text;
using System.Windows;

namespace NetworkClientWpfCore.ViewModel
{
    public class HttpServerViewModel
    {
        public RelayCommand SendMessageCommand { private get; set; }
        public HttpServerViewModel()
        {
            SendMessageCommand = new RelayCommand(() =>
            {
                MessageBox.Show("Http Functionality not implemented yet!");
            });
        }
        public void ShowMessage()
        {
            SendMessageCommand.Execute(null);
        }
    }
}