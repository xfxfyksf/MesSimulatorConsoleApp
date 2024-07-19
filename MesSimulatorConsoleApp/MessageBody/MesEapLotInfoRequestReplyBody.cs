using MessageSimulator.Core.Models.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace MesSimulatorConsoleApp.MessageBody
{
    [Serializable]
    public class MesEapLotInfoRequestReplyBody : Body
    {
        public new const string MESSAGENAME = "MesEapLotInfoRequestReply";
        public string PORTNAME { get; set; }
        public string PORTTYPE { get; set; }
        public string CARRIERNAME { get; set; }
        public string OPERATORID { get; set; }
        public string BATCHID { get; set; }
        public string SLOTMAP { get; set; }

        [XmlArray]
        [XmlArrayItem(ElementName = "PRODUCT")]
        public List<Product> PRODUCTLIST { get; set; }

        public class Product
        {
            public string PLATENO { get; set; }
            public string BLANKLOTNO { get; set; }
            public string RECIPENAME { get; set; }

            [XmlArray]
            [XmlArrayItem(ElementName = "RECIPEPARAM")]
            public List<RecipeParam> RECIPEPARAMLIST { get; set; }

            public string MASKTITLE { get; set; }
            public string PRODUCTTYPE { get; set; }
            public string PRODUCTIONTYPE { get; set; }
            public string PROCESSOPERATIONNAME { get; set; }
            public string WORKORDER { get; set; }
            public string SLOTNO { get; set; }
            public string INPUTACTION { get; set; }

            public class RecipeParam
            {
                public string PARAMNAME { get; set; }
                public string PARAMVALUE { get; set; }
            }
        }
    }
}
