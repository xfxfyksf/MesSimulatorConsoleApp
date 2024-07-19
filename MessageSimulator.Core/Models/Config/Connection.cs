using MessageSimulator.Core.RabbitMQ.Config;

namespace MessageSimulator.Core.Models.Config;

[Serializable]
public class Connection
{
    public RabbitMQConnectionInfo? RabbitMQConnectionInfo
    {
        get; set;
    }
    public RabbitMQConfig? RabbitMQConfig
    {
        get; set;
    }
}
