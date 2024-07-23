using MessageSimulator.Core.Models.Message;
using System.Xml.Serialization;

namespace MesSimulatorConsoleApp.MessageBody
{
    [Serializable]
    public class MesEapProcessInfoReplyBody : Body
    {
        public new const string MESSAGENAME = "MESEAP_PROCESS_INFO_REPLY";
        public string? FACTORYNAME { get; set; }

        [XmlArray]
        [XmlArrayItem(ElementName = "CARRIERNAME")]
        public List<string>? CARRIERNAMELIST { get; set; }
        public string? PORTNAME { get; set; }
        public string? JOBUSETYPE { get; set; }
        public string? CONTROLJOBID { get; set; }

        [XmlArray]
        [XmlArrayItem(ElementName = "PROCESSINFO")]
        public List<ProcessInfo>? PROCESSINFOLIST { get; set; }

        public class ProcessInfo
        {
            public string? BATCHID { get; set; }
            public string? BATCHCOUNT { get; set; }
            public string? LOTNAME { get; set; }
            public string? CARRIERNAME { get; set; }
            public string? JOBTYPE { get; set; }
            public string? PRODUCTIONTYPE { get; set; }
            public string? CONTROLJOBID { get; set; }
            public string? PROCESSJOBID { get; set; }
            public string? MACHINERECIPENAME { get; set; }
            public string? STEPPERRECIPEID { get; set; }
            public string? RETICLEID { get; set; }
            public string? DCSPECNAME { get; set; }

            [XmlArray]
            [XmlArrayItem(ElementName = "CARRIERINFO")]
            public List<CarrierInfo>? CARRIERINFOLIST { get; set; }

            public class CarrierInfo
            {
                public string? CARRIERNAME { get; set; }
                public string? SLOTMAP { get; set; }

                [XmlArray]
                [XmlArrayItem(ElementName = "LOTINFO")]
                public List<LotInfo>? LOTINFOLIST { get; set; }

                public class LotInfo
                {
                    public string? LOTNAME { get; set; }
                    public string? CARRIERNAME { get; set; }
                    public string? PROCESSWAFERCOUNT { get; set; }

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
    }
}
