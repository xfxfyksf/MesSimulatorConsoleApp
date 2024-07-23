using MessageSimulator.Core.Models.Message;

namespace MesSimulatorConsoleApp.MessageBody
{
    [Serializable]
    public class MesEapCarrierInfoReply : Body
    {
        public new const string MESSAGENAME = "MESEAP_CARRIER_INFO_REPLY";
        public string? FACTORYNAME { get; set; }
        public string? CARRIERNAME { get; set; }
        public string? PORTNAME { get; set; }
        public string? JOBUSETYPE { get; set; }
        public string? PURGERECIPE { get; set; }
        public string? SLOTMAP { get; set; }
    }
}
