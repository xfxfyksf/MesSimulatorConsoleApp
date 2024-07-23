using MessageSimulator.Core.Models.Message;

namespace MesSimulatorConsoleApp.MessageBody
{
    [Serializable]
    public class EapMesCarrierInfoRequestBody : Body
    {
        public new const string MESSAGENAME = "EAPMES_CARRIER_INFO_REQUEST";
        public string? FACTORYNAME { get; set; }
        public string? CARRIERNAME { get; set; }
        public string? PORTNAME { get; set; }
    }
}
