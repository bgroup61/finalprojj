using Attemp2.Services.interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Attemp2.Services
{
    public class SMSService : ISMSService
    {
        private readonly string username;
        private readonly string password;
        private readonly string from;
        private readonly bool send;
        public SMSService()
        {
            this.username = "Tawazon";
            this.password = "Tawazon2021";
            this.send = true;
            this.from = "MovieWeb";

            if (string.IsNullOrEmpty(this.from))
                this.from = "MovieWeb";
        }
        public void SendSingleSmsAsync(string phone, string message)
        {
            if (!send) return;
            Task.Run(() => { SendSingleSms(phone, message); });
        }
        public bool SendSingleSms(string phone, string message)
        {
            if (!send) return true ;
            phone = phone.Replace("-", "").Replace(" ", "");
            string sbXml = @"<?xml version=""1.0"" encoding=""utf-8""?>"
            + "<sms>"
            + "<user>"
            + $"<username>{this.username}</username>"
            + $"<password>{this.password}</password>"
            + "</user>"
            + $"<source>{this.from}</source>"
            + "<destinations>"
            + "<phone>" + phone + "</phone>"
            + "</destinations>"
            + "<message>" + message + "</message>"
            + "<response>0</response>"
            + "</sms>";
            var xmlResult = TelzarPostRequest("https://www.019sms.co.il:8090/api/", sbXml.ToString());
            return TelzarParseSMSResult(xmlResult);
        }


        public string GetBalance()
        {
            string sbXml = @"<?xml version=""1.0"" encoding=""utf-8""?>"
            + "<balance>"
            + "<user>"
            + $"<username>{this.username}</username>"
            + $"<password>{this.password}</password>"
            + "</user>"
            + "</balance>";
            var xmlResult = TelzarPostRequest("https://www.019sms.co.il:8090/api/", sbXml.ToString());
            if (xmlResult == null) return null;
            return TelzarParseBalanceResult(xmlResult);
        }


        public void SendBulkSmsAsync(List<string> to, string message)
        {
            if (!send) return;
            Task.Run(() => { SendBulkSms(to, message); });

        }
        public bool SendBulkSms(List<string> to, string message)
        {
            if (!send) return true;
            string sbXml = @"<?xml version=""1.0"" encoding=""utf-8""?>"
            + "<sms>"
            + "<user>"
            + $"<username>{this.username}</username>"
            + $"<password>{this.password}</password>"
            + "</user>"
            + $"<source>{this.from}</source>"
            + "<destinations>";
            foreach (string phone in to)
            {
                var p = phone.Replace("-", "").Replace(" ", "");
                sbXml += "<phone>" + p + " </phone>";
            }
            sbXml += "</destinations>"
            + "<message>" + message + "</message>"
            + "<response>0</response>"
            + "</sms>";
            var xmlResult = TelzarPostRequest("https://www.019sms.co.il:8090/api/", sbXml.ToString());
            return TelzarParseSMSResult(xmlResult);
        }


        private string TelzarPostRequest(string url, string xml)
        {
            try
            {
                WebRequest webRequest = WebRequest.Create(url);
                webRequest.Method = "POST";
                byte[] bytes = Encoding.UTF8.GetBytes(xml);
                webRequest.ContentType = "application/xml";
                webRequest.ContentLength = (long)bytes.Length;
                Stream requestStream = webRequest.GetRequestStream();
                requestStream.Write(bytes, 0, bytes.Length);
                requestStream.Close();
                WebResponse response = webRequest.GetResponse();
                Stream responseStream = response.GetResponseStream();
                StreamReader streamReader = new StreamReader(responseStream);
                string result = streamReader.ReadToEnd();
                streamReader.Close();
                responseStream.Close();
                response.Close();
                return result;
            }
            catch (Exception) { return null; }
        }
        private bool TelzarParseSMSResult(string result)
        {
            string res = "CallToSupport";
            if (result == null)
            {
                res = "UnknownError";
                return false;
            }
            if (result.Contains("<status>0</status>"))
            {
                res = "Success";
                return true;
            }
            if (result.Contains("<status>1</status>"))
            {
                res = "XmlError";
                return false;
            }
            if (result.Contains("<status>2</status>"))
            {
                res = "MissingField";
                return false;
            }
            if (result.Contains("<status>3</status>"))
            {
                res = "BadLogin";
                return false;
            }
            if (result.Contains("<status>4</status>"))
            {
                res = "NoCredits";
                return false;
            }
            if (result.Contains("<status>5</status>"))
            {
                res = "NoPermission";
                return false;
            }
            if (result.Contains("<status>997</status>"))
            {
                res = "InvalidCommand";
                return false;
            }
            if (result.Contains("<status>998</status>") || !result.Contains("<status>999</status>"))
            {
                res = "UnknownError";
                return false;
            }
            return res.ToLower().Equals("success");
        }
        private string TelzarParseBalanceResult(string result)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(result);
            var nodeList = doc.GetElementsByTagName("balance");
            return nodeList[1].ChildNodes[0].Value;
        }
    }

}
