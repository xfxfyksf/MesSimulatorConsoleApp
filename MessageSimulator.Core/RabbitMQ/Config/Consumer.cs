﻿namespace MessageSimulator.Core.RabbitMQ.Config;

[Serializable]
public class Consumer
{
    public string? RoutingKey
    {
        get; set;
    }
    public Exchange? Exchange
    {
        get; set;
    }
    public Queue? Queue
    {
        get; set;
    }
}
