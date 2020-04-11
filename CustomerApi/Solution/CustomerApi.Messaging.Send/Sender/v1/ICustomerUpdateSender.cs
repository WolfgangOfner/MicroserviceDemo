using CustomerApi.Data.Entities;

namespace CustomerApi.Messaging.Send.Sender.v1
{
    public interface ICustomerUpdateSender
    {
        void SendCustomer(Customer customer);
    }
}