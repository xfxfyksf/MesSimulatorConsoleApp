namespace MessageSimulator.Core.Models.Config;

[Serializable]
public class ConnectionInfo
{
    public string? ConnectionType
    {
        get; set;
    }
    public string? ConnectionName
    {
        get; set;
    }
    public Connection? Connection
    {
        get; set;
    }
}
