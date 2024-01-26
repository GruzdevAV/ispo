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
        public static int nClients;
        const int ECHO_PORT = 8081;
        public void Launch()
        {
            try
            {
                TcpListener clientListener = new TcpListener(Global.PORT_NUMBER);
                clientListener.Start();
                Console.WriteLine("Waiting for connections...");

                while (nClients < 3)
                {
                    TcpClient client = clientListener.AcceptTcpClient();
                    ClientHandler cHandler = new ClientHandler();
                    cHandler.clientSocket = client;
                    Thread clientThread = new Thread(new ThreadStart(cHandler.RunClient));
                    clientThread.Start();
                    nClients++;
                }
                clientListener.Stop();
            }
            catch (Exception exp)
            {
                Console.WriteLine("Exception: " + exp.Message);
            }
            Console.WriteLine("Shutting down...");
        }
    }
}
