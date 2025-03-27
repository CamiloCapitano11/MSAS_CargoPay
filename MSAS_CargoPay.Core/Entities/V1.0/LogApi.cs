using MSAS_CargoPay.Core.Resources;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSAS_CargoPay.Core.Entities.V1._0
{
    public class LogApi
    {
        public LogApi(string application, string method, string ip, string requestUrl, object request, string clientId)
        {
            StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Application = application;
            Method = method;
            IP = ip;
            RequestUrl = requestUrl;
            Requests = JsonConvert.SerializeObject(request);
            Server = Environment.MachineName;
            Microservice = Messages.Microservice;
            ClientId = clientId;
        }

        public string Application { get; set; }
        public string Method { get; set; }
        public string Requests { get; set; }
        public string Response { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string IP { get; set; }
        public string RequestUrl { get; set; }
        public string Server { get; set; }
        public string Query { get; set; }
        public string Microservice { get; set; }
        public string ClientId { get; set; }
        public string ResponseCode { get; set; }
    }
}
