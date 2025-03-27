using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MSAS_CargoPay.Application.Services;
using MSAS_CargoPay.Core.DTOs;
using MSAS_CargoPay.Core.DTOs.V1._0;
using MSAS_CargoPay.Core.Entities.V1._0;
using MSAS_CargoPay.Core.Interfaces;
using System.Reflection;

namespace MSAS_CargoPay.API.Controllers.V1._0
{
    [Authorize]
    [ApiController]
    [Route("api/v1.0/[controller]")]
    public class CargoPayController : ControllerBase
    {
        private readonly ICargoPayService _cargoPayService;
        public CargoPayController(ICargoPayService Service)
        {
            _cargoPayService = Service;
        }
        [HttpGet]
        [Route("GetCardBalance")]
        public IActionResult GetCardBalance([FromQuery] string CardNumber )
        {
            LogApi _logApi = new("", MethodBase.GetCurrentMethod().Name, HttpContext.Connection.RemoteIpAddress.ToString(), HttpContext.Request.Path, CardNumber, User.FindFirst("aud")?.Value);

            var response = _cargoPayService.GetCardBalance(CardNumber, _logApi);
            return StatusCode(response.ResponseCode, response);
        }
        [HttpPost]
        [Route("CreateCard")]
        public IActionResult CreateCard([FromBody] CreateCardRequestDto requestDto)
        {
            LogApi _logApi = new("", MethodBase.GetCurrentMethod().Name, HttpContext.Connection.RemoteIpAddress.ToString(), HttpContext.Request.Path, requestDto, User.FindFirst("aud")?.Value);

            var response = _cargoPayService.CreateCard(requestDto, _logApi);
            return StatusCode(response.ResponseCode, response);
        }
        [HttpPost]
        [Route("ProcessPayment")]
        public IActionResult ProcessPayment([FromBody] ProcessPaymentRequestDto requestDto)
        {
            LogApi _logApi = new("", MethodBase.GetCurrentMethod().Name, HttpContext.Connection.RemoteIpAddress.ToString(), HttpContext.Request.Path, requestDto, User.FindFirst("aud")?.Value);

            var response = _cargoPayService.ProcessPayment(requestDto, _logApi);
            return StatusCode(response.ResponseCode, response);
        }
    }
}
