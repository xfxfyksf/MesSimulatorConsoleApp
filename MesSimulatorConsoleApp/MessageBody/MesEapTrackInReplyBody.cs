using MessageSimulator.Core.Models.Message;
using System.Xml.Serialization;
using static MesSimulatorConsoleApp.MessageBody.EapMesTrackInRequestBody;

namespace MesSimulatorConsoleApp.MessageBody
{
    [Serializable]
    public class MesEapTrackInReplyBody : Body
    {
        public new const string MESSAGENAME = "MESEAP_TRACK_IN_REPLY";
        public string? FACTORYNAME { get; set; }

        [XmlArray]
        [XmlArrayItem(ElementName = "PROCESSJOBINFO")]
        public List<ProcessJobInfo>? PROCESSJOBINFOLIST { get; set; }
    }
}
