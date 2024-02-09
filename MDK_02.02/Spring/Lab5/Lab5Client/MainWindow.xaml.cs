using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

namespace Lab5Client
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    enum ClientState { NotConnected, InConnecting, Connected }
    public partial class MainWindow : Window
    {
        Server server;
        ClientState _state;

        internal ClientState State
        {
            get => _state;
            set
            {
                switch (value)
                {
                    case ClientState.NotConnected:
                        btn_send_msg.IsEnabled = false;
                        btn_stop.IsEnabled = false;
                        btn_start.IsEnabled = true;

                        tbox_name.IsReadOnly = false;
                        break;
                    case ClientState.InConnecting:
                        btn_send_msg.IsEnabled = false;
                        btn_stop.IsEnabled = false;
                        btn_start.IsEnabled = false;

                        tbox_name.IsReadOnly = true;
                        break;
                    case ClientState.Connected:
                        btn_send_msg.IsEnabled = true;
                        btn_stop.IsEnabled = true;
                        btn_start.IsEnabled = false;

                        tbox_name.IsReadOnly = true;
                        break;
                }
                _state = value;
            }
        }

        public MainWindow()
        {
            InitializeComponent();

        }

        private void Btn_start_Click(object sender, RoutedEventArgs e)
        {
            server = new Server(action: WriteToChat);
            Action action = () => server.Start();
            var thread = new Thread(new ThreadStart(action.Invoke));
            thread.Start();
        }

        private void Btn_stop_Click(object sender, RoutedEventArgs e)
        {
        }

        private void Btn_send_msg_Click(object sender, RoutedEventArgs e)
        {
        }
        private void WriteToChat(string msg)
        {
            tblock_chat.Text += msg + '\n';
        }
        private void IsServerStarted(bool startedGood)
        {
            if (startedGood)
            {
                //
            }
            else
            {
                //
            }
        }
    }
    class Server
    {
        public int Port { get; set; }
        public Action<string> WriteMessage;
        TcpClient client;
        StreamReader reader;
        NetworkStream writer;
        Thread readingThread;
        bool _reading = false;
        public Server(int port = 8081, Action<string> action = null)
        {
            Port = port;
            if (action != null) WriteMessage += action;

        }
        public void Start(Action<bool> action = null)
        {
            try
            {
                client = new TcpClient("127.0.0.1", Port);
                reader = new StreamReader(client.GetStream());
                writer = client.GetStream();
                _reading = true;
                readingThread = new Thread(new ThreadStart(Reading));
                readingThread.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                action?.Invoke(false);
                return;
            }
            action?.Invoke(true);
        }
        public void Stop()
        {
            _reading = false;
            readingThread.Abort();
            client?.Close();
        }
        public void WriteLine(string msg)
        {
            byte[] data = Encoding.UTF8.GetBytes(msg);
            writer.Write(data, 0, data.Length);
        }
        public void Reading()
        {
            while (_reading)
            {
                string returnData = reader.ReadLine();
                WriteMessage.Invoke("Server: " + returnData);
            }
        }
    }

}
