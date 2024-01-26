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
        public Action<string> SendMessage;
        public ClientHandler()
        {
        }
        public ClientHandler(Action<string> action)
        {
            SendMessage += action;
        }
        public void RunClient()
        {
            StreamReader readerStream = new StreamReader(clientSocket.GetStream());
            NetworkStream writerStream = clientSocket.GetStream();
            string returnData = readerStream.ReadLine();
            string name = returnData;
            SendMessage?.Invoke("Welcome " + name + " to the server");
            while (true)
            {
                returnData = readerStream.ReadLine();
                if (returnData.ToUpper().Contains("/QUIT"))
                {
                    SendMessage?.Invoke("Goodbye, " + name);
                    break;
                }
                SendMessage?.Invoke(name + ": " + returnData);
                byte[] dataWrite = Encoding.ASCII.GetBytes(returnData + "\r\n");
                writerStream.Write(dataWrite, 0, dataWrite.Length);
            }
            clientSocket.Close();
        }
    }
}
