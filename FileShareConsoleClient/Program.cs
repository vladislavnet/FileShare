using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileShareConsoleClient
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Выберите команду");
                Console.WriteLine("1. Загрузить файл");
                Console.WriteLine("2. Выгрузить файл");
                string command = Console.ReadLine();
                switch (command)
                {
                    case "1":
                        downloadFile();
                        break;
                }
            }
        }

        static void downloadFile()
        {
            Console.WriteLine("Выберите файл");

        }
    }
}
