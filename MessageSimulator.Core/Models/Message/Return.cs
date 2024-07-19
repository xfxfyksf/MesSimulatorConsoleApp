namespace MessageSimulator.Core.Models.Message;

[Serializable]
public class Return
{
    public int RETURNCODE { get; set; }
    public string? RETURNMESSAGE { get; set; }
}
