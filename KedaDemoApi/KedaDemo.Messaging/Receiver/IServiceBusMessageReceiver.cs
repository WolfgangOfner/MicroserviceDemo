using System.Threading.Tasks;

namespace KedaDemo.Messaging.Receiver
{
    public interface IServiceBusMessageReceiver
    {
        Task<int> ReceiveMessagesFromKedaQueue();
    }
}