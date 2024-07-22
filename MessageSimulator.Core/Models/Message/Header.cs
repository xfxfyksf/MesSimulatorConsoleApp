﻿namespace MessageSimulator.Core.Models.Message;

[Serializable]
public class Header
{
    public string? MESSAGENAME { get; set; }
    public string? TRANSACTIONID { get; set; }
    public string? TIMESTAMP { get; set; }
    public string? FACTORYNAME { get; set; }
    public string? ORIGINALSOURCESUBJECTNAME { get; set; }
    public string? SOURCESUBJECTNAME { get; set; }
    public string? TARGETSUBJECTNAME { get; set; }
    public string? EVENTUSER { get; set; }
    public string? EVENTCOMMENT { get; set; }
    public string? TOPIC { get; set; }
    public string? REPLYTO { get; set; }
    public string? MESSAGETYPE { get; set; }
    public string? ROUTINGKEY { get; set; }
    public string? SENDEXCHANGE { get; set; }
    public string? SNEDER { get; set; }

    public Header() { }
    public Header(string messageName)
    {
        MESSAGENAME = messageName;
    }
}
