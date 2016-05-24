using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.ServiceBus.Messaging;
using System.Diagnostics;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using Microsoft.Azure.Devices;
using Newtonsoft.Json;

namespace ExecuterProgram
{
    

    class RuleExecuterEventProcessor : IEventProcessor
    {
        
        public RuleExecuterEventProcessor()
        {
            
        }
        async Task IEventProcessor.CloseAsync(PartitionContext context, CloseReason reason)
        {
            Console.WriteLine("Executer Processor Shutting Down. Partition '{0}', Reason: '{1}'.", context.Lease.PartitionId, reason);
            if (reason == CloseReason.Shutdown)
                await context.CheckpointAsync();
            
        }

        Task IEventProcessor.OpenAsync(PartitionContext context)
        {
            Console.WriteLine("Executer Processor initialized.  Partition: '{0}', Offset: '{1}'", context.Lease.PartitionId, context.Lease.Offset);
            return Task.FromResult<object>(null);
        }

        async Task IEventProcessor.ProcessEventsAsync(PartitionContext context, IEnumerable<EventData> messages)
        {
            foreach (EventData eventData in messages)
            {
                string message = Encoding.UTF8.GetString(eventData.GetBytes());


                // TODO: Work with the message

            }
            // For the sake of the demo we are checkpointing with ever event batch
            await context.CheckpointAsync();

   
        }
    }
}
