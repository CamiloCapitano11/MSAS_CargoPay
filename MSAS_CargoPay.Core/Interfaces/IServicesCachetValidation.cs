using MSAS_CargoPay.Core.DTOs;
using MSAS_CargoPay.Core.DTOs.V1._0;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSAS_CargoPay.Core.Interfaces
{
    public interface IServicesCachetValidation
    {
        ResponseDto<string> ValidateRequestCard(CreateCardRequestDto requestDto);
        ResponseDto<string> ValidateRequestPayment(ProcessPaymentRequestDto requestDto);
        ResponseDto<string> ValidateRequest(string CardNumber);
    }
}
