using System.Xml.Serialization;

namespace MessageSimulator.Core.Models.Message;

[Serializable]
[XmlType(AnonymousType = true)]
[XmlRoot(ElementName = "Message")]
public class Message<T> where T : Body, new()
{
    [XmlElement(ElementName = "Header")]
    public Header? Header { get; set; }

    [XmlElement(ElementName = "Body")]
    public T? Body { get; set; }

    [XmlElement(ElementName = "Return")]
    public Return? Return { get; set; }

    public Message() { }
    public Message(string messageName, T body)
    {
        Header = new(messageName);
        Body = body;
        Return = new();
    }

    public Message(string messageName)
    {
        Header = new(messageName);
        Body = new T();
        Return = new();
    }
}
