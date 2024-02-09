using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    class ClientHandler
    {
        public TcpClient clientSocket;
        NetworkStream writerStream;
        public ClientHandler()
        {
        }
        ~ClientHandler()
        {
            Program.SendMessage -= WriteToServer;
        }
        public void WriteToServer(string message)
        {
            byte[] dataWrite = Encoding.UTF8.GetBytes($"{message}\r\n");
            writerStream.Write(dataWrite, 0, dataWrite.Length);
        }
        public void RunClient()
        {
            StreamReader readerStream = new StreamReader(clientSocket.GetStream());
            writerStream = clientSocket.GetStream();

            Program.SendMessage += WriteToServer;

            string returnData = readerStream.ReadLine();
            string name = returnData;
            Program.SendMessage?.Invoke($"Welcome, {name}, to the server!");
            while (true)
            {
                returnData = readerStream.ReadLine();
                if (returnData.ToUpper().Contains("/QUIT"))
                {
                    Program.SendMessage?.Invoke($"Goodbye, {name}!");
                    break;
                }
                Program.SendMessage?.Invoke(name + ": " + returnData);
            }
            clientSocket.Close();
        }
    }
}
