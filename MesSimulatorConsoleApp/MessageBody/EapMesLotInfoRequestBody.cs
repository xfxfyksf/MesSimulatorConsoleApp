using MessageSimulator.Core.Models.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MesSimulatorConsoleApp.MessageBody
{
    [Serializable]
    public class EapMesLotInfoRequestBody : Body
    {
        public new const string MESSAGENAME = "EapMesLotInfoRequest";
        public string PORTNAME { get; set; }
        public string PORTTYPE { get; set; }
        public string CARRIERNAME { get; set; }
        public string OPERATORID { get; set; }
    }
}
