using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSAS_CargoPay.Core.DTOs
{
    public class ProcessPayment
    {
        public int TransactionId { get; set; }
        public string CardNumber { get; set; }
        public decimal Amount { get; set; }
        public string Message { get; set; }
    }

}
