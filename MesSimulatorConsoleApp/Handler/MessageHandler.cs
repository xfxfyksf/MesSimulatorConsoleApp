using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using MessageSimulator.Core.Models.Message;
using System;
using MesSimulatorConsoleApp.MessageBody;

namespace MesSimulatorConsoleApp.Handler
{
    public class MessageHandler : IMessageHandler
    {
        public event MessageSenderDelegate MessageSender;

        public void Handel(string message)
        {
            var slotCount = 25;
            //var formattedDateTime = DateTime.Now.ToString("yyMMddHHmmss");
            var formattedDateTime = "000000000000"; // 为了保持信息的一致性，使用固定值
            var lotName = "LOT_" + formattedDateTime;
            var controlJobId = "CJ_" + formattedDateTime;
            var processJobId = "PJ_" + formattedDateTime;
            var slotMap = new string('1', slotCount);
            var jobUseType = "lot";
            var productionType = "Production";
            var machineRecipeName = "PPID01";

            XmlSerializer xmlSerializer = new(typeof(Message<Body>));
            using StringReader stringReader = new(message);
            Message<Body> msg = (Message<Body>)xmlSerializer.Deserialize(stringReader);
            switch (msg.Header.MESSAGENAME)
            {
                case EapMesCarrierInfoRequestBody.MESSAGENAME:
                    {
                        List<MesEapCarrierInfoReplyBody.LotInfo.SlotInfo> slotInfoList = [];
                        for (int i = 1; i <= slotCount; i++)
                        {
                            slotInfoList.Add(new MesEapCarrierInfoReplyBody.LotInfo.SlotInfo
                            {
                                SLOTID = i.ToString(),
                                WAFERID = $"{lotName}#{i:D2}",
                                T7CODE = string.Empty,
                                SELECTED = "Y",
                                LOTNAME = lotName
                            });
                        }

                        xmlSerializer = new(typeof(Message<EapMesCarrierInfoRequestBody>));
                        using StringReader sr = new(message);
                        Message<EapMesCarrierInfoRequestBody> messageReceived = (Message<EapMesCarrierInfoRequestBody>)xmlSerializer.Deserialize(sr);
                        Message<MesEapCarrierInfoReplyBody> messageSend = new(MesEapCarrierInfoReplyBody.MESSAGENAME, new MesEapCarrierInfoReplyBody
                        {
                            MACHINENAME = messageReceived.Body.MACHINENAME,
                            FACTORYNAME = messageReceived.Body.FACTORYNAME,
                            CARRIERNAME = messageReceived.Body.CARRIERNAME,
                            PORTNAME = messageReceived.Body.PORTNAME,
                            JOBUSETYPE = jobUseType,
                            PURGERECIPE = string.Empty,
                            SLOTMAP = slotMap,
                            LOTINFOLIST =
                            [
                                new()
                                {
                                    LOTNAME = lotName,
                                    PJID = processJobId,
                                    CJID = controlJobId,
                                    BATCHID = string.Empty,
                                    BATCHCOUNT = string.Empty,
                                    PROCESSWAFERCOUNT = slotCount.ToString(),
                                    PRODUCTIONTYPE = productionType,
                                    MACHINERECIPENAME = machineRecipeName,
                                    SLOTINFOLIST = slotInfoList
                                }
                            ]
                        });

                        Send(messageSend, messageReceived.Header.TRANSACTIONID, messageReceived.Header.FACTORYNAME, true);
                    }
                    break;
                case EapMesProcessInfoRequestBody.MESSAGENAME:
                    {
                        List<MesEapProcessInfoReplyBody.ProcessInfo.CarrierInfo.LotInfo.SlotInfo> slotInfoList = [];
                        for (int i = 1; i <= slotCount; i++)
                        {
                            slotInfoList.Add(new MesEapProcessInfoReplyBody.ProcessInfo.CarrierInfo.LotInfo.SlotInfo
                            {
                                SLOTID = i.ToString(),
                                WAFERID = $"{lotName}#{i:D2}",
                                T7CODE = string.Empty,
                                SELECTED = "Y",
                                LOTNAME = lotName
                            });
                        }

                        xmlSerializer = new(typeof(Message<EapMesProcessInfoRequestBody>));
                        using StringReader sr = new(message);
                        Message<EapMesProcessInfoRequestBody> messageReceived = (Message<EapMesProcessInfoRequestBody>)xmlSerializer.Deserialize(sr);
                        Message<MesEapProcessInfoReplyBody> messageSend = new(MesEapProcessInfoReplyBody.MESSAGENAME, new MesEapProcessInfoReplyBody
                        {
                            MACHINENAME = messageReceived.Body.MACHINENAME,
                            FACTORYNAME = messageReceived.Body.FACTORYNAME,
                            CARRIERNAMELIST = messageReceived.Body.CARRIERNAMELIST,
                            PORTNAME = messageReceived.Body.PORTNAME,
                            JOBUSETYPE = jobUseType,
                            CONTROLJOBID = controlJobId,
                            PROCESSINFOLIST =
                            [
                                new()
                                {
                                    BATCHID = string.Empty,
                                    BATCHCOUNT = string.Empty,
                                    LOTNAME = lotName,
                                    CARRIERNAME = messageReceived.Body.CARRIERNAMELIST[0],
                                    JOBTYPE = string.Empty,
                                    PRODUCTIONTYPE = productionType,
                                    CONTROLJOBID = controlJobId,
                                    PROCESSJOBID = processJobId,
                                    MACHINERECIPENAME = machineRecipeName,
                                    STEPPERRECIPEID = string.Empty,
                                    RETICLEID = string.Empty,
                                    DCSPECNAME = string.Empty,
                                    CARRIERINFOLIST =
                                    [
                                        new()
                                        {
                                            CARRIERNAME = messageReceived.Body.CARRIERNAMELIST[0],
                                            SLOTMAP = slotMap,
                                            LOTINFOLIST =
                                            [
                                                new()
                                                {
                                                    LOTNAME = lotName,
                                                    CARRIERNAME = messageReceived.Body.CARRIERNAMELIST[0],
                                                    PROCESSWAFERCOUNT = slotCount.ToString(),
                                                    SLOTINFOLIST = slotInfoList
                                                }
                                            ]
                                        }
                                    ]
                                }
                            ]
                        });

                        Send(messageSend, messageReceived.Header.TRANSACTIONID, messageReceived.Header.FACTORYNAME, true);
                    }
                    break;
                case EapMesTrackInRequestBody.MESSAGENAME:
                    {
                        xmlSerializer = new(typeof(Message<EapMesTrackInRequestBody>));
                        using StringReader sr = new(message);
                        Message<EapMesTrackInRequestBody> messageReceived = (Message<EapMesTrackInRequestBody>)xmlSerializer.Deserialize(sr);
                        Message<MesEapTrackInReplyBody> messageSend = new(MesEapTrackInReplyBody.MESSAGENAME, new MesEapTrackInReplyBody
                        {
                            MACHINENAME = messageReceived.Body.MACHINENAME,
                            FACTORYNAME = messageReceived.Body.FACTORYNAME,
                            PROCESSJOBINFOLIST = messageReceived.Body.PROCESSJOBINFOLIST
                        });

                        Send(messageSend, messageReceived.Header.TRANSACTIONID, messageReceived.Header.FACTORYNAME, true);
                    }
                    break;
                case EapMesTrackOutRequestBody.MESSAGENAME:
                    {
                        xmlSerializer = new(typeof(Message<EapMesTrackOutRequestBody>));
                        using StringReader sr = new(message);
                        Message<EapMesTrackOutRequestBody> messageReceived = (Message<EapMesTrackOutRequestBody>)xmlSerializer.Deserialize(sr);
                        Message<MesEapTrackOutReplyBody> messageSend = new(MesEapTrackOutReplyBody.MESSAGENAME, new MesEapTrackOutReplyBody
                        {
                            MACHINENAME = messageReceived.Body.MACHINENAME,
                            FACTORYNAME = messageReceived.Body.FACTORYNAME,
                            PROCESSJOBID = messageReceived.Body.PROCESSJOBID,
                            PORTNAME = string.Empty,
                            MACHINERECIPENAME = messageReceived.Body.MACHINERECIPENAME,
                            STEPPERRECIPEID = messageReceived.Body.STEPPERRECIPEID,
                            LAYOUTRECIPENAME = string.Empty,
                            CARRIERMAP = string.Empty,
                            CARRIERINFOLIST = messageReceived.Body.CARRIERINFOLIST,
                            LOTLIST = [],
                            DUMMYINFOLIST = []
                        });

                        Send(messageSend, messageReceived.Header.TRANSACTIONID, messageReceived.Header.FACTORYNAME, true);
                    }
                    break;
                default:
                    break;
            }
        }

        private void Send<T>(Message<T> message, string transactionId = null, string factoryName = null, bool returnOK = true) where T : Body, new()
        {
            message.Header.TRANSACTIONID = transactionId != null && transactionId != string.Empty ? transactionId : Guid.NewGuid().ToString();
            message.Header.TIMESTAMP = DateTime.Now.ToString("yyyyMMddHHmmssfff");
            message.Header.FACTORYNAME = factoryName != null && factoryName != string.Empty ? factoryName : string.Empty;
            message.Header.ORIGINALSOURCESUBJECTNAME = string.Empty;
            message.Header.SOURCESUBJECTNAME = string.Empty;
            message.Header.TARGETSUBJECTNAME = string.Empty;
            message.Header.EVENTUSER = string.Empty;
            message.Header.EVENTCOMMENT = string.Empty;
            message.Header.TOPIC = string.Empty;
            message.Header.REPLYTO = string.Empty;
            message.Header.MESSAGETYPE = string.Empty;
            message.Header.ROUTINGKEY = string.Empty;
            message.Header.SENDEXCHANGE = string.Empty;
            message.Header.SNEDER = string.Empty;

            if (returnOK)
            {
                message.Return = new Return
                {
                    RETURNCODE = 0,
                    RETURNMESSAGE = string.Empty
                };
            }
            else
            {
                message.Return = new Return
                {
                    RETURNCODE = 1,
                    RETURNMESSAGE = "NG"
                };
            }

            // 配置 XmlWriterSettings 以使用 UTF-8 编码
            XmlWriterSettings settings = new()
            {
                Encoding = new UTF8Encoding(false), // false 不会写入 BOM (Byte Order Mark)
                Indent = true, // 格式化输出
            };

            // 创建空的命名空间以去除默认命名空间
            XmlSerializerNamespaces namespaces = new();
            namespaces.Add(string.Empty, string.Empty);

            // 序列化为 UTF-8 编码的 XML 字符串
            using MemoryStream memoryStream = new();
            using (XmlWriter xmlWriter = XmlWriter.Create(memoryStream, settings))
            {
                XmlSerializer serializer = new(typeof(Message<T>));
                serializer.Serialize(xmlWriter, message, namespaces);
                xmlWriter.Flush();
            }
            string xmlString = Encoding.UTF8.GetString(memoryStream.ToArray());

            MessageSender?.Invoke(xmlString, message.Body.MACHINENAME);
        }
    }
}
