﻿using FileShareLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace FileShareHost
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("*****Console Based WCF Host * ****");
            using (ServiceHost serviceHost =
            new ServiceHost(typeof(FileTransferService)))
            {
                serviceHost.Open();
                DisplayHostlnfо(serviceHost);
                Console.WriteLine("The service is ready.");
                Console.WriteLine("Press the Enter key to terminate service.");
                Console.ReadLine();
            }
        }

        static void DisplayHostlnfо(ServiceHost host)
        { 
            Console.WriteLine();
            Console.WriteLine("***** Host Info *****");
            foreach (System.ServiceModel.Description.ServiceEndpoint se
            in host.Description.Endpoints)
            {
                Console.WriteLine("Address: {0}", se.Address); // Адрес
                Console.WriteLine("Binding: {0}", se.Binding.Name); // Привязка
                Console.WriteLine("Contract: {0}", se.Contract.Name); // Контракт
                Console.WriteLine();
            }
            Console.WriteLine("**********************");
        }
}
}
