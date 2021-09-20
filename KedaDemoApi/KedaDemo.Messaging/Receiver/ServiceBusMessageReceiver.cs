using System;
using System.Threading.Tasks;
using Azure.Messaging.ServiceBus;
using KedaDemo.Messaging.Options.v1;
using Microsoft.Extensions.Options;

namespace KedaDemo.Messaging.Receiver
{
    public class ServiceBusMessageReceiver : IServiceBusMessageReceiver
    {
        private readonly string _connectionString;
        private readonly string _queueName;

        public ServiceBusMessageReceiver(IOptions<AzureServiceBusConfiguration> serviceBusOptions)
        {
            _connectionString = serviceBusOptions.Value.ConnectionString;
            _queueName = serviceBusOptions.Value.QueueName;
        }

        public async Task<int> ReceiveMessagesFromKedaQueue()
        {
            var hasMoreMessages = true;
            var messagesReceived = 0;
            
            await using (var client = new ServiceBusClient(_connectionString))
            {
                var serviceBusReceiver = client.CreateReceiver(_queueName, new ServiceBusReceiverOptions{ReceiveMode = ServiceBusReceiveMode.ReceiveAndDelete});
                
                do
                {
                    var receiveMessagesAsync = serviceBusReceiver.ReceiveMessageAsync(TimeSpan.FromSeconds(0.5)).Result;

                    if (receiveMessagesAsync is null)
                    {
                        hasMoreMessages = false;
                    }
                    else
                    {
                        messagesReceived++;
                    }
                } while (hasMoreMessages);
            }

            return messagesReceived;
        }
    }
}