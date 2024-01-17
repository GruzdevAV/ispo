using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static string message = "HTTP/1.1 200 Everything is fine\nServer: my_sharp_server\nContent-Type: text/html;charset: UTF-8\n\n"+
                       "<!DOCTYPE html>" +                                      //Environment.NewLine +
                       "<html>" +                                               Environment.NewLine +
                       "<body>" +                                               Environment.NewLine +
                       "<h1>My First Heading</h1>" +                            Environment.NewLine +
                       "<p>My first paragraph.</p>" +                           Environment.NewLine +
                       "<p>{0}</p>" +                                           Environment.NewLine +
                       "</body>" +                                              Environment.NewLine +
                       "</html>" +                                              Environment.NewLine +
                        Environment.NewLine;
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
            Console.ReadKey();

        }
    }
}
