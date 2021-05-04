using System;
using System.Linq;
using Microsoft.Azure.ServiceBus.Core;
using Microsoft.Azure.WebJobs;
using Newtonsoft.Json;

namespace OrderApi.Messaging.Receive
{
    public class OrderNameUpdateFunction
    {
        private readonly OrderContext _orderContext;

        public OrderNameUpdateFunction(OrderContext orderContext)
        {
            _orderContext = orderContext;
        }

        [FunctionName("OrderNameUpdateFunction")]
        public void Run([ServiceBusTrigger("CustomerQueue", Connection = "QueueConnectionString")] string queueItem, MessageReceiver messageReceiver, string locktoken)
        {
            try
            {
                var updateCustomerFullNameModel = JsonConvert.DeserializeObject<UpdateCustomerFullNameModel>(queueItem);

                var ordersToUpdate = _orderContext.Order.Where(x => x.CustomerGuid == updateCustomerFullNameModel.Id).ToList();

                if (ordersToUpdate.Any())
                {
                    foreach (var order in ordersToUpdate)
                    {
                        order.CustomerFullName = $"{updateCustomerFullNameModel.FirstName} {updateCustomerFullNameModel.LastName}";
                    }

                    _orderContext.Order.UpdateRange(ordersToUpdate);
                    _orderContext.SaveChanges();
                }
                else
                {
                    messageReceiver.DeadLetterAsync(locktoken);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                messageReceiver.DeadLetterAsync(locktoken);
            }
        }
    }
}