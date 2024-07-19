namespace MessageSimulator.Core.RabbitMQ.Config;

[Serializable]
public class RabbitMQConnectionInfo
{
    public string? HostName
    {
        get; set;
    }
    public int Port
    {
        get; set;
    }
    public string? UserName
    {
        get; set;
    }
    public string? Password
    {
        get; set;
    }
    public string? VirtualHost
    {
        get; set;
    }
}
