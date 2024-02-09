using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Server
{
    class Server
    {
        public void Launch()
        {
            Program.SendMessage += (msg) => Console.WriteLine(msg);
            TcpListener clientListener;
            try
            {
                clientListener = new TcpListener(Global.PORT_NUMBER);
                clientListener.Start();
                Program.SendMessage?.Invoke("Waiting for connections...");

                while (true)
                {
                    TcpClient client = clientListener.AcceptTcpClient();
                    ClientHandler cHandler = new ClientHandler();
                    cHandler.clientSocket = client;
                    Thread clientThread = new Thread(new ThreadStart(cHandler.RunClient));
                    clientThread.Start();
                }
            }
            catch (Exception exp)
            {
                Program.SendMessage?.Invoke("Exception: " + exp.Message);
            }
            Program.SendMessage?.Invoke("Shutting down...");
        }
    }
}
