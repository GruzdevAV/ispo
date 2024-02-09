using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AsInTheInstruction
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool Done { get; set; } = true;
        private UdpClient Client { get; set; }
        private IPAddress GroupAddress { get; set; }
        private int LocalPort { get; set; }
        private int RemotePort { get; set; }
        private int Ttl { get; set; }

        private IPEndPoint RemoteEP { get; set; }
        private UnicodeEncoding Encoding = new UnicodeEncoding();

        private string Nickname { get; set; }
        private string Message { get; set; }

        private readonly SynchronizationContext _syncContext;

        public MainWindow()
        {
            InitializeComponent();
            try
            {
                NameValueCollection configuration = ConfigurationSettings.AppSettings;

                GroupAddress = IPAddress.Parse(configuration["GroupAddress"]);
                LocalPort = int.Parse(configuration["LocalPort"]);
                RemotePort = int.Parse(configuration["RemotePort"]);
                Ttl = int.Parse(configuration["Ttl"]);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Error Mulricast Chart", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            _syncContext = SynchronizationContext.Current;
        }

        private void BtnStart_Click(object sender, RoutedEventArgs e)
        {
            Nickname = textName.Text;
            textName.IsReadOnly = true;
            btnStop.IsEnabled = btnSend.IsEnabled = true;

            try
            {
                Client = new UdpClient(LocalPort);
                Client.JoinMulticastGroup(GroupAddress, Ttl);

                RemoteEP = new IPEndPoint(GroupAddress, RemotePort);
                Thread receiver = new Thread(new ThreadStart(Listener));
                receiver.IsBackground = true;
                receiver.Start();

                byte[] data = Encoding.GetBytes($"{Nickname} has joined the chat");
                Client.Send(data, data.Length, RemoteEP);
            }
            catch (SocketException ex)
            {
                MessageBox.Show(this, ex.Message, "Error Mulricast Chart", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Listener()
        {
            Done = false;
            try
            {
                while (!Done)
                {
                    IPEndPoint ep = null;
                    byte[] buffer = Client.Receive(ref ep);
                    Message = Encoding.GetString(buffer);

                    _syncContext.Post(o => DisolayReceivedMessage(), null);
                }
            }
            catch (Exception ex)
            {
                if (Done)
                    return;
                MessageBox.Show(this, ex.Message, "Error Mulricast Chart", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void DisolayReceivedMessage()
        {
            string time = DateTime.Now.ToString("t");
            textMessages.Text = $"{time} {Message}\r\n{textMessages.Text}";
        }

        private void BtnStop_Click(object sender, RoutedEventArgs e)
        {
            StopListener();
        }

        private void BtnSend_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                byte[] data = Encoding.GetBytes($"{Nickname}: {textMessage.Text}");
                Client.Send(data, data.Length, RemoteEP);
                textMessage.Clear();
                textMessage.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Error Mulricast Chart", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void StopListener()
        {
            byte[] data = Encoding.GetBytes($"{Nickname} has left the chat");
            Client.Send(data, data.Length, RemoteEP);

            Client.DropMulticastGroup(GroupAddress);
            Client.Close();

            Done = true;
            btnStart.IsEnabled = true;
            btnStop.IsEnabled = btnSend.IsEnabled = false;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!Done)
                StopListener();
        }
    }
}
