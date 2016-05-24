using Microsoft.ServiceBus.Messaging;
using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage.Auth;

namespace ExecuterProgram
{
    class Executer
    {
        
        

        private static void WriteUsage()
        {
            
        }


        static void Main(string[] args)
        {
            if (args.Length < 11)
            {
                WriteUsage();
                return;
            }



            






            string eventHubConnectionString = "{Event Hub Connection String}";
            string eventHubName = "{EventHub Name}";
            string eventHubConsumerGroupName = "{Event Hub Consumer Name}";
            string storageAccountName = "{Storage Account Name, to save checpoints for Event Hub}";
            string storageAccountKey = "Storage Account Key";
            string eventProcessorHostName = "myprocessor";




            


            string storageConnectionString = string.Format("DefaultEndpointsProtocol=https;AccountName={0};AccountKey={1}", storageAccountName, storageAccountKey);


            EventProcessorHost eventProcessorHost = new EventProcessorHost(eventProcessorHostName, 
                                                                           eventHubName,
                                                                           eventHubConsumerGroupName, 
                                                                           eventHubConnectionString, 
                                                                           storageConnectionString);

            Console.WriteLine("Registering Executer EventProcessor...");

            var options = new EventProcessorOptions();
            options.InitialOffsetProvider = (pId) => DateTime.UtcNow;
            options.ExceptionReceived += (sender, e) => { Console.WriteLine(e.Exception); };
            eventProcessorHost.RegisterEventProcessorAsync<RuleExecuterEventProcessor>(options).Wait();

            Console.WriteLine("Receiving. Press enter key to stop the Executer and stop the app.");
            Console.ReadLine();

            eventProcessorHost.UnregisterEventProcessorAsync().Wait();
            
        }
    }
}
