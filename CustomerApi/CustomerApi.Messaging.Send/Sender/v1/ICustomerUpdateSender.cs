using CustomerApi.Domain.Entities;

namespace CustomerApi.Messaging.Send.Sender.v1
{
    public interface ICustomerUpdateSender
    {
        void SendCustomer(Customer customer);
    }
}