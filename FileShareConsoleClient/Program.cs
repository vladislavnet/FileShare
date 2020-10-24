using FileShareConsoleClient.FIleTransferServiceReference;
using System;
using System.Collections.Generic;
using System.IO;
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
            string fileName = "Test.txt";
            using (var client = new FileTransferServiceClient())
            {
                client.DownloadFile(ref fileName, out Stream stream);
                int byteLenght = 4;
                byte[] array = new byte[byteLenght];
                int readByte = 0;
                while (true)
                {
                    readByte = stream.Read(array, 0, byteLenght);
                    Console.WriteLine(readByte);
                    if (readByte < 1)
                        break;
                    Console.WriteLine(Encoding.Default.GetString(array));
                }
                //Console.WriteLine(Encoding.Default.GetString(array));
            }
        }
    }
}
