using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSAS_CargoPay.Core.DTOs.V1._0
{
    public class ProcessPaymentRequestDto
    {
        public string CardNumber { get; set; }
        public decimal Amount { get; set; }
    }

}
