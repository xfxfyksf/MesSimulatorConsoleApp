using MessageSimulator.Core.Models.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MesSimulatorConsoleApp.MessageBody
{
    [Serializable]
    public class MesEapLogInRequestReplyBody : Body
    {
        public new const string MESSAGENAME = "MesEapLogInRequestReply";
        public string USERNAME { get; set; }
    }
}
