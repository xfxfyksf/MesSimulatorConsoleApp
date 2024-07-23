using MessageSimulator.Core.Models.Message;
using System.Xml.Serialization;

namespace MesSimulatorConsoleApp.MessageBody
{
    [Serializable]
    public class EapMesTrackOutRequestBody : Body
    {
        public new const string MESSAGENAME = "EAPMES_TRACK_OUT_REQUEST";
        public string? FACTORYNAME { get; set; }
        public string? PROCESSJOBID { get; set; }
        public string? MACHINERECIPENAME { get; set; }
        public string? STEPPERRECIPEID { get; set; }

        [XmlArray]
        [XmlArrayItem(ElementName = "CARRIERINFO")]
        public List<CarrierInfo>? CARRIERINFOLIST { get; set; }

        public class CarrierInfo
        {
            public string? CARRIERNAME { get; set; }

            [XmlArray]
            [XmlArrayItem(ElementName = "LOTINFO")]
            public List<LotInfo>? LOTINFOLIST { get; set; }

            public class LotInfo
            {
                public string? LOTNAME { get; set; }
            }
        }
    }
}
