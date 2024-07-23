using MessageSimulator.Core.Models.Message;
using System.Xml.Serialization;

namespace MesSimulatorConsoleApp.MessageBody
{
    [Serializable]
    public class MesEapCarrierInfoReplyBody : Body
    {
        public new const string MESSAGENAME = "MESEAP_CARRIER_INFO_REPLY";
        public string? FACTORYNAME { get; set; }
        public string? CARRIERNAME { get; set; }
        public string? PORTNAME { get; set; }
        public string? JOBUSETYPE { get; set; }
        public string? PURGERECIPE { get; set; }
        public string? SLOTMAP { get; set; }

        [XmlArray]
        [XmlArrayItem(ElementName = "LOTINFO")]
        public List<LotInfo>? LOTINFOLIST { get; set; }

        public class LotInfo
        {
            public string? LOTNAME { get; set; }
            public string? PJID { get; set; }
            public string? CJID { get; set; }
            public string? BATCHID { get; set; }
            public string? BATCHCOUNT { get; set; }
            public string? PROCESSWAFERCOUNT { get; set; }
            public string? PRODUCTIONTYPE { get; set; }
            public string? MACHINERECIPENAME { get; set; }

            [XmlArray]
            [XmlArrayItem(ElementName = "SLOTINFO")]
            public List<SlotInfo>? SLOTINFOLIST { get; set; }

            public class SlotInfo
            {
                public string? SLOTID { get; set; }
                public string? WAFERID { get; set; }
                public string? T7CODE { get; set; }
                public string? SELECTED { get; set; }
                public string? LOTNAME { get; set; }
            }
        }
    }
}
