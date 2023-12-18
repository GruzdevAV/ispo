using DatabaseController;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var str = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"D:\\HW\\UP_02.01\\Задание 5\\App\\Mockup\\App_Data\\UP_02_01_DB.mdf\";Integrated Security=True";

            Database database = Database.GetInstance(str);
            var a = database.GetRowsData("SELECT * FROM Sales");

            foreach (var  row in a)
            {
                foreach (var col in row)
                {
                    Console.Write($"{col} ");
                }
                Console.WriteLine();
            }
            Console.ReadLine();
        }
    }
}
