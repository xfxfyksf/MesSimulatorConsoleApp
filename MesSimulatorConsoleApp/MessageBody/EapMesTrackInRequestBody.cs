using MessageSimulator.Core.Models.Message;
using System.Xml.Serialization;

namespace MesSimulatorConsoleApp.MessageBody
{
    [Serializable]
    public class EapMesTrackInRequestBody : Body
    {
        public new const string MESSAGENAME = "EAPMES_TRACK_IN_REQUEST";
        public string? FACTORYNAME { get; set; }

        [XmlArray]
        [XmlArrayItem(ElementName = "PROCESSJOBINFO")]
        public List<ProcessJobInfo>? PROCESSJOBINFOLIST { get; set; }
        
        public class ProcessJobInfo
        {
            public string? PROCESSJOBID { get; set; }
            public string? MACHINERECIPENAME { get; set; }
            public string? STEPPERRECIPEID { get; set; }

            [XmlArray]
            [XmlArrayItem(ElementName = "CARRIERINFO")]
            public List<CarrierInfo>? CARRIERINFOLIST { get; set; }

            public class CarrierInfo
            {
                public string? PORTNAME { get; set; }
                public string? CARRIERNAME { get; set; }

                [XmlArray]
                [XmlArrayItem(ElementName = "LOTNAME")]
                public List<string>? LOTINFOLIST { get; set; }
            }
        }
    }
}
