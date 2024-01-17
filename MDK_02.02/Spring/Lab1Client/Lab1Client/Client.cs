using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Net;
using System.Threading.Tasks;

namespace Lab1
{
    class Server
    {
        const int ECHO_PORT = 8081;
        static void Main(string[] args)
        {
            try
            {
                var client = new TcpClient("127.0.0.1", ECHO_PORT);
                var reader = new StreamReader(client.GetStream());
                var writer = client.GetStream();
                Console.Write("Your name -> ");
                string name = Console.ReadLine();
                byte[] data = Encoding.ASCII.GetBytes(name + "\r\n");
                writer.Write(data, 0, data.Length);
                while (true)
                {
                    Console.Write(name + ": ");
                    string dataToSend = Console.ReadLine();
                    data = Encoding.ASCII.GetBytes(dataToSend + "\r\n");
                    writer.Write(data, 0, data.Length);
                    if (dataToSend.ToUpper().Contains("/QUIT"))
                        break;
                    string returnData = reader.ReadLine();
                    Console.WriteLine("Server: " + returnData);

                }
                client.Close();
                Console.WriteLine("DEAD");
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
            Console.ReadLine();
        }
    }
}
