using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PackManagerTest2;

namespace UsingNuGetPack
{
    class Program
    {
        static void Main(string[] args)
        {
            var a = new Lab7Class();
            Console.WriteLine(a.PublicString);
            a.PublicString = "pub";
            Console.WriteLine(a.PublicString);
            Console.WriteLine(a.AccessToInternalString);
            a.AccessToInternalString = "int";
            Console.WriteLine(a.AccessToInternalString);
            Console.Read();
        }
    }
}
