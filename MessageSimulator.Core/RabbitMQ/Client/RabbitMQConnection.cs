using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using MessageSimulator.Core.Models.Config;

namespace MessageSimulator.Core.RabbitMQ.Client;
public class RabbitMQConnection(ConnectionInfo connectionInfo) : Models.IConnection
{
    public IConnection? Connection
    {
        get; private set;
    }
    private IModel? ConsumerChannel
    {
        get; set;
    }
    private IModel? ProducerChannel
    {
        get; set;
    }
    public EventingBasicConsumer? Consumer
    {
        get; private set;
    }

    public ConnectionInfo ConnectionInfo
    {
        get; private set;
    } = connectionInfo;

#pragma warning disable CS8602 // 解引用可能出现空引用。
    public void Connect()
    {
        Disconnect();
        try
        {
            // 创建 connection
            var connectionFactory = new ConnectionFactory
            {
                ClientProvidedName = ConnectionInfo.ConnectionName,
                Port = ConnectionInfo.Connection.RabbitMQConnectionInfo.Port,
                UserName = ConnectionInfo.Connection.RabbitMQConnectionInfo.UserName,
                Password = ConnectionInfo.Connection.RabbitMQConnectionInfo.Password,
                VirtualHost = ConnectionInfo.Connection.RabbitMQConnectionInfo.VirtualHost
            };
            Connection = connectionFactory.CreateConnection(ConnectionInfo.Connection.RabbitMQConnectionInfo.HostName.Split(';'));

            // 创建 consumer channel
            ConsumerChannel = Connection.CreateModel();

            // 创建 consumer
            ConsumerChannel.ExchangeDeclare(
                ConnectionInfo.Connection.RabbitMQConfig.Consumer.Exchange.ExchangeName,
                ConnectionInfo.Connection.RabbitMQConfig.Consumer.Exchange.Type,
                ConnectionInfo.Connection.RabbitMQConfig.Consumer.Exchange.Durable,
                ConnectionInfo.Connection.RabbitMQConfig.Consumer.Exchange.AutoDelete,
                ConnectionInfo.Connection.RabbitMQConfig.Consumer.Exchange.Arguments
                );
            ConsumerChannel.QueueDeclare(
                ConnectionInfo.Connection.RabbitMQConfig.Consumer.Queue.QueueName,
                ConnectionInfo.Connection.RabbitMQConfig.Consumer.Queue.Durable,
                ConnectionInfo.Connection.RabbitMQConfig.Consumer.Queue.Exclusive,
                ConnectionInfo.Connection.RabbitMQConfig.Consumer.Queue.AutoDelete,
                ConnectionInfo.Connection.RabbitMQConfig.Consumer.Queue.Arguments
                );
            ConsumerChannel.QueueBind(
                ConnectionInfo.Connection.RabbitMQConfig.Consumer.Queue.QueueName,
                ConnectionInfo.Connection.RabbitMQConfig.Consumer.Exchange.ExchangeName,
                ConnectionInfo.Connection.RabbitMQConfig.Consumer.RoutingKey,
                null
                );
            Consumer = new EventingBasicConsumer(ConsumerChannel);

            // 创建 producer channel
            ProducerChannel = Connection.CreateModel();

            // 创建 producer
            //ProducerChannel.ExchangeDeclare(
            //    ConnectionInfo.Connection.RabbitMQConfig.Producer.Exchange.ExchangeName,
            //    ConnectionInfo.Connection.RabbitMQConfig.Producer.Exchange.Type,
            //    ConnectionInfo.Connection.RabbitMQConfig.Producer.Exchange.Durable,
            //    ConnectionInfo.Connection.RabbitMQConfig.Producer.Exchange.AutoDelete,
            //    ConnectionInfo.Connection.RabbitMQConfig.Producer.Exchange.Arguments
            //    );
            //ProducerChannel.QueueDeclare(
            //    ConnectionInfo.Connection.RabbitMQConfig.Producer.Queue.QueueName,
            //    ConnectionInfo.Connection.RabbitMQConfig.Producer.Queue.Durable,
            //    ConnectionInfo.Connection.RabbitMQConfig.Producer.Queue.Exclusive,
            //    ConnectionInfo.Connection.RabbitMQConfig.Producer.Queue.AutoDelete,
            //    ConnectionInfo.Connection.RabbitMQConfig.Producer.Queue.Arguments
            //    );
            //ProducerChannel.QueueBind(
            //    ConnectionInfo.Connection.RabbitMQConfig.Producer.Queue.QueueName,
            //    ConnectionInfo.Connection.RabbitMQConfig.Producer.Exchange.ExchangeName,
            //    ConnectionInfo.Connection.RabbitMQConfig.Producer.RoutingKey,
            //    null
            //    );
        }
        catch (Exception)
        {
            throw;
        }
    }
    public void Disconnect()
    {
        ConsumerChannel?.Close();
        ConsumerChannel?.Dispose();
        ConsumerChannel = null;
        ProducerChannel?.Close();
        ProducerChannel?.Dispose();
        ProducerChannel = null;
        Connection?.Close();
        Connection?.Dispose();
        Connection = null;
    }
    public bool IsConnected()
    {
        return Connection != null && Connection.IsOpen;
    }
    public void Receive()
    {
        ConsumerChannel.BasicConsume(
            ConnectionInfo.Connection.RabbitMQConfig.Consumer.Queue.QueueName,
            true,
            Consumer
            );
    }
    public void Send(string message, string machineName)
    {
        ProducerChannel?.ExchangeDeclare(
                ConnectionInfo.Connection.RabbitMQConfig.Producer.Exchange.ExchangeName.Replace("${MachineName}", machineName),
                ConnectionInfo.Connection.RabbitMQConfig.Producer.Exchange.Type,
                ConnectionInfo.Connection.RabbitMQConfig.Producer.Exchange.Durable,
                ConnectionInfo.Connection.RabbitMQConfig.Producer.Exchange.AutoDelete,
                ConnectionInfo.Connection.RabbitMQConfig.Producer.Exchange.Arguments
                );
        ProducerChannel?.QueueDeclare(
            ConnectionInfo.Connection.RabbitMQConfig.Producer.Queue.QueueName.Replace("${MachineName}", machineName),
            ConnectionInfo.Connection.RabbitMQConfig.Producer.Queue.Durable,
            ConnectionInfo.Connection.RabbitMQConfig.Producer.Queue.Exclusive,
            ConnectionInfo.Connection.RabbitMQConfig.Producer.Queue.AutoDelete,
            ConnectionInfo.Connection.RabbitMQConfig.Producer.Queue.Arguments
            );
        ProducerChannel?.QueueBind(
            ConnectionInfo.Connection.RabbitMQConfig.Producer.Queue.QueueName.Replace("${MachineName}", machineName),
            ConnectionInfo.Connection.RabbitMQConfig.Producer.Exchange.ExchangeName.Replace("${MachineName}", machineName),
            ConnectionInfo.Connection.RabbitMQConfig.Producer.RoutingKey.Replace("${MachineName}", machineName),
            null
            );

        // 发布消息
        var messageBytes = Encoding.UTF8.GetBytes(message);
        ProducerChannel?.BasicPublish(
            ConnectionInfo.Connection.RabbitMQConfig.Producer.Exchange.ExchangeName.Replace("${MachineName}", machineName),
            ConnectionInfo.Connection.RabbitMQConfig.Producer.RoutingKey.Replace("${MachineName}", machineName),
            null,
            messageBytes
            );
    }
#pragma warning restore CS8602 // 解引用可能出现空引用。
}
