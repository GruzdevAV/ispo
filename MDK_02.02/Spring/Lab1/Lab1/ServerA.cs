using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Lab1
{
    class ServerA
    {
        public void Launch()
        {
            var clientListener = new TcpListener(8081);

            clientListener.Start();

            Console.WriteLine("Start listening");
            var client = clientListener.AcceptTcpClient();
            var reader = new StreamReader(client.GetStream());
            var writer = client.GetStream();
                string dataFromClient = reader.ReadLine();
                Console.WriteLine(dataFromClient);
                client.Close();
                clientListener.Stop();
            Console.ReadLine();
        }
    }
}
