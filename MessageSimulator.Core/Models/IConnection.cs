using MessageSimulator.Core.Models.Config;

namespace MessageSimulator.Core.Models;
public interface IConnection
{
    public ConnectionInfo ConnectionInfo
    {
        get;
    }
    void Connect();
    void Disconnect();
    bool IsConnected();
    void Receive();
    void Send(string message, string machineName);
}
