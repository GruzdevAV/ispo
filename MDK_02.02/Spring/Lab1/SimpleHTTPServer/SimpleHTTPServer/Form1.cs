using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace SimpleHTTPServer
{
    public partial class Form1 : Form
    {
        private Socket httpServer;
        private int serverPort = 80;
        private Thread thread;
        public Form1()
        {
            InitializeComponent();
        }

        private void StartServerBtn_Click(object sender, EventArgs e)
        {
            serverLogsText.Text = "";

            try
            {
                httpServer = new Socket(SocketType.Stream, ProtocolType.Tcp);
                try
                {
                    serverPort = int.Parse(serverPortText.Text);
                    if (serverPort >= 65535 || serverPort <= 0)
                        throw new Exception("Server port not within the range");
                }
                catch (Exception ex)
                {
                    serverPort = 80;
                    MessageBox.Show("Server Failed to start specified port");
                }

                thread = new Thread(new ThreadStart(connectionThreadMethod));
                thread.Start();

                StartServerBtn.Enabled = false;
                StopServerBtn.Enabled = true;
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Error while starting server");
                serverLogsText.Text = "Error while starting server";
            }

            serverLogsText.Text = "server Started";
        }

        private void StopServerBtn_Click(object sender, EventArgs e)
        {
            try
            {
                httpServer.Close();

                thread.Abort();

                StartServerBtn.Enabled = true;
                StopServerBtn.Enabled = false;
            }
            catch (Exception ex)
            {
                ;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            StopServerBtn.Enabled = false;
        }

        private void connectionThreadMethod()
        {
            try
            {
                IPEndPoint endpoint = new IPEndPoint(IPAddress.Any, serverPort);
                httpServer.Bind(endpoint);
                httpServer.Listen(1);
                startListeningForConnection();
            }
            catch (Exception ex)
            {
                ;
            }
        }

        private void startListeningForConnection()
        {
            while (true)
            {
                DateTime time = DateTime.Now;

                string data = "";
                byte[] bytes = new byte[2048];

                Socket client = httpServer.Accept();

                // reading the inbound connection data
                while (true)
                {
                    int numBytes = client.Receive(bytes);
                    data += Encoding.ASCII.GetString(bytes, 0, numBytes);

                    if (data.IndexOf("\n\r") > -1)
                        break;
                }

                serverLogsText.Invoke((MethodInvoker)delegate
               {
                   serverLogsText.Text += "\r\n\r\n";
                   serverLogsText.Text += data;
                   serverLogsText.Text += "\n\n------- End of Request------";
               });

                string message = GetTextBoxText();

                string resHeader = "HTTP/1.1 200 Everything is fine\nServer: my_sharp_server\nContent-Type: text/html;charset: UTF-8\n\n";
                string resBody = "<!DOCTYPE html>" + 
                    "<html>" + 
                        "<head>" + 
                            "<title>My Server</title>" + 
                        "</head>" + 
                        "<body>" + 
                            "<h4>Server time is " + time.ToString() + " </h4>" + 
                            "<p>" + message + "</p>" + 
                        "</body>" + 
                    "</html>";

                string resStr = resHeader + resBody;

                byte[] resData = Encoding.ASCII.GetBytes(resStr);

                client.SendTo(resData, client.RemoteEndPoint);

                client.Close();
            }
        }


        public string GetTextBoxText()
        {
            return messageToBrowser.Text;
        }
    }
}
