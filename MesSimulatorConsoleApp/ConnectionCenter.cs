using MessageSimulator.Core.Models.Config;
using MessageSimulator.Core.RabbitMQ.Client;
using MesSimulatorConsoleApp.Handler;
using MesSimulatorConsoleApp.Helpers;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace MesSimulatorConsoleApp
{
    internal class ConnectionCenter
    {
        private static readonly Lazy<ConnectionCenter> _instance = new(() => new ConnectionCenter());

        private MessageSimulator.Core.Models.IConnection? Connection
        {
            get; set;
        }

        private MessageHandler? MessageHandler
        {
            get; set;
        }

        private ConnectionInfo? ConnectionInfo
        {
            get; set;
        }

        private ConnectionCenter() { }

        public static ConnectionCenter Instance
        {
            get
            {
                return _instance.Value;
            }
        }

        public async void LoadConfigAndCreateConnection(string configFileName)
        {
            // 加载 config.json
            string config = string.Empty;
            try
            {
                config = await FileLoader.LoadText($"Configs/{configFileName}.json");
            }
            catch (Exception ex)
            {
                ConsoleLog($"ERROR OCCURED: {ex.Message}{Environment.NewLine}{ex.StackTrace}");
            }

            if (config == string.Empty)
            {
                return;
            }
            
            var connectionInfos = JsonConvert.DeserializeObject<List<ConnectionInfo>>(config);
            if (connectionInfos != null)
            {
                // Log("CONFIG LOADED");
                foreach (var connectionInfo in connectionInfos)
                {
                    try
                    {
                        ConnectionInfo = connectionInfo;
                        Connect(connectionInfo);
                        // 暂时仅支持一个连接
                        break;
                    }
                    catch (Exception ex)
                    {
                        ConsoleLog($"CONNECTION CREATION FAILED: {ex.Message}{Environment.NewLine}{ex.StackTrace}");
                    }
                }
            }
        }

        private void Connect(ConnectionInfo? connectionInfo)
        {
            Connection?.Disconnect();

            ArgumentNullException.ThrowIfNull(connectionInfo);
            var connection = MessageSimulator.Core.Models.ConnectionFactory.CreateConnection(connectionInfo);
            Connection = connection;

            // 启动连接
            Connection?.Connect();
            if (Connection != null && Connection is RabbitMQConnection)
            {
                if (Connection is RabbitMQConnection rabbitMQConnection)
                {
                    if (rabbitMQConnection.Connection != null)
                    {
                        // 连接断开事件处理
                        rabbitMQConnection.Connection.ConnectionShutdown += OnDisConnect;
                    }

                    if (rabbitMQConnection.Consumer != null)
                    {
                        // 收到消息事件处理
                        rabbitMQConnection.Consumer.Received += OnReceived;
                    }

                    rabbitMQConnection.Receive();
                }
            }

            // 设置 Handler
            MessageHandler = new MessageHandler();
            MessageHandler.MessageSender += SendMessage;

            ConsoleLog($"CONNECTED");
        }

        private bool IsConnected()
        {
            return Connection != null && Connection.IsConnected();
        }

        private void OnDisConnect(object? sender, ShutdownEventArgs e)
        {
            ConsoleLog($"CONNECTION ABORTED: {e.ReplyCode}{Environment.NewLine}{e.Exception}");
            ConsoleLog("DISCONNECTED");

            Reconnect();
        }

        private void Reconnect()
        {
            while (!IsConnected())
            {
                ConsoleLog("ATTEMPTING TO RECONNECT...");
                try
                {
                    Connect(ConnectionInfo);
                    Task.Delay(5000).Wait(); // Delay before retrying
                }
                catch (Exception ex)
                {
                    ConsoleLog($"RECONNECT FAILED: {ex.Message}");
                    Task.Delay(5000).Wait(); // Delay before retrying
                }
            }
        }

        private void OnReceived(object? sender, BasicDeliverEventArgs e)
        {
            var body = e.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            MessageReceivedConsoleLog(message);

            try
            {
                MessageHandler?.Handel(message);
            }
            catch (Exception ex)
            {
                ConsoleLog($"HANDLE MESSAGE ERROR: {ex.Message}{Environment.NewLine}{ex.StackTrace}");
            }
        }

        private void SendMessage(string message, string machineName)
        {
            try
            {
                if (IsConnected())
                {
                    Connection?.Send(message, machineName);
                    MessageSentConsoleLog(message);
                }
            }
            catch (Exception ex)
            {
                ConsoleLog($"MESSAGE SEND FAILED: {ex.Message}{Environment.NewLine}{ex.StackTrace}");
            }
        }

        private static void ConsoleLog(string log)
        {
            Console.WriteLine(LogHeader() + log);
        }
        private static void ConsoleLog(string description, string messasge)
        {
            ConsoleLog(description + Environment.NewLine + messasge + Environment.NewLine);
        }
        private static void MessageReceivedConsoleLog(string message)
        {
            ConsoleLog("###### RECEIVED MESSAGE ###### ", message);
        }
        private static void MessageSentConsoleLog(string message)
        {
            ConsoleLog("###### SENT MESSAGE ###### ", message);
        }

        private static string LogHeader()
        {
            return "[" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "] - ";
        }
    }
}
