using MSAS_CargoPay.Core.DTOs;
using MSAS_CargoPay.Core.DTOs.V1._0;
using MSAS_CargoPay.Core.Entities.V1._0;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSAS_CargoPay.Core.Interfaces
{
    public interface ICargoPayRepository
    {
        ResponseDto<CreateCard> CreateCard(CreateCardRequestDto requestDto, LogApi log);
        ResponseDto<CardBalance> GetCardBalance(string CardNumber, LogApi log);
        ResponseDto<ProcessPayment> ProcessPayment(ProcessPaymentRequestDto requestDto, LogApi log);
    }

}
