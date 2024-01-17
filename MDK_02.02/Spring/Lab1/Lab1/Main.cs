using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
namespace Lab1
{
    class Program
    {
        static void Main(string[] args)
        {
            new ServerC.ServerC().Launch();
        }

    }
}
