using System.Threading.Tasks;

namespace KedaDemo.Messaging.Writer
{
    public interface IServiceBusMessageWriter
    {
        Task WriteMessagesToKedaQueue(int numberOfQueueItems);
    }
}