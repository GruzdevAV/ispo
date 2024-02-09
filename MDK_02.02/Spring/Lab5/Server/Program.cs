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
        public static Action<string> SendMessage;
        static void Main(string[] args)
        {
            var server = new Server();
            server.Launch();
        }
    }
}
