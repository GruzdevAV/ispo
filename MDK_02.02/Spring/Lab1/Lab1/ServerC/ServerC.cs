﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Lab1.ServerC
{
    class ServerC
    {
        public static int nClients;
        const int ECHO_PORT = 8081;
        public void Launch()
        {
            try
            {
                TcpListener clientListener = new TcpListener(ECHO_PORT);
                clientListener.Start();
                Console.WriteLine("Waiting for connections...");


                while (nClients < 100)
                {
                    Console.WriteLine("Connection #" + ++nClients);
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
