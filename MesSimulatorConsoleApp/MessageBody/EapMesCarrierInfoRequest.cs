using MessageSimulator.Core.Models.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MesSimulatorConsoleApp.MessageBody
{
    [Serializable]
    public class EapMesCarrierInfoRequest : Body
    {
        public new const string MESSAGENAME = "EAPMES_CARRIER_INFO_REQUEST";
        public string? FACTORYNAME { get; set; }
        public string? CARRIERNAME { get; set; }
        public string? PORTNAME { get; set; }
    }
}
