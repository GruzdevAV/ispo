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

                //GroupAddress = IPAddress.Parse("239.255.255.255");
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
            btnStart.IsEnabled = false;
            textName.IsReadOnly =
            btnStop.IsEnabled = btnSend.IsEnabled = true;

            try
            {
                Client = new UdpClient(LocalPort);
                Client.JoinMulticastGroup(GroupAddress, Ttl);
                //Client.Client.Bind(new IPEndPoint(IPAddress.Any, RemotePort));
                //Client.JoinMulticastGroup(GroupAddress,IPAddress.Ex);
                Client.MulticastLoopback = true;

                RemoteEP = new IPEndPoint(GroupAddress, RemotePort);
                Thread receiver = new Thread(new ThreadStart(Listener))
                {
                    IsBackground = true
                };
                receiver.Start();
                //AddressFamily InterNetwork    System.Net.Sockets.AddressFamily

                var msg = $"{Nickname} has joined the chat";
                byte[] data = Encoding.GetBytes(msg);
                Client.Send(data, data.Length, RemoteEP);
                string time = DateTime.Now.ToString("t");
                textMessages.Text = $"{time} {msg}\r\n{textMessages.Text}";
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
                    //IPEndPoint ep = new IPEndPoint(IPAddress.Any,0);
                    byte[] buffer = Client.Receive(ref ep);
                    //MessageBox.Show(ep.ToString());
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
                var msg = $"{Nickname}: {textMessage.Text}";
                byte[] data = Encoding.GetBytes(msg);
                string time = DateTime.Now.ToString("t");
                textMessages.Text = $"{time} {msg}\r\n{textMessages.Text}";
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
            var msg = $"{Nickname} has left the chat";
            byte[] data = Encoding.GetBytes(msg);
            string time = DateTime.Now.ToString("t");
            textMessages.Text = $"{time} {msg}\r\n{textMessages.Text}";
            Client.Send(data, data.Length, RemoteEP);

            Client.DropMulticastGroup(GroupAddress);
            Client.Close();

            Done = true;
            btnStart.IsEnabled = true;
            textName.IsReadOnly = btnStop.IsEnabled = btnSend.IsEnabled = false;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!Done)
                StopListener();
        }
    }
}
