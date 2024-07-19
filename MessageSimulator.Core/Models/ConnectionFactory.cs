using MessageSimulator.Core.Models.Config;
using MessageSimulator.Core.RabbitMQ.Client;

namespace MessageSimulator.Core.Models;
public class ConnectionFactory
{
    public static IConnection CreateConnection(ConnectionInfo connectionInfo)
    {
        return connectionInfo.ConnectionType switch
        {
            "RabbitMQ" => new RabbitMQConnection(connectionInfo),
            // 添加其他类型的连接，如需要
            _ => throw new NotSupportedException($"ConnectionType '{connectionInfo.ConnectionType}' not supported."),
        };
    }
}
