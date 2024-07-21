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
            XmlSerializer xmlSerializer = new(typeof(Message<Body>));
            using StringReader stringReader = new(message);
            Message<Body> msg = (Message<Body>)xmlSerializer.Deserialize(stringReader);
            switch (msg.Header.MESSAGENAME)
            {
                case "EapMesLotInfoRequest":
                    {
                        xmlSerializer = new(typeof(Message<EapMesLotInfoRequestBody>));
                        using StringReader sr = new(message);
                        Message<EapMesLotInfoRequestBody> messageReceived = (Message<EapMesLotInfoRequestBody>)xmlSerializer.Deserialize(sr);
                        Message<MesEapLotInfoRequestReplyBody> messageSend = new(MesEapLotInfoRequestReplyBody.MESSAGENAME, new MesEapLotInfoRequestReplyBody
                        {
                            MACHINENAME = messageReceived.Body.MACHINENAME,
                            PORTNAME = messageReceived.Body.PORTNAME,
                            PORTTYPE = messageReceived.Body.PORTTYPE,
                            CARRIERNAME = messageReceived.Body.CARRIERNAME,
                            OPERATORID = messageReceived.Body.OPERATORID,
                            BATCHID = "",
                            SLOTMAP = "3",
                            PRODUCTLIST =
                            [
                                new()
                            {
                                PLATENO = "PLATENO01",
                                BLANKLOTNO = "BLANKLOTNO01",
                                RECIPENAME = "RECIPENAME01",
                                RECIPEPARAMLIST =
                                [
                                    new()
                                    {
                                        PARAMNAME = "PARAMNAME01",
                                        PARAMVALUE = "PARAMVALUE01"
                                    },
                                    new()
                                    {
                                        PARAMNAME = "PARAMNAME02",
                                        PARAMVALUE = "PARAMVALUE02"
                                    }
                                ],
                                MASKTITLE = "MASKTITLE01",
                                PRODUCTTYPE = string.Empty,
                                PRODUCTIONTYPE = string.Empty,
                                PROCESSOPERATIONNAME = "NG03",
                                WORKORDER = string.Empty,
                                SLOTNO = "1",
                                INPUTACTION = "Y"
                            }
                            ]
                        });

                        Send(messageSend, messageReceived.Header.TRANSACTIONID, true);
                    }
                    break;
                case "EapMesTrackInRequest":
                    {
                        Send(msg, returnOK: true);
                    }
                    break;
                case "EapMesTrackOutRequest":
                    {
                        Send(msg, returnOK: true);
                    }
                    break;
                case "EapMesAreYouThereRequest":
                    {
                        xmlSerializer = new(typeof(Message<EapMesAreYouThereRequestBody>));
                        using StringReader sr = new(message);
                        Message<EapMesAreYouThereRequestBody> messageReceived = (Message<EapMesAreYouThereRequestBody>)xmlSerializer.Deserialize(sr);
                        Message<MesEapAreYouThereRequestReplyBody> messageSend = new(MesEapAreYouThereRequestReplyBody.MESSAGENAME, new MesEapAreYouThereRequestReplyBody
                        {
                            MACHINENAME = messageReceived.Body.MACHINENAME
                        });

                        Send(messageSend, messageReceived.Header.TRANSACTIONID, true);
                    }
                    break;
                case "EapMesLogInRequest":
                    {
                        xmlSerializer = new(typeof(Message<EapMesLogInRequestBody>));
                        using StringReader sr = new(message);
                        Message<EapMesLogInRequestBody> messageReceived = (Message<EapMesLogInRequestBody>)xmlSerializer.Deserialize(sr);
                        Message<MesEapLogInRequestReplyBody> messageSend = new(MesEapLogInRequestReplyBody.MESSAGENAME, new MesEapLogInRequestReplyBody
                        {
                            MACHINENAME = messageReceived.Body.MACHINENAME,
                            USERNAME = messageReceived.Body.USERNAME
                        });

                        Send(messageSend, messageReceived.Header.TRANSACTIONID, true);
                    }
                    break;
                default:
                    break;
            }
        }

        private void Send<T>(Message<T> message, string transactionId = null, bool returnOK = true) where T : Body, new()
        {
            message.Header.TRANSACTIONID = transactionId != null && transactionId != string.Empty ? transactionId : Guid.NewGuid().ToString();
            message.Header.TIMESTAMP = DateTime.Now.ToString("yyyyMMddHHmmssfff");
            message.Header.FACTORYNAME = string.Empty;
            message.Header.ORIGINALSOURCESUBJECTNAME = string.Empty;
            message.Header.SOURCESUBJECTNAME = string.Empty;
            message.Header.TARGETSUBJECTNAME = string.Empty;
            message.Header.EVENTUSER = string.Empty;
            message.Header.EVENTCOMMENT = string.Empty;

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

            MessageSender?.Invoke(xmlString);
        }
    }
}
