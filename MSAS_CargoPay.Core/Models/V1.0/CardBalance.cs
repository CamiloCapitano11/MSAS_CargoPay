using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSAS_CargoPay.Core.DTOs
{
    public class CardBalance
    {
        public string CardNumber { get; set; }
        public decimal Balance { get; set; }
        public string Message { get; set; }
    }

}
