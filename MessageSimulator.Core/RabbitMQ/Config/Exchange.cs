namespace MessageSimulator.Core.RabbitMQ.Config;

[Serializable]
public class Exchange
{
    public string? ExchangeName
    {
        get; set;
    }
    public string? Type
    {
        get; set;
    }
    public bool Durable
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
