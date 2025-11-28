using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace NetworkClientWpfCore.ViewModel
{
    // Note: CommunityToolkit standard class is usually "ObservableObject" (no 'e' in middle)
    public class UdpServerViewModel : ObservableObject
    {
        private UdpClient _udpListener;
        private bool _isListening;
        private string _receivedLog = "Server Log:\n";
        private int _port = 8080; // Default port

        // --- Properties ---

        public int Port
        {
            get => _port;
            set
            {
                SetProperty(ref _port, value);
                // Notify that the command execution state might change
                ToggleListeningCommand.NotifyCanExecuteChanged();
            }
        }

        public string ReceivedLog
        {
            get => _receivedLog;
            set => SetProperty(ref _receivedLog, value);
        }

        public bool IsListening
        {
            get => _isListening;
            private set
            {
                SetProperty(ref _isListening, value);
                ToggleListeningCommand.NotifyCanExecuteChanged();
            }
        }

        // --- Commands ---

        public IRelayCommand ToggleListeningCommand { get; }
        public IRelayCommand ClearLogCommand { get; }

        // --- Constructor ---

        public UdpServerViewModel()
        {
            ToggleListeningCommand = new RelayCommand(ToggleListening);
            ClearLogCommand = new RelayCommand(() => ReceivedLog = "Server Log:\n");
        }

        // --- Logic ---

        private void ToggleListening()
        {
            if (IsListening)
            {
                StopListening();
            }
            else
            {
                StartListening();
            }
        }

        private async void StartListening()
        {
            try
            {
                // Initialize UDP Client on the specified port
                _udpListener = new UdpClient(Port);
                IsListening = true;

                AddToLog($"--- Started listening on Port {Port} ---");

                while (IsListening)
                {
                    // This creates a non-blocking await. The UI stays responsive.
                    // ReceiveAsync returns a UdpReceiveResult containing Buffer and RemoteEndPoint
                    var result = await _udpListener.ReceiveAsync();

                    string message = Encoding.ASCII.GetString(result.Buffer);
                    string sender = result.RemoteEndPoint.ToString();

                    AddToLog($"[{sender}]: {message}");
                }
            }
            catch (ObjectDisposedException)
            {
                // This happens when we close the socket to stop listening. 
                // It is expected behavior to break the loop.
                AddToLog("--- Stopped listening ---");
            }
            catch (SocketException ex)
            {
                AddToLog($"Error: {ex.Message}");
                StopListening();
            }
            catch (Exception ex)
            {
                AddToLog($"Critical Error: {ex.Message}");
                StopListening();
            }
        }

        private void StopListening()
        {
            if (_udpListener != null)
            {
                _udpListener.Close(); // This breaks the ReceiveAsync loop
                _udpListener = null;
            }
            IsListening = false;
        }

        private void AddToLog(string text)
        {
            // Appending text. In a real app, consider using an ObservableCollection<string>
            // to avoid string concatenation performance issues over time.
            ReceivedLog += $"{DateTime.Now.ToLongTimeString()} - {text}\n";
        }
    }
}