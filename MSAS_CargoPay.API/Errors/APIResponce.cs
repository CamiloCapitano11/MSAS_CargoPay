using MSAS_CargoPay.Core.Resources;
using System;
using System.Net;

namespace MSAS_CargoPay.API.Errors
{
    public class APIResponce
    {
        public int statusCode { get; set; }
        public string Message { get; set; }

        public APIResponce(int StatusCode, string message = null)
        {
            statusCode = StatusCode;
            Message = message ?? DefaultStatusCodeMessage(StatusCode);
        }
        private string DefaultStatusCodeMessage(int StatusCode)
        {
            return StatusCode switch
            {
                (int)HttpStatusCode.BadRequest => Messages.BadRequest,
                (int)HttpStatusCode.Unauthorized => Messages.Unauthorized,
                (int)HttpStatusCode.NotFound => Messages.NotFound,
                (int)HttpStatusCode.InternalServerError => Messages.InternalServerError,
                0 => Messages.Wrong,
                _ => throw new NotImplementedException()
            };
        }
    }
}
