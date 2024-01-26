using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    static class Global
    {
        public static int PORT_NUMBER = 8081;
    }
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                TcpListener clientListener = new TcpListener(8081);
                clientListener.Start();
                TcpClient client = clientListener.AcceptTcpClient();
                StreamReader readerStream = new StreamReader(client.GetStream());
                var s = readerStream.ReadLine();
                var t = string.Format(message, DateTime.Now);
                Console.WriteLine(s);
                NetworkStream writerStream = client.GetStream();
                byte[] dataWrite = Encoding.UTF8.GetBytes(t);
                Console.WriteLine(t);
                Console.WriteLine(dataWrite);
                writerStream.Write(dataWrite, 0, dataWrite.Length);
                Console.WriteLine("Closing socket");
                client.Close();
                clientListener.Stop();
            }

        }
    }
}
