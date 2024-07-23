using MessageSimulator.Core.Models.Message;
using System.Xml.Serialization;

namespace MesSimulatorConsoleApp.MessageBody
{
    [Serializable]
    public class EapMesProcessInfoRequestBody : Body
    {
        public new const string MESSAGENAME = "EAPMES_PROCESS_INFO_REQUEST";
        public string? FACTORYNAME { get; set; }

        [XmlArray]
        [XmlArrayItem(ElementName = "CARRIERNAME")]
        public List<string>? CARRIERNAMELIST { get; set; }
        public string? PORTNAME { get; set; }
    }
}
