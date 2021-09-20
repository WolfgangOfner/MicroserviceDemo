using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Azure.Messaging.ServiceBus;
using KedaDemo.Messaging.Options.v1;
using Microsoft.Extensions.Options;

namespace KedaDemo.Messaging.Writer
{
    public class ServiceBusMessageWriter : IServiceBusMessageWriter
    {
        private readonly string _connectionString;
        private readonly string _queueName;

        public ServiceBusMessageWriter(IOptions<AzureServiceBusConfiguration> serviceBusOptions)
        {
            _connectionString = serviceBusOptions.Value.ConnectionString;
            _queueName = serviceBusOptions.Value.QueueName;
        }

        public async Task WriteMessagesToKedaQueue(int numberOfQueueItems)
        {
            await using (var client = new ServiceBusClient(_connectionString))
            {
                var serviceBusSender = client.CreateSender(_queueName);

                var randomMessages = CreateRandomMessages(numberOfQueueItems);

                await serviceBusSender.SendMessagesAsync(randomMessages);
            }
        }

        private static IEnumerable<ServiceBusMessage> CreateRandomMessages(in int numberOfQueueItems)
        {
            var randomMessages = new List<ServiceBusMessage>();

            for (var i = 0; i < numberOfQueueItems; i++)
            {
                randomMessages.Add(new ServiceBusMessage(new Guid().ToString()));
            }

            return randomMessages;
        }
    }
}