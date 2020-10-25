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
                    case "2":
                        uploadFile();
                        break;
                }
            }
        }

        static void downloadFile()
        {
            try
            {
                Console.WriteLine("Выберите файл");
                string fileName = "Test.txt";
                string pathDirectory = getFileDirectory();
                if (!checkDerectory(pathDirectory))
                    createDirectory(pathDirectory);


                using (var client = new FileTransferServiceClient())
                {
                    using (FileStream fstream = new FileStream(Path.Combine(pathDirectory, fileName),
                        FileMode.OpenOrCreate))
                    {
                        client.DownloadFile(ref fileName, out Stream stream);
                        int byteLenght = 2048;
                        int readByte = 0;
                        byte[] array = new byte[byteLenght];
                        while (true)
                        {
                            readByte = stream.Read(array, 0, byteLenght);
                            if (readByte < 1)
                                break;
                            fstream.Write(array, 0, readByte);
                        }
                    }
                }
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        static void uploadFile()
        {
            try
            {
                Console.WriteLine("Выберите файл");
                string pathDirectory = getUploadFileDirectory();
                if (!checkDerectory(pathDirectory))
                    createDirectory(pathDirectory);
                string[] files = getUploadFilesName(pathDirectory);
                showUploadFiles(files);
                int index = int.Parse(Console.ReadLine());
                string pathFile = getFile(index, files);
                using (FileStream fstream = File.OpenRead(pathFile))
                {
                    using (var client = new FileTransferServiceClient())
                    {
                        client.UploadFile(getFileName(pathFile), fstream.Length, fstream);
                    }
                }
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }


        static string getFileDirectory() => Path.Combine(Directory.GetCurrentDirectory(), "Files");

        static string getUploadFileDirectory() => Path.Combine(Directory.GetCurrentDirectory(), "UploadFiles");

        static string[] getUploadFilesName(string pathDirectory) => Directory.GetFiles(pathDirectory);
        
        static bool checkDerectory(string pathDirectory) => Directory.Exists(pathDirectory);

        static void createDirectory(string pathDirectory) => Directory.CreateDirectory(pathDirectory);

        static void showFiles(string[] files)
        {
            int i = 1;
            foreach (string file in files)
            {
                Console.WriteLine($"{i++}. {file}");
            }
        }

        static void showUploadFiles(string[] files)
        {
            int i = 1;
            foreach (string file in files)
            {
                FileInfo fileInfo = new FileInfo(file);
                Console.WriteLine($"{i++}. {fileInfo.Name}");
            }
        }

        static string getFile(int index, string[] files)
        {
            if (files.Length == 0)
                return string.Empty;
            if (index < 1 && index > files.Length)
                throw new ArgumentOutOfRangeException("index", "Вы выбрали неккоректное значение из списка файлов");
            return files[index - 1];
        }

        static string getFileName(string pathFile) => new FileInfo(pathFile).Name;
    }
}
