namespace CustomerApi.Messaging.Send.Options.v1
{
    public class RabbitMqConfiguration
    {
        public string Hostname { get; set; }

        public string QueueName { get; set; }
    }
}