using MessageSimulator.Core.Models.Message;
using System.Xml.Serialization;
using static MesSimulatorConsoleApp.MessageBody.EapMesTrackOutRequestBody;

namespace MesSimulatorConsoleApp.MessageBody
{
    [Serializable]
    public class MesEapTrackOutReplyBody : Body
    {
        public new const string MESSAGENAME = "MESEAP_TRACK_OUT_REPLY";
        public string? FACTORYNAME { get; set; }
        public string? PROCESSJOBID { get; set; }
        public string? PORTNAME { get; set; }
        public string? MACHINERECIPENAME { get; set; }
        public string? STEPPERRECIPEID { get; set; }
        public string? LAYOUTRECIPENAME { get; set; }
        public string? CARRIERMAP { get; set; }

        [XmlArray]
        [XmlArrayItem(ElementName = "CARRIERINFO")]
        public List<CarrierInfo>? CARRIERINFOLIST { get; set; }

        //public class CarrierInfo
        //{
        //    public string? CARRIERNAME { get; set; }

        //    [XmlArray]
        //    [XmlArrayItem(ElementName = "LOTINFO")]
        //    public List<LotInfo>? LOTINFOLIST { get; set; }

        //    public class LotInfo
        //    {
        //        public string? LOTNAME { get; set; }
        //        public string? CARRIERNAME { get; set; }
        //        public string? SEQNBR { get; set; }
        //    }
        //}

        [XmlArray]
        [XmlArrayItem(ElementName = "LOT")]
        public List<string>? LOTLIST { get; set; }

        [XmlArray]
        [XmlArrayItem(ElementName = "DUMMYINFO")]
        public List<string>? DUMMYINFOLIST { get; set; }
    }
}
