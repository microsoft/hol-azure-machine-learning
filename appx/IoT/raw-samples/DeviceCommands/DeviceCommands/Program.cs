using Microsoft.Azure.Devices;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeviceCommands
{
    class Program
    {
        static void Main(string[] args)
        {
           
            // Commands
            // This is similar to the one sent by the rule processor 
            // as we have seen in the key note demo.
            SendDevicecommand("Fanon").GetAwaiter().GetResult();

            // the following commands control the none blinking led.
            //SendDevicecommand("FanOff").GetAwaiter().GetResult();
            //SendDevicecommand("LedRed").GetAwaiter().GetResult();
            //SendDevicecommand("LedWhite").GetAwaiter().GetResult();
            


            Console.WriteLine("done.");
            Console.Read();


        }

        static async Task SendDevicecommand(string sDeviceState)
        {
            //1- Get Device Id from Config
            var DeviceId = ConfigurationManager.AppSettings["DeviceId"];
            //2- Get IoTHubConneciton String
            var connectionString = ConfigurationManager.AppSettings["IoTHubConnectionString"];

            //3- Create a service client
            var serviceClient = ServiceClient.CreateFromConnectionString(connectionString);

            // 4- Build the Command
            var cmd = new
                        {

                            Name = "ChangeDeviceState",
                            MessageId = ((new Random()).Next(1,10) * 10) + 1,
                            CreatedTime= DateTime.UtcNow.ToString(),
                            Parameters = new  { DeviceState= sDeviceState}
                        };

            //5- To Json
            var cmdJson = JsonConvert.SerializeObject(cmd);

            Console.WriteLine(string.Format("Target Device: {0}", DeviceId));
            Console.WriteLine(string.Format("Command      : {0}", cmdJson));

            // 6- IoTHub Message (cmd is now bytes).
            var cmdMessage = new Message(Encoding.ASCII.GetBytes(cmdJson));
            
            // 7- Send it to IoT Hub, the Hub will route it to the device. 
            // This app does not need direct connectivity to the device
            await serviceClient.SendAsync(DeviceId, cmdMessage);


        }
    }
}
