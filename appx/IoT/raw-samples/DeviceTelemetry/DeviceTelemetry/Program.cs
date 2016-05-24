
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.Devices;
using Microsoft.Azure.Devices.Client;

namespace DeviceTelemetrt
{
    class Program
    {
        static void Main(string[] args)
        {

            // Commands
            // This is similar to the one sent by the rule processor 
            // as we have seen in the key note demo.
            var cts = new CancellationTokenSource();
            
            var taskSending = TelemetrySendLoop(cts.Token);



            
            Console.WriteLine("Sending telemetry, press any key to stop");
            Console.Read();

            Console.WriteLine("Stopping..");
            cts.Cancel();
            Console.WriteLine("Stopped, done!");

            Console.Read();


        }

        static async Task TelemetrySendLoop(CancellationToken ct)
        {
            // Get DeviceId,DeviceKey & IoTHubHostName from config.
            var DeviceId = ConfigurationManager.AppSettings["DeviceId"]; 
            var DeviceKey = ConfigurationManager.AppSettings["DeviceKey"]; 
            var IoTHubHostName = ConfigurationManager.AppSettings["IoTHubHostName"];


            // Generate the symmetric key
            var authMethod = new DeviceAuthenticationWithRegistrySymmetricKey(DeviceId, DeviceKey);

            // Build the connection string
            var connecitonString = IotHubConnectionStringBuilder.Create(IoTHubHostName, authMethod).ToString(); 
            var Deviceclient =  DeviceClient.CreateFromConnectionString(connecitonString, TransportType.Http1);


            Console.WriteLine(string.Format("Device {0} Connected to Hub{1}", DeviceId, IoTHubHostName));

            while (!ct.IsCancellationRequested)
            {

                var messageObject = new
                {
                    DeviceID = DeviceId,
                    Temperature = 34.2,
                    ExternalTemperature = 38.7, 
                    Humidity = 37.7
                };

                // to Json
                var messageJson = JsonConvert.SerializeObject(messageObject);
                // to device message
                var message = new Message(Encoding.UTF8.GetBytes(messageJson));

                Console.WriteLine(string.Format("Sending : {0}", messageJson));

                await Deviceclient.SendEventAsync(message);
                await Task.Delay(200);
            }
        }
    }
}
