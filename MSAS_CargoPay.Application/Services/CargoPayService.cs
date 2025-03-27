using Microsoft.Extensions.Configuration;
using MSAS_CargoPay.Core.DTOs;
using MSAS_CargoPay.Core.DTOs.V1._0;
using MSAS_CargoPay.Core.Entities.V1._0;
using MSAS_CargoPay.Core.Interfaces;
using MSAS_CargoPay.Core.Resources;
using MSAS_CargoPay.Infrastracture.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MSAS_CargoPay.Application.Services
{
    public class CargoPayService : ICargoPayService
    {
        private readonly ICargoPayRepository _serviceRepository;
        private readonly DatabaseLogger _logger;
        public CargoPayService(ICargoPayRepository serviceRepository, IConfiguration configuration)
        {
            _logger = new DatabaseLogger(configuration);
            _serviceRepository = serviceRepository;
        }

        public ResponseDto<CreateCard> CreateCard(CreateCardRequestDto requestDto, LogApi _logApi)
        {
            ResponseDto<CreateCard> result = new();
            try
            {

                ResponseDto<string> validationRequest = new();
                if (validationRequest.ResponseCode == (int)HttpStatusCode.OK)
                {
                    result = _serviceRepository.CreateCard(requestDto, _logApi);
                }

                _logger.Log(_logApi, result, result.ResponseCode.ToString());

                result.ResponseMessage = result.ResponseCode == (int)HttpStatusCode.BadRequest ? "Sistema no disponible." : result.ResponseMessage;

                return result;
            }
            catch (Exception ex) when (ex.Data != null)
            {
                result.Status = Messages.StatusError;
                result.ResponseCode = (int)HttpStatusCode.BadRequest;
                result.ResponseMessage = $"{"Sistema no disponible."} | {ex.Message}";
                _logApi.ResponseCode = result.ResponseCode.ToString();
                _logger.Log(_logApi, result, result.ResponseCode.ToString());

                result.ResponseMessage = "Sistema no disponible.";
                return result;
            }
        }

        public ResponseDto<CardBalance> GetCardBalance(string CardNumber, LogApi log)
        {
            ResponseDto<CardBalance> result = new();
            try
            {

                ResponseDto<string> validationRequest = new();
                if (validationRequest.ResponseCode == (int)HttpStatusCode.OK)
                {
                    result = _serviceRepository.GetCardBalance(CardNumber, log);
                }

                _logger.Log(log, result, result.ResponseCode.ToString());

                result.ResponseMessage = result.ResponseCode == (int)HttpStatusCode.BadRequest ? "Sistema no disponible." : result.ResponseMessage;

                return result;
            }
            catch (Exception ex) when (ex.Data != null)
            {
                result.Status = Messages.StatusError;
                result.ResponseCode = (int)HttpStatusCode.BadRequest;
                result.ResponseMessage = $"{"Sistema no disponible."} | {ex.Message}";
                log.ResponseCode = result.ResponseCode.ToString();
                _logger.Log(log, result, result.ResponseCode.ToString());

                result.ResponseMessage = "Sistema no disponible.";
                return result;
            }
        }

        public ResponseDto<ProcessPayment> ProcessPayment(ProcessPaymentRequestDto requestDto, LogApi _logApi)
        {
            ResponseDto<ProcessPayment> result = new();
            try
            {

                ResponseDto<string> validationRequest = new();
                if (validationRequest.ResponseCode == (int)HttpStatusCode.OK)
                {
                    result = _serviceRepository.ProcessPayment(requestDto, _logApi);
                }

                _logger.Log(_logApi, result, result.ResponseCode.ToString());

                result.ResponseMessage = result.ResponseCode == (int)HttpStatusCode.BadRequest ? "Sistema no disponible." : result.ResponseMessage;

                return result;
            }
            catch (Exception ex) when (ex.Data != null)
            {
                result.Status = Messages.StatusError;
                result.ResponseCode = (int)HttpStatusCode.BadRequest;
                result.ResponseMessage = $"{"Sistema no disponible."} | {ex.Message}";
                _logApi.ResponseCode = result.ResponseCode.ToString();
                _logger.Log(_logApi, result, result.ResponseCode.ToString());

                result.ResponseMessage = "Sistema no disponible.";
                return result;
            }
        }
    }
}
