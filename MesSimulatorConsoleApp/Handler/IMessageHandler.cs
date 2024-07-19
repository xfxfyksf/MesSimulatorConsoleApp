namespace MesSimulatorConsoleApp.Handler
{
    public delegate void MessageSenderDelegate(string message);

    public interface IMessageHandler
    {
        event MessageSenderDelegate MessageSender;

        void Handel(string message);
    }
}
