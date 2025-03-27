using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSAS_CargoPay.Core.Entities
{
    public class Logger
    {
        public string Application { get; set; }
        public string IP { get; set; }
        public string Method { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Parameters { get; set; }
        public string Response { get; set; }
        public string Server { get; set; }
    }
}
