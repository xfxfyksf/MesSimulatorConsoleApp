namespace MessageSimulator.Core.RabbitMQ.Config;

[Serializable]
public class RabbitMQConfig
{
    public Consumer? Consumer
    {
        get; set;
    }
    public Producer? Producer
    {
        get; set;
    }
}
