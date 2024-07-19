namespace MessageSimulator.Core.RabbitMQ.Config;

[Serializable]
public class Queue
{
    public string? QueueName
    {
        get; set;
    }
    public bool Durable
    {
        get; set;
    }
    public bool Exclusive
    {
        get; set;
    }
    public bool AutoDelete
    {
        get; set;
    }
    public Dictionary<string, object>? Arguments
    {
        get; set;
    }
}
