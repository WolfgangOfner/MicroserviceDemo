using Azure.Messaging.ServiceBus;
using CustomerApi.Domain.Entities;
using CustomerApi.Messaging.Send.Options.v1;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace CustomerApi.Messaging.Send.Sender.v1
{
    public class CustomerUpdateSenderServiceBus : ICustomerUpdateSender
    {
        private readonly string _connectionString;
        private readonly string _queueName;

        public CustomerUpdateSenderServiceBus(IOptions<AzureServiceBusConfiguration> serviceBusOptions)
        {
            _connectionString = serviceBusOptions.Value.ConnectionString;
            _queueName = serviceBusOptions.Value.QueueName;
        }

        public async void SendCustomer(Customer customer)
        {
            // todo add exception handling when queue is not accessible
            await using (var client = new ServiceBusClient(_connectionString))
            {
                var sender = client.CreateSender(_queueName);

                var json = JsonConvert.SerializeObject(customer);
                var message = new ServiceBusMessage(json);

                await sender.SendMessageAsync(message);
            }
        }
    }
}