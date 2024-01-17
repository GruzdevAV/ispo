using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Lab1.ServerC
{
    class ClientHandler
    {
        public TcpClient clientSocket;
        string message =    "HTTP/1.1 200 OK\n" +
                            "Date: Wed, 11 Feb 2009 11:20:59 GMT\n" +
                            "Server: Apache\n" +
                            "Last-Modified: Wed, 11 Feb 2021 11:20:59 GMT\n" +
                            "Content-Type: text/html; charset=utf-8-bom\n" +
                            "<!DOCTYPE html>\n" +
                            "<html>\n" +
                            "<body>\n" +
                            "<h1>My First Heading</h1>\n" +
                            "<p>My first paragraph.</p>\n" +
                            "</body>\n" +
                            "</html>\n\r\n";
        public void RunClient()
        {
            StreamReader readerStream = new StreamReader(clientSocket.GetStream());
            NetworkStream writerStream = clientSocket.GetStream();
            var read = readerStream.ReadLine();
            Console.WriteLine("Got connection: " + read);
            Console.WriteLine("Sending message: "+ message);
            byte[] dataWrite = Encoding.ASCII.GetBytes(message);
            writerStream.Write(dataWrite, 0, dataWrite.Length);
            Console.WriteLine("Closing socket");
            clientSocket.Close();
        }
    }
}


